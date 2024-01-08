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
    public bool raidingBase;


    public int defendTimer;

    void Start()
    {
        PlayerPrefs.SetInt("animal", 0);
        PlayerPrefs.SetInt("raid", 0);
        raidingBase = false;
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("animalCounter") > 0 && PlayerPrefs.GetInt("raid") == 0)
        {
            defendTimer = Random.Range(10, 16);
            StartCoroutine(DefendRaid(defendTimer));
        }

        Debug.Log(PlayerPrefs.GetInt("raid"));
        if (PlayerPrefs.GetInt("raid") >= 1 && !raidingBase)
        {
            StartCoroutine(InstantiateObjects());
            raidingBase = true;
        }
    }
    IEnumerator DefendRaid(float timer)
    {
        Debug.LogWarning("Raiding!");
        yield return new WaitForSeconds(timer * 60);
        //show being raided;
        PlayerPrefs.SetInt("raid", 1);
        yield break;
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

        instantiations = 0;
        raidingBase = false;
        PlayerPrefs.SetInt("raid", 0);
        yield break;
        //Debug.Log("Stopped instantiating objects.");
    }

    // You can call this method to stop instantiating objects if needed.
    public void StopInstantiating()
    {
        canInstantiate = false;
    }
}
