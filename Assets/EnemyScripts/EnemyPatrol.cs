using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyPatrol : MonoBehaviour
{
    //    public float DeathTimer, Death;
    //    public GameObject Respawn;
    //    private Animator EnemyAnimator;



    //AudioSource source;
    //public AudioClip scareSound;

    //wyatt sight
    public bool playerAttacking = false;
    public bool playerIsInLOS = false;
    public float fieldOfViewAngle = 160f;
    public float losRadius = 20f;

    //wyatt memory
    private bool aiMemorizePlayer = false;
    public float memoryStartTime = 10f;
    private float increasingMemoryTime;

    //wyatt sound
    Vector3 noisePosition;
    private bool aiHeardPlayer = false;
    public float noiseTravelDistance = 50f;
    public float spinSpeed = 2f;
    private bool canSpin = false;
    private float isSpinningTime;
    public float spinTime = 5f;

    //patrol Wyatt
    public Transform[] moveSpots;
    private int randomSpot;

    NavMeshAgent navEnemy;

    public float distToPlayer = 2f;

    private float randomStrafeStartTime;
    private float waitStrafeTime;
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
        Time.timeScale = 1;
        waitTime = startWaitTime;
        GameObject.Find("PlayerStage").GetComponent<PlayerMovement>().enabled = true;
        randomSpot = Random.Range(0, moveSpots.Length);
        //  EnemyAnimator = GetComponent<Animator>();
        //  EnemyAnimator.Play("Walk");
        // source = GetComponent<AudioSource>();

        // Find all child GameObjects with the specified tag
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("WayPoint");

        // Resize the waypointTransforms array to match the number of waypoints found
        moveSpots = new Transform[waypointObjects.Length];

        // Assign positions to the Transform array
        for (int i = 0; i < waypointObjects.Length; i++)
        {
            // Store the transform component of each waypoint GameObject
            moveSpots[i] = waypointObjects[i].transform;
        }
    }

    void Update()
    {


        float distance = Vector3.Distance(PlayerMovement.playerPos, transform.position);
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
            else if (playerIsInLOS == false && aiMemorizePlayer == false && aiHeardPlayer == true)
            {
                canSpin = true;
                GoToNoisePosition();
            }
            else if (playerIsInLOS == true)
            {
                aiMemorizePlayer = true;
                FacePlayer();
                ChasePlayer();
            }
            else if (aiMemorizePlayer == true && playerIsInLOS == false)
            {
                ChasePlayer();
                StartCoroutine(AiMemory());
            }
        }
    }

    void ScareSounds()
    {
        // source.PlayOneShot(scareSound);
    }
    void NoiseCheck()
    {
        float distance = Vector3.Distance(PlayerMovement.playerPos, transform.position);

        if (distance <= noiseTravelDistance)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                //  EnemyAnimator.Play("Run");
                navEnemy.speed = 4.0f;
                ScareSounds();
                noisePosition = PlayerMovement.playerPos;
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
                navEnemy.speed = 2.0f;
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
        Vector3 direction = PlayerMovement.playerPos - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, losRadius))
            {
                if (hit.collider.CompareTag("Player"))
                {
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

        navEnemy.SetDestination(moveSpots[randomSpot].position);
        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 1f)
        {
            // EnemyAnimator.Play("Idle");
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
                // EnemyAnimator.Play("Walk");
                navEnemy.speed = 2.0f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(PlayerMovement.playerPos, transform.position);

        if (distance <= chaseRadius && distance > distToPlayer && !playerAttacking)
        {
            // Chase the player
            navEnemy.SetDestination(PlayerMovement.playerPos);
            navEnemy.speed = 4.0f;
        }
        if (distance <= distToPlayer + 0.5f)
        {
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
        Vector3 direction = (PlayerMovement.playerPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * facePlayerFactor);
    }

}
