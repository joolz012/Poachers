using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogScript : MonoBehaviour
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

    public DialogManager dialogManager;
    public MouseScript mouseScript;
    public GameObject dialogCanvas;
    public GameObject[] highlights;
    public bool moving;
    public bool cantClick = true;

    public int index;

    public AudioSource[] audioSource;

    public GameObject soundsManager;
    private TutorialSounds1 tutorialSounds1;

    void Start()
    {
        tutorialSounds1 = soundsManager.GetComponent<TutorialSounds1>();
        soundsManager.SetActive(true);
        dialogCanvas.SetActive(false);
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
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
                            StartChangingDialog();
                        }
                    }
                    else if (index == 8)
                    {
                        if (!moving)
                        {
                            StopAllCoroutines();
                            dialogCanvas.SetActive(false);
                            mouseScript.gameObject.SetActive(true);
                            highlights[0].SetActive(true);
                            cantClick = true;
                        }
                    }
                    else if (index == 12)
                    {
                        if (!moving)
                        {
                            StopAllCoroutines();
                            GameObject bestiaryCanvas = GameObject.FindGameObjectWithTag("BestiaryCanvas");
                            bestiaryCanvas.SetActive(false);
                            StartChangingDialog();
                        }
                    }
                    else if (index == 14)
                    {
                        if (!moving)
                        {
                            NextLine();
                            audioSource[3].Play();
                        }
                    }
                    else if (index == 16)
                    {
                        if (!moving)
                        {
                            NextLine();
                            audioSource[0].Play();
                        }
                    }
                    else if (index == 17)
                    {
                        if (!moving)
                        {
                            NextLine();
                            audioSource[0].Play();
                        }
                    }
                    else if (index == 21)
                    {
                        if (!moving)
                        {
                            NextLine();
                            audioSource[0].Play();
                        }
                    }
                    else if (index == 28)
                    {
                        if (!moving)
                        {
                            SceneManager.LoadScene("StageTutorialScene");
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
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        UpdateImage();
        UpdateName();
    }

    public IEnumerator Countdown()
    {
        Debug.Log("Countdown");
        HighlightsDisable();
        yield return new WaitForSeconds(3);
        dialogCanvas.SetActive(true);
        NextLine();
        cantClick = false;
    }

    public void HighlightsDisable()
    {
        foreach(GameObject images in highlights)
        {
            images.SetActive(false);
        }
    }

    IEnumerator CooldownDialog()
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
    void StartChangingDialog()
    {
        if (!moving)
        {
            dialogManager.AddIndex();
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
            tutorialSounds1.indexSounds += 1;
            tutorialSounds1.isPlaying = false;
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
