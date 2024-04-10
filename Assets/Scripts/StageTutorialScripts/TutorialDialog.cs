using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDialog : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameText;
    public float textSpeed;
    public string[] characterNames;
    public string[] lines;

    [Header("Image")]
    public Texture2D[] images;
    public RawImage rawImage;

    public TutorialDialogManager tutorialDialogManager;
    public MouseScriptTutorial mouseScriptTutorial;
    public GameObject player, inviWall;
    public GameObject cameraGameObject;
    public GameObject dialogCanvas, talismanGameObject;
    public GameObject[] highlights;
    public bool moving;
    private bool cantClick = true;

    public int index;

    public GameObject soundsManager;
    private TutorialSounds2 tutorialSounds2;

    void Start()
    {
        tutorialSounds2 = soundsManager.GetComponent<TutorialSounds2>();
        soundsManager.SetActive(true);
        Time.timeScale = 0;
        dialogCanvas.SetActive(false);
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
        tutorialSounds2.isPlaying = false;
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
                    if (index == 5)
                    {
                        if (!moving)
                        {
                            player.SetActive(true);
                            StartChangingDialog();
                        }
                    }
                    else if (index == 10)
                    {
                        if (!moving)
                        {
                            dialogCanvas.SetActive(false); 
                            player.GetComponent<PlayerMovementStage>().enabled = true;
                            cameraGameObject.GetComponent<CameraScript>().enabled = true;
                            tutorialDialogManager.GetComponent<TutorialDialogManager>().enabled = false;
                            Time.timeScale = 1;
                            StopAllCoroutines();
                        }
                    }
                    else if (index == 11)
                    {
                        if (!moving)
                        {
                            dialogCanvas.SetActive(false);
                            highlights[1].SetActive(true);
                            StopAllCoroutines();
                            StartCoroutine(Countdown());
                        }
                    }
                    else if (index == 12)
                    {
                        if (!moving)
                        {
                            dialogCanvas.SetActive(false);
                            player.GetComponent<PlayerMovementStage>().enabled = true;
                            player.GetComponent<PlayerAttack>().enabled = true;
                            player.GetComponent<PlayerHealth>().enabled = true;
                            inviWall.SetActive(false);
                            //player.GetComponent<PlayerSKills>().enabled = true;
                            cameraGameObject.GetComponent<CameraScript>().enabled = true;
                            talismanGameObject.SetActive(true);
                            highlights[0].SetActive(true);
                            highlights[1].SetActive(true);
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
        }

        if (highlights[0].activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            highlights[0].SetActive(false);
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
        tutorialSounds2.isPlaying = false;
        tutorialSounds2.indexSounds += 1;
        dialogCanvas.SetActive(true);
        StartCoroutine(TypeLine());
        UpdateImage();
        UpdateName();
    }

    public IEnumerator Countdown()//highlights disable and wait
    {
        yield return new WaitForSeconds(2);
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
            tutorialDialogManager.AddIndex();
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
            tutorialSounds2.indexSounds += 1;
            tutorialSounds2.isPlaying = false;
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
