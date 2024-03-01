using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questTextBox;
    public Text currentTarsierText, totalTarsierText;
    public float currentTarsier, totalTarsier, finalTarsierCost;
    private int questCounter;
    // Start is called before the first frame update
    void Start()
    {
        questTextBox.SetActive(false);
        gameObject.SetActive(true);
        questCounter = 0;
        currentTarsier = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarsier >= 0)
        {
            questTextBox.SetActive(true);
            currentTarsierText.text = currentTarsier.ToString();
        }
        else if(currentTarsier < 0)
        {
            currentTarsierText.text = "0";
        }
        totalTarsierText.text = totalTarsier.ToString();

        if (currentTarsier >= totalTarsier && questCounter != 2)
        {
            questCounter += 1;
            currentTarsier = 0;
            totalTarsier = finalTarsierCost;
        }
        else if(questCounter == 2)
        {
            gameObject.SetActive(false);
        }
    }
}
