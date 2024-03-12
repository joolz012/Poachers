using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    private int questCounter;
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
        if(currentAnimal >= 0)
        {
            questTextBox.SetActive(true);
            questTextBox2.SetActive(true);
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
            if (questCounter != 2)
            {
                //animals
                currentAnimal = 0;
                totalAnimal = finalAnimalCost;

                //keys
                currentKey = 0;
            }
            else if(questCounter == 2)
            {
                questTextBox.SetActive(false);
                totalKey = 4;
            }
        }
        else if(questCounter == 3)
        {
            gameObject.SetActive(false);
        }
    }
}
