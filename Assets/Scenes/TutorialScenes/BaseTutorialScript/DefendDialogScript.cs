using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefendDialogScript : MonoBehaviour
{
    public GameObject playerGameObject;
    [Header("Text")]
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameText;
    public float textSpeed;
    public string[] characterNames;
    public string[] lines;

    [Header("Image")]
    public Texture2D[] images;
    public RawImage rawImage;

    [Header("Others")]
    public DefendManagerScript defendManagerScript;
    public GameObject player;
    public GameObject thorfin;
    public GameObject ballista;
    public GameObject enemyOne;
    public GameObject[] enemyGameObject;
    public GameObject cameraGameObject;
    public GameObject dialogCanvas;
    public GameObject talismanCanvas, talismanGameObject;
    public GameObject[] highlights;
    public AudioSource[] audioSource;
    public float waitTime;
    public bool moving;
    private bool cantClick = true;

    public int index;


    public GameObject soundsManager;
    private TutorialSounds3 tutorialSounds3;
    void Start()
    {
        tutorialSounds3 = soundsManager.GetComponent<TutorialSounds3>();
        soundsManager.SetActive(true);
        Time.timeScale = 1;
        dialogCanvas.SetActive(false);
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
        tutorialSounds3.isPlaying = false;
        dialogCanvas.SetActive(true);
        textComponent.text = string.Empty;
        nameText.text = string.Empty;
        StartDialogue();
        cantClick = false;
        yield break;
    }

    void Update()
    {
        if (!cantClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    if (index == 4)
                    {
                        if (!moving)
                        {
                            NextLine();
                            audioSource[3].Play();
                        }
                    }
                    else if (index == 7)
                    {
                        if (!moving)
                        {
                            dialogCanvas.SetActive(false);
                            thorfin.SetActive(true);
                            player.GetComponent<PlayerMovementBase>().enabled = true;
                            //player.GetComponent<PlayerAttack>().enabled = true;
                            cameraGameObject.GetComponent<CameraScript>().enabled = true;
                            defendManagerScript.GetComponent<DefendManagerScript>().enabled = false;
                            Time.timeScale = 1;
                            StopAllCoroutines();
                        }
                    }
                    else if (index == 8)
                    {
                        if (!moving)
                        {
                            StopAllCoroutines();
                            StartCoroutine(WaitAttack());
                            StartCoroutine(WaitSpeak());
                            ballista.GetComponent<WeaponScript>().enabled = true;
                            enemyOne.SetActive(true);
                            defendManagerScript.AddIndex();
                        }
                    }
                    else if (index == 11)
                    {
                        if (!moving)
                        {
                            enemyGameObject[0].SetActive(true);
                            defendManagerScript.AddIndex();
                            talismanCanvas.SetActive(true);
                            dialogCanvas.SetActive(false);
                            //talismanGameObject.SetActive(true);
                            waitTime = 1;
                            StopAllCoroutines();
                            moving = true;
                        }
                    }
                    else if (index == 12)
                    {
                        player.GetComponent<PlayerMovementBase>().enabled = true;
                        player.GetComponent<PlayerAttackBase>().enabled = true;
                        if (!moving)
                        {
                            GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
                            foreach (GameObject weapon in weapons)
                            {
                                WeaponScript weaponScript = weapon.GetComponent<WeaponScript>();
                                if (weaponScript != null)
                                {
                                    weaponScript.enabled = true;
                                }
                            }
                            foreach (GameObject enemy in enemyGameObject)
                            {
                                EnemyAttackBase enemyAttack = enemy.GetComponent<EnemyAttackBase>();
                                if (enemyAttack != null)
                                {
                                    enemyAttack.enabled = true;
                                }
                            }
                            dialogCanvas.SetActive(false);
                            //waitTime = 7;
                            StopAllCoroutines();
                            //StartCoroutine(WaitAttack());
                        }
                    }
                    else if (index == 13)
                    {
                        NextLine();
                        player.GetComponent<PlayerMovementBase>().enabled = false;
                        player.GetComponent<PlayerAttackBase>().enabled = false;
                    }
                    else if (index == 19)
                    {
                        if (!moving)
                        {
                            SceneManager.LoadScene("Base");
                        }
                    }
                    else
                    {
                        NextLine();
                    }
                }
                else if (!moving)
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
            if (index == 11 && Input.GetKeyDown(KeyCode.Alpha1))
            {
                moving = false;
                HighlightsDisable();
                cantClick = false;
                StartCoroutine(WaitAttack());
                GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
                foreach (GameObject weapon in weapons)
                {
                    WeaponScript weaponScript = weapon.GetComponent<WeaponScript>();
                    if (weaponScript != null)
                    {
                        weaponScript.enabled = false;
                    }

                }

                foreach (GameObject enemy in enemyGameObject) 
                { 
                    EnemyAttackBase enemyAttack = enemy.GetComponent<EnemyAttackBase>();
                    if(enemyAttack != null)
                    {
                        Debug.Log("Stun");
                        enemyAttack.enemyAnimator.Play("Idle");
                        enemyAttack.enabled = false;
                    }
                    else
                    {
                        Debug.Log("No Enemy");
                    }
                }
            }
            if(index == 12 && enemyGameObject[0] == null)
            {
                player.GetComponent<PlayerMovementBase>().playerAnim.Play("Idle");
                player.GetComponent<PlayerMovementBase>().enabled = false;
                dialogCanvas.SetActive(true);
                NextLine();
                moving = false;
                cantClick = false;
                Debug.Log("Cooldown");
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        UpdateImage();
        UpdateName();
    }

    public void ContinueDialogue()
    {
        index++;
        dialogCanvas.SetActive(true);
        StartCoroutine(TypeLine());
        UpdateImage();
        UpdateName();

        //reset to dialog mode
        player.GetComponent<PlayerMovementBase>().enabled = false;
        //player.GetComponent<PlayerAttack>().enabled = false;
        cameraGameObject.GetComponent<CameraScript>().enabled = false;
        defendManagerScript.GetComponent<DefendManagerScript>().enabled = true;
    }

    public IEnumerator Countdown()//highlights disable and wait
    {
        yield return new WaitForSeconds(3);
        HighlightsDisable();
        dialogCanvas.SetActive(true);
        NextLine();
        moving = false;
    }

    public void HighlightsDisable()
    {
        foreach (GameObject images in highlights)
        {
            images.SetActive(false);
        }
    }
    IEnumerator WaitAttack()//wait attack
    {
        cantClick = true;
        dialogCanvas.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        defendManagerScript.AddIndex();
        dialogCanvas.SetActive(true);
        NextLine();
        moving = false;
        cantClick = false;
        Debug.Log("Cooldown");
    }
    IEnumerator WaitSpeak()//wait attack
    {
        yield return new WaitForSeconds(waitTime);
        tutorialSounds3.isPlaying = false;
    }
    IEnumerator CooldownDialog()//close dialog no movement
    {
        dialogCanvas.SetActive(false);
        yield return new WaitForSeconds(2);
        dialogCanvas.SetActive(true);
        NextLine();
        moving = false;
        Debug.Log("Cooldown");
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void StartChangingDialog()//change cam
    {
        if (!moving)
        {
            defendManagerScript.AddIndex();
            textComponent.text = string.Empty;
            nameText.text = string.Empty;
            StopAllCoroutines();
            StartCoroutine(CooldownDialog());
            moving = true;
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            tutorialSounds3.indexSounds += 1;
            tutorialSounds3.isPlaying = false;
            StartCoroutine(TypeLine());
            UpdateImage();
            UpdateName();
        }
        else
        {
            index = 0; // Restart dialogue loop
            dialogCanvas.SetActive(false);
        }
    }

    void UpdateName()
    {
        if (index < characterNames.Length)
        {
            nameText.text = characterNames[index];
        }
    }

    void UpdateImage()
    {
        if (index < images.Length)
        {
            rawImage.texture = images[index];
        }
    }

}
