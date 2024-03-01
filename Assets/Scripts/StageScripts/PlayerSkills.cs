using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] clip;
    public Camera playerCamera;

    [Header("Talisman One")]
    public float talismanOneDefaultCd;
    public float visionDuration;
    private float talismanOneCd;
    private bool talismanOneBool;

    [Header("Talisman Two")]
    public float talismanTwoDefaultCd;
    public float talismanTwoDuration;
    private float talismanTwoCd;
    private bool talismanTwoBool;

    [Header("Talisman Three")]
    public float talismanThreeDefaultCd;
    public float talismanThreeDuration;
    private float talismanThreeCd;
    private bool talismanThreeBool;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        talismanOneCd = talismanOneDefaultCd;
    }

    // Update is called once per frame
    void Update()
    {
        TalismanGetUpgrade();

        TalismanController();
        TalismanCooldown();
    }


    void TalismanGetUpgrade()
    {
        visionDuration = PlayerPrefs.GetFloat("tarsierVision");
    }

    void TalismanController()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && talismanOneCd >= talismanOneDefaultCd) 
        {
            Debug.Log("talisman One");
            talismanOneBool = true;
        }
        if (talismanOneBool)
        {
            audioSource.PlayOneShot(clip[0]);
            StartCoroutine(TalismanOne());
            talismanOneCd = 0;
            talismanOneBool = false;
        }
    }

    void TalismanCooldown()
    {
        if(talismanOneCd <= talismanOneDefaultCd)
        {
            talismanOneCd += Time.deltaTime;
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
