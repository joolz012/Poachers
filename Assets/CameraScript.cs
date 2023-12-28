using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;  // Player's transform
    public float smoothSpeed = 0.125f;  // Smoothing factor for camera movement
    public float minY = 2f;  // Minimum height of the camera
    public float maxY = 10f; // Maximum height of the camera
    public float minDistance = 5f;  // Minimum distance from player along the Z-axis
    public float maxDistance = 10f; // Maximum distance from player along the Z-axis

    void LateUpdate()
    {
        if (target == null)
        {
            // Ensure there is a target assigned
            Debug.LogWarning("No target assigned for camera follow!");
            return;
        }

        // Get the target's position
        Vector3 targetPosition = target.position;

        // Smoothly interpolate to the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Set the Y-axis to a fixed value
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        // Calculate the distance along the Z-axis
        float distance = Mathf.Clamp(Vector3.Distance(targetPosition, transform.position), minDistance, maxDistance);

        // Set the new position with the adjusted distance along the Z-axis
        transform.position = targetPosition - transform.forward * distance;
    }
}
