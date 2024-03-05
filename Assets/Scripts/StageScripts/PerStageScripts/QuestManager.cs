using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questTextBox;
    public Text currentAnimalText, totalAnimalText;
    public float currentAnimal, totalAnimal, finalAnimalCost;
    private int questCounter;
    // Start is called before the first frame update
    void Start()
    {
        questTextBox.SetActive(false);
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
            currentAnimalText.text = currentAnimal.ToString();
        }
        else if(currentAnimal < 0)
        {
            currentAnimalText.text = "0";
        }
        totalAnimalText.text = totalAnimal.ToString();

        if (currentAnimal >= totalAnimal && questCounter != 2)
        {
            questCounter += 1;
            currentAnimal = 0;
            totalAnimal = finalAnimalCost;
        }
        else if(questCounter == 2)
        {
            gameObject.SetActive(false);
        }
    }
}
