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
            currentAnimalText.text = currentAnimal.ToString();

            questTextBox2.SetActive(true);
            currentAnimalText2.text = currentAnimal2.ToString();

            if (currentAnimal >= 7 && currentAnimal2 >= 5)
            {
                questTextBox.SetActive(false);
                questTextBox2.SetActive(false);
                questTextBox3.SetActive(true);
                currentAnimalText3.text = currentAnimal3.ToString();
            }
        }
        else if (currentAnimal < 0)
        {
            currentAnimalText.text = "0";
        }
        totalAnimalText.text = totalAnimal.ToString();
        totalAnimalText2.text = totalAnimal2.ToString();
        totalAnimalText3.text = totalAnimal3.ToString();

        if (questCounter == 0)
        {
            if(currentAnimal >= totalAnimal && currentAnimal2 >= totalAnimal2)
            {
                questCounter += 1;
            }
        }
        else if (questCounter == 1)
        {
            if (currentAnimal3 >= totalAnimal3)
            {
                questCounter += 1;
            }
        }
        else if (questCounter == 2)
        {
            gameObject.SetActive(false);
        }
    }
}
