using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager4 : MonoBehaviour
{
    [Header("Animal 1")]
    public GameObject questTextBox;
    public Text currentAnimalText, totalAnimalText;
    public float currentAnimal, totalAnimal;

    // Start is called before the first frame update
    void Start()
    {
        
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

        if(currentAnimal >= 10)
        {
            currentAnimalText.text = "";
            totalAnimalText.text = "";
            Text mainText = questTextBox.GetComponent<Text>();
            mainText.text = "Defeat Ragnar";
        }
    }
}
