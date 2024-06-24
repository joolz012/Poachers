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
    // public LayerMask layerMask;

    // private TransparencyScript raycastedObj;
    void LateUpdate()
    {
        if (playerTarget == null)
        {
            return;
        }

        Vector3 targetPosition = playerTarget.position + offset;
        targetPosition.y = playerTarget.position.y + fixedYPosition;
        targetPosition.z += distanceZ;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }


    private void Start()
    {
        // raycastedObj = null;
    }

    private void Update()
    {
        #region OldCutout
        // if (playerTarget.gameObject != null)
        // {
        //     Vector3 direction = playerTarget.position - transform.position;
        //     Ray ray = new Ray(transform.position, direction);

        //     if (Physics.Raycast(ray, out RaycastHit hit, layerMask))
        //     {
        //         TransparencyScript hitObject = hit.collider.gameObject.GetComponent<TransparencyScript>();

        //         if (hitObject != null)
        //         {
        //             if (hit.collider.gameObject != playerTarget.gameObject)
        //             {
        //                 if (raycastedObj != hitObject)
        //                 {
        //                     Debug.Log("Fade");
        //                     raycastedObj = hitObject;
        //                     raycastedObj.doFade = true;
        //                 }
        //             }
        //             else if (hit.collider.gameObject.tag != playerTarget.tag)
        //             {
        //                 // Object with different tag is hit, reset fading
        //                 if (raycastedObj != null)
        //                 {
        //                     raycastedObj.doFade = false;
        //                     raycastedObj = null;
        //                 }
        //             }
        //         }
        //         else
        //         {
        //             if (raycastedObj != null)
        //             {
        //                 Debug.Log("Not Fade (No TransparencyScript)");
        //                 raycastedObj.doFade = false;
        //                 raycastedObj = null; 
        //             }
        //         }
        //     }
        //     else
        //     {
        //         // Reset raycastedObj and disable fading
        //         if (raycastedObj != null)
        //         {
        //             Debug.Log("Not Fade (Ray did not hit anything)");
        //             raycastedObj.doFade = false;
        //             raycastedObj = null; 
        //         }
        //     }
        // }
        #endregion
    }



}
