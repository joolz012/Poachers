using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager3 : MonoBehaviour
{
    [Header("Animal 1")]
    public GameObject questTextBox;
    public Text currentAnimalText, totalAnimalText;
    public float currentAnimal, totalAnimal, finalAnimalCost;

    [Header("Animal 2")]
    public GameObject questTextBox2;
    public Text currentAnimalText2, totalAnimalText2;
    public float currentAnimal2, totalAnimal2, finalAnimalCost2;

    [Header("Animal 3")]
    public GameObject questTextBox3;
    public Text currentAnimalText3, totalAnimalText3;
    public float currentAnimal3, totalAnimal3, finalAnimalCost3;


    [Header("Keys")]
    public GameObject questTextBox4;
    public Text currentKeyText, totalKeyText;
    public float currentKey, totalKey;

    [Header("Per Stage Needs")]
    public string animal1;
    public string animal2;
    public string animal3;

    [Header("QuestCounter")]
    public int questCounter;
    // Start is called before the first frame update
    void Start()
    {
        questTextBox.SetActive(false);
        questTextBox2.SetActive(false);
        questTextBox3.SetActive(false);
        gameObject.SetActive(true);
        questCounter = 0;
        currentAnimal = -1;
        currentAnimal2 = 0;
        currentAnimal3 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnimal >= 0)
        {
            questTextBox.SetActive(true);
            questTextBox2.SetActive(true);
        }
        else if (currentAnimal < 0)
        {
            currentAnimalText.text = "0";
        }

        currentAnimalText.text = currentAnimal.ToString();
        totalAnimalText.text = totalAnimal.ToString();

        currentAnimalText2.text = currentAnimal2.ToString();
        totalAnimalText2.text = totalAnimal2.ToString();

        currentAnimalText3.text = currentAnimal3.ToString();
        totalAnimalText3.text = totalAnimal3.ToString();

        if (questCounter == 0)
        {
            if(currentAnimal >= totalAnimal && currentAnimal2 >= totalAnimal2)
            {
                questTextBox.SetActive(false);
                questTextBox2.SetActive(false);
                questTextBox3.SetActive(true);

                currentKey = 0;
                questCounter += 1;
            }
        }
        else if (questCounter == 1)
        {
            if (currentAnimal3 >= totalAnimal3)
            {
                questTextBox3.SetActive(false);
                currentKey = 0;
                totalKey = 8;
                questCounter += 1;
            }
        }
        else if (questCounter == 2)
        {
            if (currentKey >= totalKey)
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