using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Animator enemyAnimator;

    public AudioClip chasePlayerSfx;
    private AudioSource audioSource;
    public bool isChasing;

    //enemy sight
    public bool playerAttacking = false;
    public bool playerIsInLOS = false;
    public float fieldOfViewAngle;
    public float losRadius;
    public LayerMask layerMask;
    public string excludeLayerName;

    //enemy memory
    private bool aiMemorizePlayer = false;
    public float memoryStartTime = 10f;
    private float increasingMemoryTime;

    //enemy sound
    Vector3 noisePosition;
    private bool aiHeardPlayer = false;
    public float noiseTravelDistance = 50f;
    public float spinSpeed = 2f;
    private bool canSpin = false;
    private float isSpinningTime;
    public float spinTime = 5f;

    //patrol enemy
    public Transform[] moveSpots;
    private int randomSpot;

    NavMeshAgent navEnemy;

    public float distToPlayer = 2f;

    //private float randomStrafeStartTime;
    //private float waitStrafeTime;
    public float t_minStrafe;
    public float t_maxStrafe;

    public Transform strafeLeft;
    public Transform strafeRight;
    private int randomStrafeDir;

    //when to chase
    public float chaseRadius = 20f;

    public float facePlayerFactor = 20f;

    //wait time to patrol again
    private float waitTime;
    public float startWaitTime = 2f;

    public float walkSpeed, runSpeed;
    public void respawnPlayer()
    {
        //SceneManager.LoadScene("Stage1");
    }


    private void Awake()
    {
        navEnemy = GetComponent<NavMeshAgent>();
        navEnemy.enabled = true;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1;
        waitTime = startWaitTime;
        //GameObject.Find("PlayerStage").GetComponent<PlayerMovement>().enabled = true;

        // Get the parent's transform
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            // Find all child GameObjects with the specified tag under the parent
            GameObject[] waypointObjects = parentTransform.GetComponentsInChildren<Transform>()
                                                        .Where(child => child.CompareTag("WayPoint"))
                                                        .Select(child => child.gameObject)
                                                        .ToArray();

            // Resize the waypointTransforms array to match the number of waypoints found
            moveSpots = new Transform[waypointObjects.Length];

            // Assign positions to the Transform array
            for (int i = 0; i < waypointObjects.Length; i++)
            {
                // Store the transform component of each waypoint GameObject
                moveSpots[i] = waypointObjects[i].transform;
            }
        }

        randomSpot = Random.Range(0, moveSpots.Length);

    }

    void Update()
    {
        float distance = Vector3.Distance(PlayerMovementStage.playerPos, transform.position);
        if (distance <= losRadius)
        {
            CheckLOS();
        }
        else
        {
            playerIsInLOS = false;
            playerAttacking = false;
        }
        if (navEnemy.isActiveAndEnabled)
        {
            if (playerIsInLOS == false && aiMemorizePlayer == false && aiHeardPlayer == false)
            {
                Patrol();
                NoiseCheck();
                StopCoroutine(AiMemory());
            }
            else if (playerIsInLOS == true)
            {
                aiMemorizePlayer = true;
                FacePlayer();
                ChasePlayer();
            }
            else if (aiMemorizePlayer == true && playerIsInLOS == false)
            {
                EnemyLost();
                ChasePlayer();
                StartCoroutine(AiMemory());

            }
            else if (playerIsInLOS == false && aiMemorizePlayer == false && aiHeardPlayer == true)
            {
                canSpin = true;
                GoToNoisePosition();
            }
        }

        PoacherSfx();

    }

    void PoacherSfx()
    {
        if(playerIsInLOS && !isChasing)
        {
            audioSource.PlayOneShot(chasePlayerSfx);
            isChasing = true;
        }
    }

    void EnemyLost()
    {
        if (Vector3.Distance(transform.position, navEnemy.destination) <= 3.5f)
        {
            isChasing = false;
            Debug.Log("Idle");
            enemyAnimator.Play("Idle");
        }
    }

    void ScareSounds()
    {
        // source.PlayOneShot(scareSound);
    }
    void NoiseCheck()
    {
        float distance = Vector3.Distance(PlayerMovementStage.playerPos, transform.position);

        if (distance <= noiseTravelDistance)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                //  EnemyAnimator.Play("Run");
                navEnemy.speed = runSpeed;
                ScareSounds();
                noisePosition = PlayerMovementStage.playerPos;
                aiHeardPlayer = true;
            }
            else
            {
                //wyattAnimator.Play("Walk");
                aiHeardPlayer = false;
                canSpin = false;
            }
        }
    }

    void GoToNoisePosition()
    {
        navEnemy.SetDestination(noisePosition);
        if (Vector3.Distance(transform.position, noisePosition) <= 1f && canSpin == true)
        {
            //  EnemyAnimator.Play("Idle");
            isSpinningTime += Time.deltaTime;
            transform.Rotate(Vector3.up * spinSpeed, Space.World);
            if (isSpinningTime >= spinTime)
            {
                canSpin = false;
                aiHeardPlayer = false;
                isSpinningTime = 0f;
                //  EnemyAnimator.Play("Walk");
                navEnemy.speed = walkSpeed;
            }
        }
    }

    IEnumerator AiMemory()
    {
        increasingMemoryTime = 0;
        while (increasingMemoryTime < memoryStartTime)
        {
            increasingMemoryTime += Time.deltaTime;
            aiMemorizePlayer = true;
            yield return null;
        }
        aiHeardPlayer = false;
        aiMemorizePlayer = false;
    }

    void CheckLOS()
    {
        Vector3 direction = PlayerMovementStage.playerPos - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMask.value;

        if (angle < fieldOfViewAngle * 0.5f)
        {
            if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, losRadius, mask))
            {
                Debug.DrawRay(transform.position, direction.normalized * losRadius, Color.green);
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Hit");
                    playerIsInLOS = true;
                    aiMemorizePlayer = true;
                }
                else
                {
                    playerIsInLOS = false;
                }
            }
        }
    }


    void Patrol()
    {
        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 5f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
                navEnemy.speed = walkSpeed;
            }
            else
            {
                navEnemy.SetDestination(transform.position);
                enemyAnimator.Play("Idle");
                waitTime -= Time.deltaTime;
            }
        }
        else
        {
            navEnemy.SetDestination(moveSpots[randomSpot].position);
            enemyAnimator.Play("Walk");
        }
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(PlayerMovementStage.playerPos, transform.position);

        if (distance <= chaseRadius && distance > distToPlayer && !playerAttacking)
        {
            // Chase the player
            enemyAnimator.Play("Run");
            navEnemy.SetDestination(PlayerMovementStage.playerPos);
            navEnemy.speed = runSpeed;
        }
        else if (distance <= distToPlayer + 0.5f)
        {
            enemyAnimator.Play("Attack");
            navEnemy.SetDestination(transform.position);
            playerAttacking = true;
        }
        else
        {
            playerAttacking = false;
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (PlayerMovementStage.playerPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * facePlayerFactor);
    }

}
