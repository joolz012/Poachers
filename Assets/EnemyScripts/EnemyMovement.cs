using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent navMeshAgent;
    private int currentWaypoint = 0;

    private void Start()
    {
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
        // Check if the enemy has reached the current waypoint
        if (navMeshAgent.remainingDistance < 0.1f && !navMeshAgent.pathPending)
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

    private void SetDestination()
    {
        // Set the destination to the current waypoint
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
    }
}
