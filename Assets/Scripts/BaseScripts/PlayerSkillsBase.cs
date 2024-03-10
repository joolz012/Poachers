using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillsBase : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] clip;

    public EnemyMovement[] enemyMovements;
    public WeaponScript[] weaponScript;

    // Find all GameObjects with the "Enemy" tag
    //GameObject[] enemyObjects;
    [Header("Talisman Details")]
    public GameObject talismanDetailsObject;
    public Text talismanText;
    private string[] talismanDetails = new string[3];

    [Header("Talisman One")]
    public float talismanOneDefaultCd;
    public float stunDuration;
    private float talismanOneCd;
    private bool talismanOneBool;
    public Slider talisman1Slider;

    [Header("Talisman Two")]
    public float talismanTwoDefaultCd;
    public float buffDuration;
    public float dmgIncrease;
    private float talismanTwoCd;
    private bool talismanTwoBool;
    public Slider talisman2Slider;

    [Header("Talisman Three")]
    public float talismanThreeDefaultCd;
    public float talismanHealthIncrease;
    private float talismanThreeCd;
    private bool talismanThreeBool;
    public Slider talisman3Slider;

    private void Start()
    {
        talismanDetails[0] = "<b>Crocodile Talisman</b> \nStun All Enemies";
        talismanDetails[1] = "<b>Tamaraw Talisman</b> \nBuff Defensive Towers";
        talismanDetails[2] = "<b>Pangolin Talisman</b> \nIncrease Health Points of Towers";
        audioSource = GetComponent<AudioSource>();

        talismanOneCd = 0;
        talisman1Slider.maxValue = 30;

        talismanTwoCd = 0;
        talisman2Slider.maxValue = 50;

        talismanThreeCd = 0;
        talisman3Slider.maxValue = 30;

    }
    void Update()
    {
        TalismanGetUpgrade();

        TalismanController();
        TalismanCooldown();

    }
    public void CrocodileDetails()
    {
        talismanText.text = talismanDetails[0].ToString();
        talismanDetailsObject.SetActive(true);
    }
    public void TamarawDetails()
    {
        talismanText.text = talismanDetails[1].ToString();
        talismanDetailsObject.SetActive(true);
    }
    public void PangolinDetails()
    {
        talismanText.text = talismanDetails[2].ToString();
        talismanDetailsObject.SetActive(true);
    }

    public void ExitPointer()
    {
        talismanDetailsObject.SetActive(false);
        talismanText.text = "";
    }

    void TalismanGetUpgrade()
    {
        stunDuration = PlayerPrefs.GetFloat("stunDuration");

        //tamaraw
        buffDuration = PlayerPrefs.GetFloat("buffDuration");
        dmgIncrease = PlayerPrefs.GetFloat("tamarawDmg");

        //pangolin
        talismanHealthIncrease = PlayerPrefs.GetFloat("pangolinHp");
    }

    void TalismanController()
    {
        //Talisman 1
        CheckEnemy();
        if (Input.GetKeyDown(KeyCode.Alpha1) && talismanOneCd <= 0)
        {
            talismanOneBool = true;
        }
        if (talismanOneBool)
        {
            audioSource.PlayOneShot(clip[0]);
            foreach (EnemyMovement movement in enemyMovements)
            {
                if (movement != null)
                {
                    movement.stunned = true;
                    movement.StunEnemy(stunDuration);
                }
                else
                {
                    Debug.Log("No Enemy");
                }
            }
            talismanOneCd = talismanOneDefaultCd;
            talismanOneBool = false;
        }


        //Talisman 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && talismanTwoCd <= 0)
        {
            if(PlayerPrefs.GetInt("Talisman2Def") >= 1)
            {
                Debug.Log("Tamaraw Talisman");
                talismanTwoBool = true;
            }
            else
            {
                Debug.Log("Tamaraw Not Unlocked");
            }
        }
        if (talismanTwoBool)
        {
            audioSource.PlayOneShot(clip[1]);
            foreach (WeaponScript weapon in weaponScript)
            {
                if (weapon != null)
                {
                    weapon.attackDamage += dmgIncrease;
                    StartCoroutine(TamarawDuration(weapon));
                }
                else
                {
                    Debug.Log("No Tower");
                }
            }
            talismanThreeCd = talismanTwoDefaultCd;
            talismanTwoBool = false;
        }

        //Talisman 3
        if (Input.GetKeyDown(KeyCode.Alpha3) && talismanThreeCd <= 0)
        {
            if (PlayerPrefs.GetInt("Talisman3Def") >= 1)
            {
                Debug.Log("Tamaraw Pangolin");
                talismanThreeBool = true;
            }
            else
            {
                Debug.Log("Pangolin Not Unlocked");
            }
        }
        if (talismanThreeBool)
        {
            audioSource.PlayOneShot(clip[2]);

            GameObject baseObject = GameObject.FindGameObjectWithTag("Base");
            BaseHealth baseHealth = baseObject.GetComponent<BaseHealth>();
            baseHealth.baseHealth += talismanHealthIncrease;

            talismanThreeCd = talismanThreeDefaultCd;
            talismanThreeBool = false;
        }
    }


    void TalismanCooldown()
    {
        talisman1Slider.value = talismanOneCd;
        if (talismanOneCd >= 0)
        {
            talismanOneCd -= Time.deltaTime;
        }

        talisman2Slider.value = talismanTwoCd;
        if (talismanTwoCd >= 0)
        {
            talismanTwoCd -= Time.deltaTime;
        }

        talisman3Slider.value = talismanThreeCd;
        if (talismanThreeCd >= 0)
        {
            talismanThreeCd -= Time.deltaTime;
        }
    }

    //talisman 1
    void CheckEnemy()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");

        // List to hold found scripts
        List<EnemyMovement> foundScripts = new List<EnemyMovement>();

        foreach (GameObject obj in taggedObjects)
        {
            EnemyMovement script = obj.GetComponent<EnemyMovement>();
            if (script != null)
            {
                foundScripts.Add(script);
            }
        }

        // Convert list to array
        enemyMovements = foundScripts.ToArray();
    }

    //talisman 2
    IEnumerator TamarawDuration(WeaponScript weapon)
    {
        yield return new WaitForSeconds(buffDuration);
        weapon.attackDamage -= dmgIncrease;
        yield break;
    }
}
