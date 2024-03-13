using HUDIndicator;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [Header("Animal")]
    public GameObject questTextBox;
    public Text currentAnimalText, totalAnimalText;
    public float currentAnimal, totalAnimal, finalAnimalCost;

    [Header("Keys")]
    public GameObject questTextBox2;
    public Text currentKeyText, totalKeyText;
    public float currentKey, totalKey;

    public int questCounter;
    private bool doOnce = false;

    public GameObject[] indicator;
    // Start is called before the first frame update
    void Start()
    {
        questTextBox.SetActive(false);
        questTextBox2.SetActive(false);
        gameObject.SetActive(true);
        questCounter = 0;
        currentAnimal = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Animals: " + PlayerPrefs.GetInt("animalCounter"));
        if(currentAnimal >= 0 && !doOnce)
        {
            indicator[0].SetActive(true);
            questTextBox.SetActive(true);
            questTextBox2.SetActive(true);
            doOnce = true;
        }
        else if(currentAnimal < 0)
        {
            currentAnimalText.text = "0";
        }
        currentAnimalText.text = currentAnimal.ToString();
        totalAnimalText.text = totalAnimal.ToString();

        currentKeyText.text = currentKey.ToString();
        totalKeyText.text = totalKey.ToString();

        if (currentAnimal >= totalAnimal && currentKey >= totalKey)
        {
            questCounter += 1;
            if (questCounter == 1)
            {
                indicator[1].SetActive(true);
                //animals
                currentAnimal = 0;
                totalAnimal = finalAnimalCost;

                //keys
                currentKey = 0;
            }
            else if(questCounter == 2)
            {
                indicator[2].SetActive(true);
                questTextBox.SetActive(false);
                //keys
                currentKey = 0;
                totalKey = 4;

            }
        }
        else if(questCounter == 3)
        {
            gameObject.SetActive(false);
        }

        if (currentKey >= 4)
        {
            gameObject.SetActive(false);
            questCounter += 1;
            SceneManager.LoadScene("Base");
        }

    }
}
