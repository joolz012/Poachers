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

    [Header("Talisman Two")]
    public float talismanTwoDefaultCd;
    public float buffDuration;
    public float dmgIncrease;
    private float talismanTwoCd;
    private bool talismanTwoBool;

    [Header("Talisman Three")]
    public float talismanThreeDefaultCd;
    public float talismanThreeDuration;
    private float talismanThreeCd;
    private bool talismanThreeBool;

    private void Start()
    {
        talismanDetails[0] = "<b>Tarsier Talisman</b> \nStun All Enemies";
        talismanDetails[1] = "<b>Tamaraw Talisman</b> \nBuff Defensive Towers";
        talismanDetails[2] = "<b>Pangolin Talisman</b> \nIncrease Health Points of Towers";
        audioSource = GetComponent<AudioSource>();
        talismanOneCd = talismanOneDefaultCd;
        talismanTwoCd = talismanTwoDefaultCd;
        talismanThreeCd = talismanThreeDefaultCd;
    }
    void Update()
    {

        TalismanGetUpgrade();

        TalismanController();
        TalismanCooldown();

    }
    public void CrocodileDetails()
    {
        talismanDetailsObject.SetActive(true);
        talismanText.text = talismanDetails[0].ToString();
    }
    public void TamaraweDetails()
    {
        talismanDetailsObject.SetActive(true);
        talismanText.text = talismanDetails[1].ToString();
    }
    public void PangolinDetails()
    {
        talismanDetailsObject.SetActive(true);
        talismanText.text = talismanDetails[2].ToString();
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
    }

    void TalismanController()
    {
        //Talisman 1
        CheckEnemy();
        if (Input.GetKeyDown(KeyCode.Alpha1) && talismanOneCd >= talismanOneDefaultCd)
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
            talismanOneCd = 0;
            talismanOneBool = false;
        }


        //Talisman 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && talismanTwoCd >= talismanTwoDefaultCd)
        {
            if(PlayerPrefs.GetInt("Talisman2Def") == 1)
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
            talismanTwoCd = 0;
            talismanTwoBool = false;
        }
    }


    void TalismanCooldown()
    {
        if (talismanOneCd <= talismanOneDefaultCd)
        {
            talismanOneCd += Time.deltaTime;
        }

        if (talismanTwoCd <= talismanTwoDefaultCd)
        {
            talismanTwoCd += Time.deltaTime;
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
