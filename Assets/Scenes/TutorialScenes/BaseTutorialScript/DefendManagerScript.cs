using UnityEngine;

public class DefendManagerScript : MonoBehaviour
{
    public Transform cameraTrans;
    public float smoothSpeed;
    public float fixedYPosition ;
    public float distanceZ;
    public int indexManager;

    [Header("Arrays")]
    public Transform[] moveSpots;


    private Vector3 offset;

    void Start()
    {
        // Calculate initial offset
        offset = cameraTrans.position - moveSpots[indexManager].position;
    }

    private void Update()
    {

    }
    void FixedUpdate()
    {
        if (indexManager < moveSpots.Length)
        {
            // Calculate target position
            Vector3 targetPosition = moveSpots[indexManager].position + offset;
            targetPosition.x = moveSpots[indexManager].position.x;
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
