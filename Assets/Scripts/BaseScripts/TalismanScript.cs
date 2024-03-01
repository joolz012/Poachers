using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalismanScript : MonoBehaviour
{
    [Header("Essence Spent")]
    public int essenceSpent;
    public string essenceSpentAnimal;
    public string talismanDuration;
    public string whatAnimal;

    [Header("For Talisman Cost Upgrade")]
    public int talismanCost;

    [Header("Tell if Talisan Unlocked")]
    public string talismanSave;
    public int unlockCost;
    public int isUnlocked;
    
    [Header("Text")]
    public Text currentEssenceCostText;
    public Text talismanCostText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt(talismanSave);

        if(isUnlocked == 1)
        {
            PlayerPrefs.SetInt(talismanSave, 1);

        }
    }

    // Update is called once per frame
    void Update()
    {
        essenceSpent = PlayerPrefs.GetInt(essenceSpentAnimal);

        currentEssenceCostText.text = essenceSpent.ToString();
        talismanCostText.text = talismanCost.ToString();
    }

    public void TalismanDefOne()
    {
        Debug.Log("Click");
        if(PlayerPrefs.GetInt(whatAnimal) > 0)
        {
            PlayerPrefs.SetInt(whatAnimal, PlayerPrefs.GetInt(whatAnimal) - 1);
            PlayerPrefs.SetInt(essenceSpentAnimal, PlayerPrefs.GetInt(essenceSpentAnimal) + 1);
            if (PlayerPrefs.GetInt(talismanSave) == 1)
            {
                if (PlayerPrefs.GetInt(essenceSpentAnimal) >= talismanCost)
                {
                    UpgradeTalisman();
                    essenceSpent = 0;
                }
            }
            else if(PlayerPrefs.GetInt(talismanSave) == 0)
            {
                if (essenceSpent >= unlockCost)
                {
                    essenceSpent = 0;
                    PlayerPrefs.SetInt(talismanSave, 1);
                }
            }
        }
    }

    public void UpgradeTalisman()
    {
        if(whatAnimal == "tarsier")
        {
            PlayerPrefs.SetFloat(talismanDuration, PlayerPrefs.GetFloat(talismanDuration) + 0.5f);
            Debug.Log("Tarsier Upgraded");
        }
        if (whatAnimal == "tamaraw")
        {

        }

        if (whatAnimal == "crocodile")
        {
            PlayerPrefs.SetFloat(talismanDuration, PlayerPrefs.GetFloat(talismanDuration) + 1);
            Debug.Log("Crocodile Upgraded");
        }

    }

    private void EssenceCost()
    {
    }
}
