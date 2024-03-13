using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager4 : MonoBehaviour
{
    [Header("Animal 1")]
    public GameObject questTextBox;
    public Text currentAnimalText, totalAnimalText;
    public float currentAnimal, totalAnimal;

    public Transform bossFightTrans, playerTrans;
    public CharacterController playerCont;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("ragnarBattle") == 1)
        {
            currentAnimalText.text = "";
            totalAnimalText.text = "";
            Text mainText = questTextBox.GetComponent<Text>();
            mainText.text = "Defeat Ragnar";

            playerCont.enabled = false; 
            playerTrans.position = bossFightTrans.position;
            playerCont.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnimal >= 0)
        {
            questTextBox.SetActive(true);
            currentAnimalText.text = currentAnimal.ToString();
            totalAnimalText.text = totalAnimal.ToString();
        }

        if(currentAnimal >= totalAnimal)
        {
            PlayerPrefs.SetFloat("ragnarBattle", 1);
            SceneManager.LoadScene("Base");
        }


    }
}
