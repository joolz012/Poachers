using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialogManager : MonoBehaviour
{
    //fdfa
    public Transform cameraTrans;
    public float smoothSpeed;
    public float fixedYPosition;
    public float distanceZ;
    public int indexManager = 0;

    [Header("Arrays")]
    public Transform[] moveSpots;
    public Transform[] charactersTrans;
    public Transform[] movePlayer;
    public Transform[] moveThorfin;


    private Vector3 offset;

    void Start()
    {
        // Calculate initial offset
        //offset = cameraTrans.position - moveSpots[indexManager].position;
    }

    private void Update()
    {
        charactersTrans[0].position = movePlayer[indexManager].position;
        charactersTrans[1].position = moveThorfin[indexManager].position;

        charactersTrans[0].rotation = movePlayer[indexManager].rotation;
        charactersTrans[1].rotation = moveThorfin[indexManager].rotation;
    }
    void FixedUpdate()
    {
        if (indexManager < moveSpots.Length)
        {
            // Calculate target position
            Vector3 targetPosition = moveSpots[indexManager].position + offset;
            targetPosition.y = fixedYPosition; // Keep Y position fixed
            targetPosition.z += distanceZ; // Adjust Z position

            // Use Lerp to smoothly move the camera towards the target position
            Vector3 smoothedPosition = Vector3.Lerp(cameraTrans.position, targetPosition, smoothSpeed * Time.deltaTime);
            cameraTrans.position = smoothedPosition;
        }
    }

    public void AddIndex()
    {
        indexManager++;
    }
}
