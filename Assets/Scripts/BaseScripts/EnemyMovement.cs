using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Animator enemyAnimator;
    public Transform[] waypoints;
    public NavMeshAgent navMeshAgent;
    public float enemyMovementSpeed;
    private int currentWaypoint = 0;

    public float stoppingRange;
    public bool attackingBase;
    public bool stunned = false;

    private void Start()
    {
        attackingBase = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the enemy GameObject.");
            enabled = false; // Disable the script if NavMeshAgent is not found.
        }
        else
        {
            SetDestination();
        }
    }

    private void Update()
    {
        if(!stunned)
        {
            MoveEnemy();
        }
    }

    private void MoveEnemy()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            stunned = true;
            StunEnemy(2);
        }
        float distance = Vector3.Distance(waypoints[0].position, transform.position);
        if (distance <= stoppingRange)
        {
            navMeshAgent.SetDestination(transform.position);
            attackingBase = true;
        }
        if (navMeshAgent.remainingDistance < 0.1f && !navMeshAgent.pathPending && !attackingBase)
        {
            currentWaypoint++;

            // Check if the enemy has completed the path
            if (currentWaypoint >= waypoints.Length)
            {
                // The enemy has reached the end of the path. Implement staying logic here.
                enabled = false; // Disable the script or add staying logic here.
            }
            else
            {
                SetDestination();
            }
        }
    }
    public void SetDestination()
    {
        enemyAnimator.Play("Walk");
        // Set the destination to the current waypoint
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
    }

    public void StunEnemy(float stunDuration)
    {
        if (stunned)
        {
            enemyAnimator.Play("Idle");
            gameObject.GetComponent<EnemyAttackBase>().enabled = false;
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
            navMeshAgent.acceleration = 25;
            stunned = false;

            // Invoke a method to remove the stun after a specified duration
            Invoke(nameof(RemoveStun), stunDuration);
        }
    }

    private void RemoveStun()
    {
        // Resume the NavMeshAgent movement and set stunned to false
        gameObject.GetComponent<EnemyAttackBase>().enabled = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = enemyMovementSpeed;
        navMeshAgent.acceleration = 8;
        SetDestination();
    }

}
