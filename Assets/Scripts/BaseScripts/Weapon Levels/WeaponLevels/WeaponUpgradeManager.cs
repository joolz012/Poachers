using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeManager : MonoBehaviour
{
    public WeaponUpgrade[] upgrades; // Assign your upgrades in the inspector
    public int currentUpgradeLevel;
    public string saveWeapon;

    public WeaponManager weaponManager;
    public void Start()
    {
        currentUpgradeLevel = PlayerPrefs.GetInt(saveWeapon);
        WeaponScript weaponScript = GetComponent<WeaponScript>(); 
        if (currentUpgradeLevel < upgrades.Length)
        {
            WeaponUpgrade currentUpgrade = upgrades[currentUpgradeLevel];

            weaponScript.currentLevel = currentUpgrade.level;
            weaponScript.attackDamage = currentUpgrade.damageIncrease;
            weaponScript.attackRange = currentUpgrade.rangeIncrease;
            weaponScript.shotsPerSecond = currentUpgrade.attackSpeedIncrease;
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
            PlayerPrefs.SetInt(saveWeapon, 0);
        }
    }

    public void UpgradeWeapon(WeaponScript weapon)
    {
        if (currentUpgradeLevel < upgrades.Length)
        {
            PlayerPrefs.SetInt(saveWeapon, PlayerPrefs.GetInt(saveWeapon) + 1);
            currentUpgradeLevel++;
            WeaponUpgrade currentUpgrade = upgrades[currentUpgradeLevel];

            weapon.currentLevel = currentUpgrade.level;
            weapon.attackDamage = currentUpgrade.damageIncrease;
            weapon.attackRange = currentUpgrade.rangeIncrease;
            weapon.shotsPerSecond = currentUpgrade.attackSpeedIncrease;

            Debug.Log("Upgraded to level " + currentUpgrade.level);
        }
        else
        {
            Debug.LogWarning("All upgrades applied");
        }
    }

    public void DegradeWeapon(WeaponScript weapon)
    {
        if (currentUpgradeLevel < upgrades.Length)
        {
            PlayerPrefs.SetInt(saveWeapon, currentUpgradeLevel);
            WeaponUpgrade currentUpgrade = upgrades[currentUpgradeLevel];
            Debug.Log("Upgrade level degrade to: " + currentUpgradeLevel);

            weapon.currentLevel = currentUpgrade.level;
            weapon.attackDamage = currentUpgrade.damageIncrease;
            weapon.attackRange = currentUpgrade.rangeIncrease;
            weapon.shotsPerSecond = currentUpgrade.attackSpeedIncrease;

            weaponManager.attackDmg.text = currentUpgrade.damageIncrease.ToString();
            weaponManager.attackRange.text = currentUpgrade.rangeIncrease.ToString();
            weaponManager.attackSpeed.text = currentUpgrade.attackSpeedIncrease.ToString();
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
