using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalismanScript : MonoBehaviour
{
    [Header("Essence Spent")]
    public float essenceSpent;
    public string essenceSpentAnimal;
    public string whatAnimal;
    public float maxLevel;
    public string whatAnimalMax;

    [Header("For Talisman Cost Upgrade")]
    public float talismanCost;

    [Header("Tell if Talisan Unlocked")]
    public string talismanSave;
    public float unlockCost;
    public float isUnlocked;
    
    [Header("Text")]
    public Text currentEssenceCostText;
    public Text talismanCostText;

    [Header("For Tamaraw Only")]
    public string talismanDmgSave;
    public string talismanDuration;
    public float talismanDmgIncrease;


    [Header("For Pangolin Only")]
    public string talismanHealthSave;
    public float talismanHealthIncrease;

    [Header("For Haribon Only")]
    public string talismanAtkSave;
    public float talismanAtkIncrease;

    [Header("For Turtle Only")]
    public string talismanHealthSave2;
    public float talismanHealthIncrease2;

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
        if(PlayerPrefs.GetInt(whatAnimal) > 0 && PlayerPrefs.GetInt(whatAnimalMax) < maxLevel)
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
        //attacking
        if(whatAnimal == "tarsier")
        {
            PlayerPrefs.SetFloat(talismanDuration, PlayerPrefs.GetFloat(talismanDuration) + 0.5f);
            Debug.Log("Tarsier Upgraded");
        }

        if (whatAnimal == "haribon")
        {
            PlayerPrefs.SetFloat(talismanAtkSave, PlayerPrefs.GetFloat(talismanAtkSave) + talismanAtkIncrease);

            Debug.Log("Tamaraw Upgraded");
        }

        if (whatAnimal == "turtle")
        {
            PlayerPrefs.SetFloat(talismanHealthSave2, PlayerPrefs.GetFloat(talismanHealthSave2) + talismanHealthIncrease2);

            Debug.Log("Turtle Upgraded");
        }

        //defending
        if (whatAnimal == "crocodile")
        {
            PlayerPrefs.SetFloat(talismanDuration, PlayerPrefs.GetFloat(talismanDuration) + 1);
            Debug.Log("Crocodile Upgraded");
        }

        if (whatAnimal == "tamaraw")
        {
            PlayerPrefs.SetFloat(talismanDuration, PlayerPrefs.GetFloat(talismanDuration) + 1);
            PlayerPrefs.SetFloat(talismanDmgSave, PlayerPrefs.GetFloat(talismanDmgSave) + talismanDmgIncrease);

            Debug.Log("Tamaraw Upgraded");
        }
        if (whatAnimal == "pangolin")
        {
            PlayerPrefs.SetFloat(talismanHealthSave, PlayerPrefs.GetFloat(talismanHealthSave) + talismanHealthIncrease);

            Debug.Log("Pangolin Upgraded");
        }

    }

    private void EssenceCost()
    {
    }
}
