using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerTarget;
    public float smoothSpeed = 0.125f;
    public float fixedYPosition;
    public float distanceZ;
    private Vector3 offset;


    private TransparencyScript raycastedObj;
    void LateUpdate()
    {
        if (playerTarget == null)
        {
            return;
        }

        Vector3 targetPosition = playerTarget.position + offset;
        targetPosition.y = fixedYPosition; // Keep Y position fixed
        targetPosition.z += distanceZ; // Adjust Z position

        // Use Lerp to smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }


    private void Start()
    {
        raycastedObj = null;
    }

    private void Update()
    {

        if (playerTarget.gameObject != null)
        {
            Vector3 direction = playerTarget.position - transform.position;
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                TransparencyScript hitObject = hit.collider.gameObject.GetComponent<TransparencyScript>();

                if (hitObject != null)
                {
                    if (hit.collider.gameObject != playerTarget.gameObject)
                    {
                        // Other object is hit, enable fading
                        if (raycastedObj != hitObject)
                        {
                            raycastedObj = hitObject;
                            raycastedObj.doFade = true;
                        }
                    }
                    else
                    {
                        // Player is hit, disable fading
                        if (raycastedObj != null)
                        {
                            raycastedObj.doFade = false;
                            raycastedObj = null; // Reset the raycastedObj
                        }
                    }
                }
                else
                {
                    // Reset raycastedObj and disable fading
                    if (raycastedObj != null)
                    {
                        Debug.Log("Not Fade (No TransparencyScript)");
                        raycastedObj.doFade = false;
                        raycastedObj = null; // Reset the raycastedObj
                    }
                }
            }
            else
            {
                // Reset raycastedObj and disable fading
                if (raycastedObj != null)
                {
                    Debug.Log("Not Fade (Ray did not hit anything)");
                    raycastedObj.doFade = false;
                    raycastedObj = null; // Reset the raycastedObj
                }
            }
        }
    }



}
