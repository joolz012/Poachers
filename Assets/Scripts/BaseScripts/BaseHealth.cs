using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BaseHealth : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clip;
    public Slider healthslider;
    public int baseLevel;
    public float baseMaxHealth;
    public float baseHealth;
    public Transform healthbar, isoCam;
    public float distanceToEnemy;


    public GameObject[] targets;

    public EnemyManager enemyManager;
    public AnimalCounter animalCounter;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        baseHealth = baseMaxHealth;
        healthslider.value = baseHealth;
        healthslider.maxValue = baseMaxHealth;

        //new Game
        if(PlayerPrefs.GetFloat("newGame") == 0)
        {
            DegradeWeaponBase();
            DegradeWeaponTrap();
            DegradeBase();
            PlayerPrefs.SetFloat("newGame", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Key: " + PlayerPrefs.GetInt("keySave"));
        CheatCodes();
        healthslider.value = baseHealth;
        healthbar.LookAt(isoCam.position);
        if (baseHealth <= 0 && PlayerPrefs.GetInt("animalCounter") > 0)
        {
            Debug.Log("DecreaseHealth");
            animalCounter.DecreaseAnimal();
            audioSource.PlayOneShot(clip[0]);
            baseHealth = baseMaxHealth;

        }
        else if(baseHealth <= 0 && PlayerPrefs.GetInt("animalCounter") <= 0)
        {
            audioSource.PlayOneShot(clip[0]);
            DegradeBase();
            DegradeWeaponBase();
            DegradeWeaponTrap();
            RemoveAllEnemy();
            baseHealth = baseMaxHealth;
            if(PlayerPrefs.GetFloat("gondarPlayerPrefs") == 1)
            {
                PlayerPrefs.SetFloat("gondarPlayerPrefs", PlayerPrefs.GetFloat("gondarPlayerPrefs") - 1);
            }
            if (PlayerPrefs.GetFloat("bjornPlayerPrefs") == 1)
            {
                PlayerPrefs.SetFloat("bjornPlayerPrefs", PlayerPrefs.GetFloat("bjornPlayerPrefs") - 1);
            }
        }

        HealthBar();
    }

    private void HealthBar()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, closestEnemy.transform.position);
            if (distance < distanceToEnemy)
            {
                Debug.Log("Show");
                healthbar.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Hide");
                healthbar.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Hide");
            healthbar.gameObject.SetActive(false);
        }
    }
    GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = target;
                closestDistance = distance;
            }
        }

        return closestEnemy;
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
            //defending
            //crocodile
            PlayerPrefs.SetInt("crocodile", 0);
            PlayerPrefs.SetInt("crocodileMax", 0);
            PlayerPrefs.SetInt("crocodileEssence", 0);
            PlayerPrefs.SetFloat("stunDuration", 2);

            //tamaraw
            PlayerPrefs.SetInt("tamaraw", 0);
            PlayerPrefs.SetInt("tamarawMax", 0);
            PlayerPrefs.SetInt("tamarawEssence", 0);
            PlayerPrefs.SetFloat("buffDuration", 10);
            PlayerPrefs.SetFloat("tamarawDmg", 0.5f);
            PlayerPrefs.SetFloat("Talisman2Def", 0);

            //pangolin
            PlayerPrefs.SetInt("pangolin", 0);
            PlayerPrefs.SetInt("pangolinMax", 0);
            PlayerPrefs.SetInt("pangolinEssence", 0);
            PlayerPrefs.SetFloat("pangolinHp", 100);
            PlayerPrefs.SetFloat("Talisman3Def", 0);

            //attaking
            //tarsier
            PlayerPrefs.SetInt("tarsier", 0);
            PlayerPrefs.SetInt("tarsierMax", 0);
            PlayerPrefs.SetInt("tarsierEssence", 0);
            PlayerPrefs.SetFloat("tarsierVision", 20);

            //haribon
            PlayerPrefs.SetInt("haribon", 0);
            PlayerPrefs.SetInt("haribonMax", 0);
            PlayerPrefs.SetInt("haribonEssence", 0);
            PlayerPrefs.SetFloat("haribonAtk", 10);
            PlayerPrefs.SetFloat("Talisman2Atk", 0);

            //python
            PlayerPrefs.SetInt("turtle", 0);
            PlayerPrefs.SetInt("turtleMax", 0);
            PlayerPrefs.SetInt("turtleEssence", 0);
            PlayerPrefs.SetFloat("turtleHeal", 20);
            PlayerPrefs.SetFloat("Talisman3Atk", 0);

            //Boss
            PlayerPrefs.SetFloat("gondarPlayerPrefs", 0);
            PlayerPrefs.SetFloat("bjornPlayerPrefs", 0);
            PlayerPrefs.SetFloat("ragnarPlayerPrefs", 0);

            PlayerPrefs.SetInt("keySave", 0);
            PlayerPrefs.SetInt("animalCounter", 0);
            PlayerPrefs.SetInt("raid", 0);
            PlayerPrefs.SetInt("essence", 0);
            PlayerPrefs.SetInt("talisman", 0);
            PlayerPrefs.SetInt("baseLevel", 0);
            PlayerPrefs.SetFloat("currentMoney", 100);


            PlayerPrefs.SetFloat("gondarBattle", 0);
            PlayerPrefs.SetFloat("bjornBattle", 0);
            PlayerPrefs.SetFloat("ragnarBattle", 0);

            //PlayerPrefs.SetInt("animalCounter", 1);
            PlayerPrefs.SetInt("crocodileMax", 0);
            PlayerPrefs.SetInt("tamarawMax", 0);
            PlayerPrefs.SetInt("pangolinMax", 0);
            PlayerPrefs.SetInt("tarsierMax", 0);
            PlayerPrefs.SetInt("haribonMax", 0);
            PlayerPrefs.SetInt("turtleMax", 0);

            SceneManager.LoadScene("Base");
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManager.LoadScene("Stage1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayerPrefs.SetFloat("gondarBattle", 0);
            SceneManager.LoadScene("Stage2");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.SetFloat("gondarBattle", 1);
            SceneManager.LoadScene("Stage2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayerPrefs.SetFloat("bjornBattle", 0);
            SceneManager.LoadScene("Stage3");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerPrefs.SetFloat("bjornBattle", 1);
            SceneManager.LoadScene("Stage3");
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayerPrefs.SetFloat("ragnarBattle", 0);
            SceneManager.LoadScene("Stage4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.SetFloat("ragnarBattle", 1);
            SceneManager.LoadScene("Stage4");
        }
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    PlayerPrefs.SetInt("talismanOne", 0);
        //    PlayerPrefs.SetInt("essence", 5);
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    PlayerPrefs.SetInt("raid", 1);
        //}
    }
}
