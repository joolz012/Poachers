using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    public Slider healthslider;
    public float baseMaxHealth;
    public float baseHealth;
    public Transform healthbar, isoCam;

    public EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        baseHealth = baseMaxHealth;
        healthslider.maxValue = baseMaxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        healthbar.LookAt(isoCam.position);
        healthslider.value = baseHealth;
        if (baseHealth <= 0 && PlayerPrefs.GetInt("animalCounter") > 0)
        {
            AnimalCounter.essenceCounter -= 1;
            PlayerPrefs.SetInt("animalCounter", PlayerPrefs.GetInt("animalCounter") - 1);
            baseHealth = baseMaxHealth;
        }
        else if(baseHealth <= 0 && PlayerPrefs.GetInt("animalCounter") <= 0)
        {
            DecreaseLevel();
            RemoveAllEnemy();
            baseHealth = baseMaxHealth;
        }

        if (PlayerPrefs.GetInt("animalCounter") <= 0)
        {
            PlayerPrefs.SetInt("raid", 0);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DecreaseLevel();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetInt("animal", 0);
            PlayerPrefs.SetInt("raid", 0);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("raid", 1);
        }
    }

    private void DecreaseLevel()
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
                    //weaponUpgrade.DecreaseUpgradeLevel(2);
                }
                if (weaponUpgrade.currentUpgradeLevel == 1)
                {
                    weaponUpgrade.currentUpgradeLevel -= 1;
                    weaponUpgrade.DegradeWeapon(weaponDegrade);
                    //weaponUpgrade.DecreaseUpgradeLevel(1);
                }
            }
            else
            {
                Debug.LogWarning("WeaponUpgradeManager or WeaponScript component not found in object with tag " + weaponObject.tag);
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

    }

    public void TakeDamage(float damage)
    {
        baseHealth -= damage;
    }
}
