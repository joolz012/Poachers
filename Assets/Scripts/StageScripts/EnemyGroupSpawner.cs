using UnityEngine;

public class EnemyGroupSpawner : MonoBehaviour
{
    public GameObject[] waypointPrefabs; // Array of waypoint prefabs
    public Transform[] spawnPositions;
    public Transform parentTransform;

    public GameObject enemyPrefab;
    public int numberOfEnemiesToSpawn;
    public string tagToCheck;

    private bool hasSpawned = false;

    private void Start()
    {
        //InstantiateWaypoints();
    }

    //void InstantiateWaypoints()
    //{
    //    int waypointsToInstantiate = Mathf.Min(waypointPrefabs.Length, spawnPositions.Length);

    //    for (int i = 0; i < waypointsToInstantiate; i++)
    //    {
    //        GameObject waypointGo = Instantiate(waypointPrefabs[i], parentTransform);

    //        waypointGo.transform.localPosition = spawnPositions[i].position - parentTransform.position;
    //    }
    //}

    void Update()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        if (!hasSpawned)
        {
            ShuffleArray(spawnPositions);

            int objectsToSpawn = Mathf.Min(numberOfEnemiesToSpawn, spawnPositions.Length);

            for (int i = 0; i < objectsToSpawn; i++)
            {
                GameObject spawnedObject = Instantiate(enemyPrefab, spawnPositions[i].position, Quaternion.identity);
                spawnedObject.transform.SetParent(parentTransform); // Set the parent
            }

            hasSpawned = true;
        }

        //InvokeRepeating(nameof(CheckAndSpawn), 0f, 1f); // Check and spawn every second
    }

    void CheckAndSpawn()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagToCheck);

        if (taggedObjects.Length == 0)
        {
            hasSpawned = false;
            SpawnObjects();
        }
    }

    void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
