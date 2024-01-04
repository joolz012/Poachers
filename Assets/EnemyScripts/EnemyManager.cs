using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject objectToInstantiate;
    public Transform[] spawnPoints;
    public int maxInstantiations;
    private int instantiations = 0;
    private bool canInstantiate = true;
    public float repeatRate;

    void Start()
    {
        StartCoroutine(InstantiateObjects());
    }

    IEnumerator InstantiateObjects()
    {
        while (instantiations < maxInstantiations)
        {
            yield return new WaitForSeconds(repeatRate); // Wait for 4 seconds before the next instantiation.
            if (canInstantiate)
            {
                // Get a random index from the spawnPoints array
                int randomIndex = Random.Range(0, spawnPoints.Length);

                // Get the random spawn point's position
                Vector3 randomPosition = spawnPoints[randomIndex].position;
                Instantiate(objectToInstantiate, spawnPoints[randomIndex].position, transform.rotation);
                instantiations++;
            }

        }

        Debug.Log("Stopped instantiating objects.");
    }

    // You can call this method to stop instantiating objects if needed.
    public void StopInstantiating()
    {
        canInstantiate = false;
    }
}
