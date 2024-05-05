using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalCounter : MonoBehaviour
{
    public List<GameObject> instantiatedAnimals = new List<GameObject>();
    public int animalSpawnerCounter;
    private int animalCounter = 0;
    public Transform[] animalSpawnerTrans;
    private int respawnRandom;

    public GameObject[] animalPrefab;
    private string whatAnimal;
    public int[] counters = new int[6];

    private bool doOnce = false;


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

        if(Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.SetInt("aninmalCounter", PlayerPrefs.GetInt("animalCounter") - 1);
        }
    }

    public void AnimalChecker()
    {
        counters[0] = PlayerPrefs.GetInt("tarsier");
        counters[1] = PlayerPrefs.GetInt("tamaraw");
        counters[2] = PlayerPrefs.GetInt("haribon");
        counters[3] = PlayerPrefs.GetInt("turtle");
        counters[4] = PlayerPrefs.GetInt("crocodile");
        counters[5] = PlayerPrefs.GetInt("pangolin");

        animalSpawnerCounter = PlayerPrefs.GetInt("animalCounter");
        animalCounter = animalSpawnerCounter;


        //get last animal
        if (instantiatedAnimals.Count > 0)
        {
            GameObject lastGameObject = instantiatedAnimals[instantiatedAnimals.Count - 1];
            whatAnimal = lastGameObject.tag;
            Debug.Log("Last Animal: " + whatAnimal);
        }


        //check if animalCounter has increased
        if (animalCounter > instantiatedAnimals.Count)
        {
            // Instantiate new animals
            //int numToInstantiate = animalCounter - instantiatedAnimals.Count;

            for (int j = 0; j < counters.Length; j++)
            {
                if (counters[j] != 0)
                {
                    for (int k = 0; k < counters[j]; k++)
                    {
                        respawnRandom = Random.Range(0, animalSpawnerTrans.Length);
                        GameObject newAnimal = Instantiate(animalPrefab[j], animalSpawnerTrans[respawnRandom].position, transform.rotation);
                        instantiatedAnimals.Add(newAnimal);
                    }
                }
            }
        }
        else if (animalCounter < instantiatedAnimals.Count)
        {
            if (instantiatedAnimals.Count > 0)
            {
                doOnce = true;
            }
        }

        if (doOnce)
        {
            GameObject removedAnimal = instantiatedAnimals[instantiatedAnimals.Count - 1];
            int removedAnimalIndex = -1;
            for (int i = 0; i < counters.Length; i++)
            {
                if (counters[i] > 0 && removedAnimal.tag == animalPrefab[i].tag)
                {
                    removedAnimalIndex = i;
                    break;
                }
            }

            if (removedAnimalIndex >= 0)
            {
                counters[removedAnimalIndex]--;
                PlayerPrefs.SetInt(animalPrefab[removedAnimalIndex].tag, counters[removedAnimalIndex]);
            }

            instantiatedAnimals.RemoveAt(instantiatedAnimals.Count - 1);
            // Destroy the removed animal
            Destroy(removedAnimal);

            doOnce = false;
        }
    }

    public void SetAnimalCounter(int newValue)
    {
        animalCounter = newValue;
    }

    public void DecreaseAnimal()
    {
        PlayerPrefs.SetInt("animalCounter", PlayerPrefs.GetInt("animalCounter") - 1);

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
