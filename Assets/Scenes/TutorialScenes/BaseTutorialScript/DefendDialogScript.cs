using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefendDialogScript : MonoBehaviour
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

    public DefendManagerScript defendManagerScript;
    public GameObject player;
    public GameObject thorfin;
    public GameObject ballista;
    public GameObject enemyGameObject;
    public GameObject cameraGameObject;
    public GameObject dialogCanvas;
    public GameObject[] highlights;
    public bool moving;
    private bool cantClick = true;

    public int index;


    void Start()
    {
        Time.timeScale = 1;
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
                    if (index == 7)
                    {
                        if (!moving)
                        {
                            dialogCanvas.SetActive(false);
                            thorfin.SetActive(true);
                            player.GetComponent<PlayerMovement>().enabled = true;
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
                            StartCoroutine(WaitAttack());
                            ballista.GetComponent<WeaponScript>().enabled = true;
                            enemyGameObject.SetActive(true);
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

    public void ContinueDialogue()
    {
        index++;
        dialogCanvas.SetActive(true);
        StartCoroutine(TypeLine());
        UpdateImage();
        UpdateName();

        //reset to dialog mode
        player.GetComponent<PlayerMovement>().enabled = false;
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
        dialogCanvas.SetActive(false);
        yield return new WaitForSeconds(15);
        dialogCanvas.SetActive(true);
        NextLine();
        moving = false;
        Debug.Log("Cooldown");
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
