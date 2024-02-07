using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    public Slider healthslider;
    public int baseLevel;
    public float baseMaxHealth;
    public float baseHealth;
    public Transform healthbar, isoCam;

    public EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        baseHealth = baseMaxHealth;
        healthslider.value = baseHealth;
        healthslider.maxValue = baseMaxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        CheatCodes();
        healthslider.value = baseHealth;
        healthbar.LookAt(isoCam.position);
        if (baseHealth <= 0 && PlayerPrefs.GetInt("animalCounter") > 0)
        {
            PlayerPrefs.SetInt("animalCounter", PlayerPrefs.GetInt("animalCounter") - 1);
            baseHealth = baseMaxHealth;
        }
        else if(baseHealth <= 0 && PlayerPrefs.GetInt("animalCounter") <= 0)
        {
            DegradeBase();
            DegradeWeaponBase();
            DegradeWeaponTrap();
            RemoveAllEnemy();
            baseHealth = baseMaxHealth;
        }

    }

    private void DegradeBase()
    {
        BaseHealth baseHealth = GetComponent<BaseHealth>();
        BaseUpgrade baseUpgrade = GetComponent<BaseUpgrade>(); 
        if (baseUpgrade != null)
        {
            if (baseUpgrade.currentUpgradeLevel >= 2)
            {
                baseUpgrade.currentUpgradeLevel -= 2;
                baseUpgrade.DegradeWeapon(baseHealth);
            }
            if (baseUpgrade.currentUpgradeLevel == 1)
            {
                baseUpgrade.currentUpgradeLevel -= 1;
                baseUpgrade.DegradeWeapon(baseHealth);
            }
            PlayerPrefs.SetInt("baseLevel", 0);
        }
        else
        {
            Debug.LogWarning("WeaponUpgradeManager or WeaponScript component not found in object with tag " + gameObject.tag);
        }
    }

    private void DegradeWeaponBase()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");

        foreach (GameObject weaponObject in weapons)
        {
            WeaponUpgradeManager weaponUpgrade = weaponObject.GetComponent<WeaponUpgradeManager>();
            WeaponScript weaponDegrade = weaponObject.GetComponent<WeaponScript>();

            if (weaponUpgrade != null && weaponDegrade != null)
            {
                if (weaponUpgrade.currentUpgradeLevel >= 2)
                {
                    weaponUpgrade.currentUpgradeLevel -= 2;
                    weaponUpgrade.DegradeWeapon(weaponDegrade);
                }
                if (weaponUpgrade.currentUpgradeLevel == 1)
                {
                    weaponUpgrade.currentUpgradeLevel -= 1;
                    weaponUpgrade.DegradeWeapon(weaponDegrade);
                }
            }
            else
            {
                Debug.LogWarning("WeaponUpgradeManager or WeaponScript component not found in object with tag " + weaponObject.tag);
            }
        }
    }

    private void DegradeWeaponTrap()
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");

        foreach (GameObject trapsObject in traps)
        {
            TrapUpgradeManager trapUpgradeManager = trapsObject.GetComponent<TrapUpgradeManager>();
            TrapScript trapDegrade = trapsObject.GetComponent<TrapScript>();

            if (trapUpgradeManager != null && trapDegrade != null)
            {
                if (trapUpgradeManager.currentUpgradeLevel >= 2)
                {
                    trapUpgradeManager.currentUpgradeLevel -= 2;
                    trapUpgradeManager.DegradeWeapon(trapDegrade);
                }
                if (trapUpgradeManager.currentUpgradeLevel == 1)
                {
                    trapUpgradeManager.currentUpgradeLevel -= 1;
                    trapUpgradeManager.DegradeWeapon(trapDegrade);
                }
            }
            else
            {
                Debug.LogWarning("WeaponUpgradeManager or WeaponScript component not found in object with tag " + trapsObject.tag);
            }
        }
    }

    private void RemoveAllEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObject in enemies)
        {
            Destroy(enemyObject);
        }
        enemyManager.StopInstantiating();
        enemyManager.raidingBase = false;

    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Attacking");
        baseHealth -= damage;
    }

    private void CheatCodes()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DegradeWeaponBase();
            DegradeWeaponTrap();
            DegradeBase();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetInt("animalCounter", 0);
            PlayerPrefs.SetInt("raid", 0);
            PlayerPrefs.SetInt("essence", 0);
            PlayerPrefs.SetInt("talisman", 0);
            PlayerPrefs.SetInt("baseLevel", 0);
            PlayerPrefs.SetFloat("currentMoney", 500);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerPrefs.SetInt("animalCounter", 1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.SetInt("talismanOne", 0);
            PlayerPrefs.SetInt("essence", 5);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("raid", 1);
        }
    }
}
