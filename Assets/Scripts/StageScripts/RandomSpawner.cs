using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public string spawnString;
    public List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds((float)Random.Range(0, 5)); 
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag(spawnString);
        foreach (GameObject spawnPoint in spawnPointObjects)
        {
            spawnPoints.Add(spawnPoint.transform);
        }
        SpawnObject();
    }

    void SpawnObject()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No more spawn points available.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Count);
        transform.position = spawnPoints[randomIndex].position;
        Destroy(spawnPoints[randomIndex].gameObject); // Destroy the original GameObject
        spawnPoints.RemoveAt(randomIndex);
    }
}
