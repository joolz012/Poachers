using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GondarDialogManager : MonoBehaviour
{
    public GameObject cutscene;
    public GameObject dialogCanvas;
    public float cutsceneTimer;

    [Header("Text")]
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameText;
    public float textSpeed;
    public string[] characterNames;
    public string[] lines;

    [Header("Image")]
    public Texture2D[] images;
    public RawImage rawImage;

    public int index;
    public bool canClick = false;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        StartCoroutine(CutScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (canClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    if (index == 0)
                    {
                        PlayerPrefs.SetFloat("gondarPlayerPrefs", PlayerPrefs.GetFloat("gondarPlayerPrefs") + 1);
                        PlayerPrefs.SetFloat("currentMoney", PlayerPrefs.GetFloat("currentMoney") + 500);
                        SceneManager.LoadScene("Base");
                    }
                    else
                    {
                        NextLine();
                    }
                }
                else
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
        canClick = true;
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
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
        dialogCanvas.SetActive(true);
        textComponent.text = string.Empty;
        nameText.text = string.Empty;
        StartDialogue();
        yield break;
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
    IEnumerator CutScene()
    {
        yield return new WaitForSeconds(cutsceneTimer);
        cutscene.SetActive(false);
        StartCoroutine(GameStart());
        yield break;
    }
}
