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
    private int questCounter;
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
            currentAnimalText.text = currentAnimal.ToString();
            if (currentAnimal >= 10)
            {
                questTextBox2.SetActive(true);
                currentAnimalText2.text = currentAnimal.ToString();
            }
        }
        else if (currentAnimal < 0)
        {
            currentAnimalText.text = "0";
        }
        totalAnimalText.text = totalAnimal.ToString();
        totalAnimalText2.text = totalAnimal.ToString();

        if (currentAnimal >= totalAnimal && questCounter != 2)
        {
            questCounter += 1;
            currentAnimal = 0;
            totalAnimal = finalAnimalCost;
        }
        else if (questCounter == 2)
        {
            gameObject.SetActive(false);
        }
    }
}
