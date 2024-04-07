using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Enemies")]
    public GameObject questTextBox4;
    public Text currentEnemyText, totalEnemyText;
    public float currentEnemy, totalEnemy;
    [Header("Per Stage Needs")]
    public string animal1;
    public string animal2;

    [Header("QuestCounter")]
    public int questCounter;
    public GameObject[] indicator;

    private bool doOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        questTextBox.SetActive(false);
        questTextBox2.SetActive(false);
        questTextBox3.SetActive(false);
        questTextBox4.SetActive(false);
        gameObject.SetActive(true);
        questCounter = -1;
        currentAnimal = -1;
        currentAnimal2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnimal >= 0 && !doOnce)
        {
            indicator[0].SetActive(true);
            questTextBox.SetActive(true);
            questTextBox3.SetActive(true);
            questTextBox4.SetActive(true);
            questCounter++;
            doOnce = true;
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
        totalKeyText.text = totalKey.ToString();

        currentEnemyText.text = currentEnemy.ToString();
        totalEnemyText.text = totalEnemy.ToString();

        if (questCounter == 0)
        {
            if (currentAnimal >= totalAnimal && currentKey >= totalKey && currentEnemy >= totalEnemy)
            {
                indicator[1].SetActive(true);
                //animals
                currentAnimal = 0;
                totalAnimal = finalAnimalCost;

                //keys
                currentKey = 0;

                //enemy
                currentEnemy = 0;

                questTextBox2.SetActive(true);
                questCounter += 1;
            }
        }
        else if (questCounter == 1)
        {
            if (currentAnimal >= totalAnimal && currentAnimal2 >= totalAnimal2 && currentKey >= totalKey && currentEnemy >= totalEnemy)
            {
                indicator[2].SetActive(true);
                questTextBox.SetActive(false);
                questTextBox2.SetActive(false);

                //keys
                currentKey = 0;
                totalKey = 6;

                //enemy
                currentEnemy = 0;
                questCounter += 1;
            }
        }
        else if (questCounter == 2)
        {
            if (currentKey >= totalKey && currentEnemy >= totalEnemy)
            {
                //enemy
                currentEnemy = 0;

                questCounter += 1;
            }
        }
        else if (questCounter == 3)
        {
            SceneManager.LoadScene("Base");
            gameObject.SetActive(false);
        }
    }
}
