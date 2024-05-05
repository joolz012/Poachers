using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapUpgradeManager : MonoBehaviour
{
    public TrapUpgrade[] upgrades;
    public int currentUpgradeLevel;
    public string saveTrap;

    public WeaponManager weaponManager;
    public void Start()
    {
        currentUpgradeLevel = PlayerPrefs.GetInt(saveTrap);
        TrapScript trapScript = GetComponent<TrapScript>();
        if (currentUpgradeLevel < upgrades.Length)
        {
            TrapUpgrade currentUpgrade = upgrades[currentUpgradeLevel];

            trapScript.currentLevel = currentUpgrade.level;
            trapScript.trapDmg = currentUpgrade.trapDmg;
            trapScript.trapCooldown = currentUpgrade.trapCooldown;
            trapScript.trapStunDuration = currentUpgrade.trapStunDuration;
        }
        else
        {
            Debug.LogWarning("Didn't Set Level");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.SetInt(saveTrap, 0);
        }
    }

    public void UpgradeTrap(TrapScript trap)
    {
        if (currentUpgradeLevel < upgrades.Length)
        {
            PlayerPrefs.SetInt(saveTrap, PlayerPrefs.GetInt(saveTrap) + 1);
            currentUpgradeLevel++;
            TrapUpgrade currentUpgrade = upgrades[currentUpgradeLevel];

            trap.currentLevel = currentUpgrade.level; 
            trap.currentLevel = currentUpgrade.level;
            trap.trapDmg = currentUpgrade.trapDmg;
            trap.trapCooldown = currentUpgrade.trapCooldown;
            trap.trapStunDuration = currentUpgrade.trapStunDuration;

            Debug.Log("Upgraded to level " + currentUpgrade.level);
        }
        else
        {
            Debug.LogWarning("All upgrades applied");
        }
    }

    public void DegradeWeapon(TrapScript trap)
    {
        if (currentUpgradeLevel < upgrades.Length)
        {
            PlayerPrefs.SetInt(saveTrap, currentUpgradeLevel);
            TrapUpgrade currentUpgrade = upgrades[currentUpgradeLevel];
            Debug.Log("Upgrade level degrade to: " + currentUpgradeLevel);

            trap.currentLevel = currentUpgrade.level;
            trap.trapDmg = currentUpgrade.trapDmg;
            trap.trapCooldown = currentUpgrade.trapCooldown;
            trap.trapStunDuration = currentUpgrade.trapStunDuration;

            weaponManager.trapDmg.text = currentUpgrade.trapDmg.ToString();
            weaponManager.trapCooldown.text = currentUpgrade.trapCooldown.ToString();
            weaponManager.trapStunDuration.text = currentUpgrade.trapStunDuration.ToString();
            Debug.Log("Degraded to level " + currentUpgrade.level);
        }
        else
        {
            Debug.LogWarning("All degrade applied");
        }
    }

    public void DecreaseUpgradeLevel(int decreaseBy)
    {
        currentUpgradeLevel -= decreaseBy;
    }
}
