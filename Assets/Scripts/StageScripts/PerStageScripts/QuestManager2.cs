using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager2 : MonoBehaviour
{
    [Header("Animal 1")]
    public GameObject questTextBox;
    public Text currentAnimalText, totalAnimalText;
    public float currentAnimal, totalAnimal, finalAnimalCost;

    [Header("Animal 2")]
    public GameObject questTextBox2;
    public Text currentAnimalText2, totalAnimalText2;
    public float currentAnimal2, totalAnimal2, finalAnimalCost2;

    [Header("Keys")]
    public GameObject questTextBox3;
    public Text currentKeyText, totalKeyText;
    public float currentKey, totalKey;

    [Header("Per Stage Needs")]
    public string animal1;
    public string animal2;

    [Header("QuestCounter")]
    public int questCounter;
    // Start is called before the first frame update
    void Start()
    {
        questTextBox.SetActive(false);
        questTextBox2.SetActive(false);
        gameObject.SetActive(true);
        questCounter = 0;
        currentAnimal = -1;
        currentAnimal2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnimal >= 0)
        {
            questTextBox.SetActive(true);
        }
        else if (currentAnimal < 0)
        {
            currentAnimalText.text = "0";
        }

        currentAnimalText.text = currentAnimal.ToString();
        totalAnimalText.text = totalAnimal.ToString();

        currentAnimalText2.text = currentAnimal2.ToString();
        totalAnimalText2.text = totalAnimal2.ToString();

        currentKeyText.text = currentKey.ToString();
        totalKeyText.text = totalKeyText.ToString();

        if (questCounter == 0)
        {
            if (currentAnimal >= totalAnimal && currentKey >= totalKey)
            {
                //animals
                currentAnimal = 0;
                totalAnimal = finalAnimalCost;

                //keys
                currentKey = 0;

                questTextBox2.SetActive(true);
                questCounter += 1;
            }
        }
        else if (questCounter == 1)
        { 
            if (currentAnimal >= totalAnimal && currentAnimal2 >= totalAnimal2 && currentKey >= totalKey)
            {
                questTextBox.SetActive(false);
                questTextBox2.SetActive(false);
                totalKey = 6;
                questCounter += 1;
            }
        }
        else if (questCounter == 2)
        {
            if(currentKey >= totalKey)
            {
                questCounter += 1;
            }
        }
        else if (questCounter == 3)
        {
            gameObject.SetActive(false);
        }
    }
}
