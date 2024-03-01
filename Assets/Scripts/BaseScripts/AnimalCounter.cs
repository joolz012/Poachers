using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class AnimalCounter : MonoBehaviour
{
    public List<GameObject> instantiatedAnimals = new List<GameObject>();
    public int animalSpawnerCounter;
    private int animalCounter = 0;
    public Transform[] animalSpawnerTrans;
    private int respawnRandom;

    public GameObject[] animalPrefab; // Assign your animal prefab in the Inspector
    private string whatAnimal;
    public int[] counters = new int[6];

    private void Start()
    {
    }
    private void Update()
    {
        AnimalChecker();

        if (PlayerPrefs.GetInt("animalCounter") <= 0)
        {
            PlayerPrefs.SetInt("raid", 0);
        }
    }

    public void AnimalChecker()
    {
        counters[0] = PlayerPrefs.GetInt("tarsier");
        counters[1] = PlayerPrefs.GetInt("tamaraw");
        //get last animal
        if (instantiatedAnimals.Count > 0)
        {
            GameObject lastGameObject = instantiatedAnimals[instantiatedAnimals.Count - 1];
            whatAnimal = lastGameObject.tag;
            Debug.Log("Last Animal: " + whatAnimal);
        }

        animalSpawnerCounter = PlayerPrefs.GetInt("animalCounter");
        animalCounter = animalSpawnerCounter;

        // Check if animalCounter has increased
        if (animalCounter > instantiatedAnimals.Count)
        {
            // Instantiate new animals
            int numToInstantiate = animalCounter - instantiatedAnimals.Count;

            // Loop through the counters array
            for (int j = 0; j < counters.Length; j++)
            {
                // If the counter is greater than 0, instantiate the corresponding game object
                if (counters[j] != 0)
                {
                    for (int k = 0; k < counters[j]; k++)
                    {
                        respawnRandom = Random.Range(0, animalSpawnerTrans.Length);
                        GameObject newAnimal = Instantiate(animalPrefab[j], animalSpawnerTrans[respawnRandom].position, Quaternion.identity);
                        instantiatedAnimals.Add(newAnimal);
                    }
                }
            }
        }
        // Check if animalCounter has decreased
        else if (animalCounter < instantiatedAnimals.Count)
        {
            // Remove excess animals
            int numToRemove = instantiatedAnimals.Count - animalCounter;
            for (int i = 0; i < numToRemove; i++)
            {
                GameObject removedAnimal = instantiatedAnimals[instantiatedAnimals.Count - 1];
                instantiatedAnimals.RemoveAt(instantiatedAnimals.Count - 1);
                PlayerPrefs.SetInt(whatAnimal, PlayerPrefs.GetInt(whatAnimal) - 1);
                Destroy(removedAnimal);
            }
        }
    }

    // You can call this method to update the animalCounter value
    public void SetAnimalCounter(int newValue)
    {
        animalCounter = newValue;
    }

    public void StageOne()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void StageTwo()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void StageThree()
    {
        SceneManager.LoadScene("Stage3");
    }
}
