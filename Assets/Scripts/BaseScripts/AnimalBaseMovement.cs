using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalBaseMovement : MonoBehaviour
{
    public Transform[] waypoints;
    NavMeshAgent navEnemy;
    private int randomSpot;
    private float waitTime;
    public float startWaitTime = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        navEnemy = GetComponent<NavMeshAgent>();
        navEnemy.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
    void Patrol()
    {
        navEnemy.SetDestination(waypoints[randomSpot].position);
        if (Vector3.Distance(transform.position, waypoints[randomSpot].position) < 1f)
        {
            // EnemyAnimator.Play("Idle");
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, waypoints.Length);
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
}
