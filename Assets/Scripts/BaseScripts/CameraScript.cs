using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public float minY = 1f;
    public float maxY = 5f;
    public float minDistance = 1f;
    public float maxDistance = 5f;

    public GameObject player;


    private TransparencyScript raycastedObj;
    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 targetPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        float distance = Mathf.Clamp(Vector3.Distance(targetPosition, transform.position), minDistance, maxDistance);
        transform.position = targetPosition - transform.forward * distance;
    }

    private void Start()
    {
        raycastedObj = null;
    }

    private void Update()
    {

        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                TransparencyScript hitObject = hit.collider.gameObject.GetComponent<TransparencyScript>();

                if (hitObject != null)
                {
                    if (hit.collider.gameObject != player)
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
