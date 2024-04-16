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
    public Text currentAnimalText, totalAnimalText;
    public float currentAnimal, totalAnimal;

    [Header("Enemies")]
    public GameObject questTextBox2;
    public Text currentEnemyText, totalEnemyText;
    public float currentEnemy, totalEnemy;

    [Header("Boss")]
    public TextMeshProUGUI mainText;
    public GameObject slash;
    public Transform bossFightTrans, playerTrans;
    public CharacterController playerCont;
    GameObject[] enemyGroups, animals;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("ragnarBattle") == 1)
        {
            slash.SetActive(false); 
            currentEnemyText.gameObject.SetActive(false);
            questTextBox.SetActive(false);
            totalEnemyText.gameObject.SetActive(false);

            playerCont.enabled = false; 
            playerTrans.position = bossFightTrans.position;
            playerCont.enabled = true;

            enemyGroups = GameObject.FindGameObjectsWithTag("EnemyGroup");
            animals = GameObject.FindGameObjectsWithTag("Animal");
            foreach (GameObject enemy in enemyGroups)
            {
                enemy.SetActive(false);
            }
            foreach (GameObject animal in animals)
            {
                animal.SetActive(false);
            }

            mainText.text = "Defeat Ragnar";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnimal >= 0 && PlayerPrefs.GetFloat("ragnarBattle") == 0)
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
            //currentEnemy = 0;
            PlayerPrefs.SetFloat("ragnarBattle", 1);
            SceneManager.LoadScene("Base");
        }
    }
}
