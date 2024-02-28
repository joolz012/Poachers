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
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, waypoints.Length);
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
        if (Vector3.Distance(transform.position, waypoints[randomSpot].position) < 1.5f)
        {
            // EnemyAnimator.Play("Idle");
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, waypoints.Length);
                waitTime = startWaitTime;
                navEnemy.speed = 2.0f;
                // EnemyAnimator.Play("Walk");
            }
                Debug.Log("waiting");
                waitTime -= Time.deltaTime;
        }
    }
}
