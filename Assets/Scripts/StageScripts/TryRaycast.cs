using UnityEngine;

public class TryRaycast : MonoBehaviour
{
    public LayerMask hitLayer;  // Specify the layer(s) to be considered for hit detection
    public string targetTag = "YourTargetTag";  // Specify the tag for the target object

    void Update()
    {
        // Get mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to a ray in world space
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // Define the box dimensions (adjust as needed)
        Vector3 boxSize = new Vector3(1f, 1f, 0.1f);

        // Cast the box along the ray
        RaycastHit hit;
        if (Physics.BoxCast(ray.origin, boxSize / 2, ray.direction, out hit, Quaternion.identity, Mathf.Infinity, hitLayer))
        {
            // Check if the hit object has the specified tag
            if (hit.collider.CompareTag(targetTag))
            {
                // Do something with the hit object (e.g., move it to the mouse position)
                Vector3 newPosition = new Vector3(hit.point.x, hit.point.y, 0f);
                hit.collider.transform.position = newPosition;
            }
        }

        // Draw the ray for visualization purposes
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);
    }
}
