using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager4 : MonoBehaviour
{
    [Header("Animal 1")]
    public GameObject questTextBox;
    TextMeshPro mainText;
    public Text currentAnimalText, totalAnimalText;
    public float currentAnimal, totalAnimal;

    [Header("Enemies")]
    public GameObject questTextBox2;
    public Text currentEnemyText, totalEnemyText;
    public float currentEnemy, totalEnemy;

    public Transform bossFightTrans, playerTrans;
    public CharacterController playerCont;
    // Start is called before the first frame update
    void Start()
    {
        mainText = questTextBox.GetComponent<TextMeshPro>();
        if (PlayerPrefs.GetFloat("ragnarBattle") == 1)
        {
            currentAnimalText.text = "";
            totalAnimalText.text = "";

            playerCont.enabled = false; 
            playerTrans.position = bossFightTrans.position;
            playerCont.enabled = true;

            mainText.text = "Defeat Ragnar";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnimal >= 0)
        {
            questTextBox.SetActive(true);
            questTextBox2.SetActive(true);
            currentAnimalText.text = currentAnimal.ToString();
            totalAnimalText.text = totalAnimal.ToString();

            currentEnemyText.text = currentEnemy.ToString();
            totalEnemyText.text = totalEnemy.ToString();
        }


        if (currentAnimal >= totalAnimal && currentEnemy >= totalEnemy)
        {
            //enemy
            currentEnemy = 0;
            PlayerPrefs.SetFloat("ragnarBattle", 1);
            SceneManager.LoadScene("Base");
        }


    }
}
