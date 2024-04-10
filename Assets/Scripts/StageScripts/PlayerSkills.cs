using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] clip;
    public Camera playerCamera;

    [Header("Talisman Details")]
    public GameObject talismanDetailsObject;
    public Text talismanText;
    private string[] talismanDetails = new string[3];

    [Header("Talisman One")]
    public float talismanOneDefaultCd;
    public float visionDuration;
    private float talismanOneCd;
    private bool talismanOneBool;
    public Slider talisman1Slider;

    [Header("Talisman Two")]
    public float talismanTwoDefaultCd;
    public float attackIncrease;
    private float talismanTwoCd;
    private bool talismanTwoBool;
    public Slider talisman2Slider;

    [Header("Talisman Three")]
    public float talismanThreeDefaultCd;
    public float healIncrease;
    private float talismanThreeCd;
    private bool talismanThreeBool;
    public Slider talisman3Slider;
    // Start is called before the first frame update
    void Start()
    {
        talismanDetails[0] = "<b>Tarsier Talisman</b> \nIncrease Vision";
        talismanDetails[1] = "<b>Haribon Talisman</b> \nIncrease Damage";
        talismanDetails[2] = "<b>Turtle Talisman</b> \nIncrease Health";

        audioSource = GetComponent<AudioSource>();
        talismanOneCd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TalismanGetUpgrade();

        TalismanController();
        TalismanCooldown();
    }
    public void TarsierDetails()
    {
        talismanDetailsObject.SetActive(true);
        talismanText.text = talismanDetails[0].ToString();
    }
    public void HaribonDetails()
    {
        talismanDetailsObject.SetActive(true);
        talismanText.text = talismanDetails[1].ToString();
    }
    public void TurtleDetails()
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
        visionDuration = PlayerPrefs.GetFloat("tarsierVision");
        attackIncrease = PlayerPrefs.GetFloat("haribonAtk");
        healIncrease = PlayerPrefs.GetFloat("turtleHeal");
    }

    void TalismanController()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && talismanOneCd <= 0)
        {
            Debug.Log("talisman One");
            talismanOneBool = true;
        }
        if (talismanOneBool)
        {
            audioSource.PlayOneShot(clip[0]);
            StartCoroutine(TalismanOne());
            talismanOneCd = talismanOneDefaultCd;
            talismanOneBool = false;
        }
        //talisman 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && talismanTwoCd <= 0)
        {
            if (PlayerPrefs.GetInt("Talisman2Atk") >= 1)
            {
                Debug.Log("talisman two");
                talismanTwoBool = true;
            }
            else
            {
                Debug.Log("Talisman Two Locked");
            }
        }
        if (talismanTwoBool)
        {
            audioSource.PlayOneShot(clip[1]);
            GameObject haribonAtk = GameObject.FindGameObjectWithTag("Player");
            PlayerAttack playerAtk = haribonAtk.GetComponent<PlayerAttack>();
            playerAtk.meleeDamage += attackIncrease;
            StartCoroutine(TalismanTwo(playerAtk));
            talismanTwoCd = talismanTwoDefaultCd;
            talismanTwoBool = false;
        }        
        
        //talisman 3
        if (Input.GetKeyDown(KeyCode.Alpha3) && talismanThreeCd <= 0)
        {
            if (PlayerPrefs.GetInt("Talisman3Atk") >= 1)
            {
                Debug.Log("talisman three");
                talismanThreeBool = true;
            }
            else
            {
                Debug.Log("Talisman Three Locked");
            }
        }
        if (talismanThreeBool)
        {
            audioSource.PlayOneShot(clip[2]); 
            GameObject pangolinHeal = GameObject.FindGameObjectWithTag("Player");
            PlayerHealth playerheal = pangolinHeal.GetComponent<PlayerHealth>();
            playerheal.playerHealth += healIncrease;
            talismanThreeCd = talismanThreeDefaultCd;
            talismanThreeBool = false;
        }
    }

    void TalismanCooldown()
    {
        if(talismanOneCd >= 0)
        {
            talisman1Slider.value = talismanOneCd;
            talismanOneCd -= Time.deltaTime;
        }

        if (talismanTwoCd >= 0 && PlayerPrefs.GetInt("Talisman2Atk") == 1)
        {
            talisman2Slider.value = talismanTwoCd;
            talismanTwoCd -= Time.deltaTime;
        }

        if (talismanThreeCd >= 0 && PlayerPrefs.GetInt("Talisman3Atk") == 1)
        {
            talisman3Slider.value = talismanThreeCd;
            talismanThreeCd -= Time.deltaTime;
        }
    }

    IEnumerator TalismanOne()
    {
        playerCamera.orthographicSize = 20;
        yield return new WaitForSeconds(visionDuration);
        playerCamera.orthographicSize = 15;
        yield break;
    }
    IEnumerator TalismanTwo(PlayerAttack attack)
    {
        yield return new WaitForSeconds(10);
        attack.meleeDamage -= attackIncrease;
        yield break;

    }
}
