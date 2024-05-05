using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public Transform cameraTrans;
    public float smoothSpeed = 0.125f;
    public float fixedYPosition = 5f;
    public float distanceZ = -10f;
    public int indexManager = 0;

    [Header("Arrays")]
    public Transform[] moveSpots;
    public Transform[] charactersTrans;
    public Transform[] movePlayer;
    public Transform[] moveBubbles;
    public Transform[] moveThorfin;


    private Vector3 offset;

    void Start()
    {
        // Calculate initial offset
        offset = cameraTrans.position - moveSpots[indexManager].position;
    }

    private void Update()
    {
        charactersTrans[0].position = movePlayer[indexManager].position;
        charactersTrans[1].position = moveBubbles[indexManager].position;
        charactersTrans[2].position = moveThorfin[indexManager].position;

        charactersTrans[0].rotation = movePlayer[indexManager].rotation;
        charactersTrans[1].rotation = moveBubbles[indexManager].rotation;
        charactersTrans[2].rotation = moveThorfin[indexManager].rotation;
    }
    void FixedUpdate()
    {
        if (indexManager < moveSpots.Length)
        {
            Vector3 targetPosition = moveSpots[indexManager].position + offset;
            targetPosition.y = fixedYPosition; 
            targetPosition.z += distanceZ; 

            Vector3 smoothedPosition = Vector3.Lerp(cameraTrans.position, targetPosition, smoothSpeed * Time.deltaTime);
            cameraTrans.position = smoothedPosition;
        }
    }

    public void AddIndex()
    {
        indexManager++;
    }
}
