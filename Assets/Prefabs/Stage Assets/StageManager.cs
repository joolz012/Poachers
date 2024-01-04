using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int animalCounter;
    public GameObject[] animalsGameObjects;
    public GameObject[] gateGameObjects;


    // Start is called before the first frame update
    void Start()
    {
        if (animalCounter <= 3)
        {
            foreach (GameObject gameObject in animalsGameObjects)
            {
                gameObject.SetActive(false);
            }
            if (animalCounter <= 2)
            {
                animalsGameObjects[2].SetActive(true);

                if (animalCounter <= 1)
                {
                    animalsGameObjects[2].SetActive(true);
                    animalsGameObjects[1].SetActive(true);

                    if (animalCounter <= 0)
                    {
                        foreach (GameObject gameObject in animalsGameObjects)
                        {
                            gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GatePass();

    }


    void GatePass()
    {
        if (!animalsGameObjects[0].activeInHierarchy)
        {
            gateGameObjects[0].SetActive(false);
        }
        //if (!animalsGameObjects[1].activeInHierarchy)
        //{
        //    gateGameObjects[1].SetActive(false);
        //}
        //if (!animalsGameObjects[2].activeInHierarchy)
        //{
        //    gateGameObjects[2].SetActive(false);
        //}
    }
}
