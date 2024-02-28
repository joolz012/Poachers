using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalCounter : MonoBehaviour
{
    public GameObject animalPrefab; // Assign your animal prefab in the Inspector
    private List<GameObject> instantiatedAnimals = new List<GameObject>();
    public int animalSpawnerCounter;
    private int animalCounter = 0;
    public Transform[] animalSpawnerTrans;
    private int respawnRandom;

    private void Start()
    {
    }
    void Update()
    {
        AnimalChecker();

        if (PlayerPrefs.GetInt("animalCounter") <= 0)
        {
            PlayerPrefs.SetInt("raid", 0);
        }
    }

    public void AnimalChecker()
    {
        animalSpawnerCounter = PlayerPrefs.GetInt("animalCounter");
        animalCounter = animalSpawnerCounter;
        // Check if animalCounter has increased
        if (animalCounter > instantiatedAnimals.Count)
        {
            // Instantiate new animals
            int numToInstantiate = animalCounter - instantiatedAnimals.Count;
            for (int i = 0; i < numToInstantiate; i++)
            {
                respawnRandom = Random.Range(0, animalSpawnerTrans.Length);
                GameObject newAnimal = Instantiate(animalPrefab, animalSpawnerTrans[respawnRandom].position, Quaternion.identity);
                instantiatedAnimals.Add(newAnimal);
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
