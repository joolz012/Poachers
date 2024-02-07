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
    private bool cantClick = true;

    private int index;

    void Start()
    {
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
                        }
                    }
                    else if (index == 12)
                    {
                        if (!moving)
                        {
                            GameObject bestiaryCanvas = GameObject.FindGameObjectWithTag("BestiaryCanvas");
                            bestiaryCanvas.SetActive(false);
                            StartChangingDialog();
                        }
                    }
                    else if (index == 28)
                    {
                        if (!moving)
                        {
                            SceneManager.LoadScene("TutorialScene");
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
        HighlightsDisable();
        yield return new WaitForSeconds(3);
        dialogCanvas.SetActive(true);
        NextLine();
        moving = false;
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
