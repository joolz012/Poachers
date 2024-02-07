using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalismanOne : MonoBehaviour
{
    public int currentEssenceCost;//essenceCost PlayerPrefs talismanOneInt
    public int talismanCost;
    public int talismanCostCounter;
    public Text currentEssenceCostText, 
        talismanCostText;//essenceNeeded

    public GameObject askUpgrade;

    public string talismanCurrentEssence;
    public string talismanSave;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt(talismanSave, 0);
        PlayerPrefs.GetInt(talismanCurrentEssence);
        PlayerPrefs.GetInt(talismanSave);
        if(PlayerPrefs.GetInt(talismanSave) == 1)
        {
            talismanCost = 3;
        }

        //Upgrading
        //currentUpgradeLevel = PlayerPrefs.GetInt(saveTalisman);
        //TrapScript trapScript = GetComponent<TrapScript>();
        //if (currentUpgradeLevel < upgrades.Length)
        //{
        //    TalismanUpgrade currentUpgrade = upgrades[currentUpgradeLevel];

        //    trapScript.currentLevel = currentUpgrade.level;
        //    trapScript.trapDmg = currentUpgrade.talismanDmg;
        //    trapScript.trapCooldown = currentUpgrade.talismanCooldown;
        //}
        //else
        //{
        //    Debug.LogWarning("Didn't Set Level");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        currentEssenceCostText.text = currentEssenceCost.ToString();
        talismanCostText.text = talismanCost.ToString();
    }

    public void TalismanDefOne()
    {
        Debug.Log("Click");
        if(PlayerPrefs.GetInt("essence") > 0 && currentEssenceCost < talismanCost)
        {
            EssenceCost();
            if (currentEssenceCost == 5)
            {
                PlayerPrefs.SetInt(talismanSave, 1);
                talismanCost = 3;
                currentEssenceCost = 0;
            }
            else if (currentEssenceCost == talismanCostCounter && PlayerPrefs.GetInt(talismanSave) == 1)
            {
                //upgrade talisman
                askUpgrade.SetActive(false);
            }
        }
    }

    public void UpgradeTalisman()
    {
        currentEssenceCost = 0;
    }

    private void EssenceCost()
    {
        currentEssenceCost++;
        PlayerPrefs.SetInt(talismanCurrentEssence, currentEssenceCost);
        PlayerPrefs.SetInt("essence", PlayerPrefs.GetInt("essence") - 1);
    }

    //Upgrading Code
    //public TalismanUpgrade[] upgrades;
    //public int currentUpgradeLevel;
    //public string saveTalisman;


    //public void UpgradeTrap(TrapScript trap)
    //{
    //    if (currentUpgradeLevel < upgrades.Length)
    //    {
    //        PlayerPrefs.SetInt(saveTalisman, PlayerPrefs.GetInt(saveTalisman) + 1);
    //        currentUpgradeLevel++;
    //        TalismanUpgrade currentUpgrade = upgrades[currentUpgradeLevel];

    //        trap.currentLevel = currentUpgrade.level;
    //        trap.trapDmg = currentUpgrade.talismanDmg;
    //        trap.trapCooldown = currentUpgrade.talismanCooldown;

    //        Debug.Log("Upgraded to level " + currentUpgrade.level);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("All upgrades applied");
    //    }
    //}

}
