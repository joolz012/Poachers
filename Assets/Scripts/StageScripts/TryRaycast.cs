using UnityEngine;

public class TryRaycast : MonoBehaviour
{
    public LayerMask hitLayer; 
    public string targetTag = "YourTargetTag";

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        Vector3 boxSize = new Vector3(1f, 1f, 0.1f);

        RaycastHit hit;
        if (Physics.BoxCast(ray.origin, boxSize / 2, ray.direction, out hit, Quaternion.identity, Mathf.Infinity, hitLayer))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                Vector3 newPosition = new Vector3(hit.point.x, hit.point.y, 0f);
                hit.collider.transform.position = newPosition;
            }
        }

        // Draw the ray for visualization purposes
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);
    }
}
