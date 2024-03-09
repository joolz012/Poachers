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

    private float talismanTwoCd;
    private bool talismanTwoBool;
    public Slider talisman2Slider;

    [Header("Talisman Three")]
    public float talismanThreeDefaultCd;
    private float talismanThreeCd;
    private bool talismanThreeBool;
    public Slider talisman3Slider;
    // Start is called before the first frame update
    void Start()
    {
        talismanDetails[0] = "<b>Tarsier Talisman</b> \nStun All Enemies";
        talismanDetails[1] = "<b>Haribon Talisman</b> \nBuff Defensive Towers";
        talismanDetails[2] = "<b>Python Talisman</b> \nIncrease Health Points of Towers";
        //PlayerPrefs.SetFloat("tarsierVision", 20);
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
    public void PythonDetails()
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
    }

    void TalismanCooldown()
    {
        talisman1Slider.value = talismanOneCd;
        if(talismanOneCd >= 0)
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

    IEnumerator TalismanOne()
    {
        while (playerCamera.orthographicSize <= 20.0f)
        {
            playerCamera.orthographicSize += Time.deltaTime;
        }
        yield return new WaitForSeconds(visionDuration);
        while (playerCamera.orthographicSize >= 15.0f)
        {
            playerCamera.orthographicSize -= Time.deltaTime;
        }
        yield break;
    }
}
