using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Enemies")]
    public GameObject questTextBox5;
    public Text currentEnemyText, totalEnemyText;
    public float currentEnemy, totalEnemy;

    [Header("Per Stage Needs")]
    public string animal1;
    public string animal2;
    public string animal3;

    [Header("QuestCounter")]
    public int questCounter;

    public TextMeshProUGUI mainText;
    public GameObject slash;
    public GameObject firstAnimal;
    public Transform bossFightTrans, playerTrans;
    public CharacterController playerCont;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("bjornBattle") == 1)
        {
            firstAnimal.SetActive(false);
            questTextBox.SetActive(false); 
            questTextBox2.SetActive(false);
            questTextBox3.SetActive(false);
            questTextBox4.SetActive(false);
            questTextBox5.SetActive(true);
            slash.SetActive(false);
            currentEnemyText.gameObject.SetActive(false);
            totalEnemyText.gameObject.SetActive(false);

            playerCont.enabled = false;
            playerTrans.position = bossFightTrans.position;
            playerCont.enabled = true;
            questCounter = 0;
            currentAnimal = -1;
            currentAnimal2 = 0;
            currentAnimal3 = 0;
            mainText.text = "Defeat Ragnar";
        }
        else if (PlayerPrefs.GetFloat("bjornBattle") == 0)
        {
            questTextBox.SetActive(false);
            questTextBox2.SetActive(false);
            questTextBox3.SetActive(false);
            questTextBox4.SetActive(false);
            questTextBox5.SetActive(false);
            gameObject.SetActive(true);
            questCounter = 0;
            currentAnimal = -1;
            currentAnimal2 = 0;
            currentAnimal3 = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnimal >= 0)
        {
            questTextBox.SetActive(true);
            questTextBox2.SetActive(true);
            questTextBox4.SetActive(true);
            questTextBox5.SetActive(true);
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

        currentKeyText.text = currentKey.ToString();
        totalKeyText.text = totalKeyText.ToString();

        currentEnemyText.text = currentEnemy.ToString();
        totalEnemyText.text = totalEnemy.ToString();

        if (questCounter == 0)
        {
            if(currentAnimal >= totalAnimal && currentAnimal2 >= totalAnimal2 && currentKey >= totalKey && currentEnemy >= totalEnemy)
            {
                questTextBox.SetActive(false);
                questTextBox2.SetActive(false);
                questTextBox3.SetActive(true);

                currentKey = 0;

                //enemy
                currentEnemy = 0;

                questCounter += 1;
            }
        }
        else if (questCounter == 1)
        {
            if (currentAnimal3 >= totalAnimal3 && currentKey >= totalKey && currentEnemy >= totalEnemy)
            {
                questTextBox3.SetActive(false);
                currentKey = 0;
                totalKey = 8;

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
            gameObject.SetActive(false);
        }
    }
}
