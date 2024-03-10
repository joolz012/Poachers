using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutSceneManager : MonoBehaviour
{
    public float CutsceneTimer;
    public string changeSceneString;
    public GameObject skipText;

    private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        skipText.SetActive(false);
        StartCoroutine(CutSceneChangeScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !isPressed)
        {
            isPressed = true;
            StartCoroutine(SkipCutscene());
        }
        else if (Input.anyKeyDown && isPressed)
        {
            SceneManager.LoadScene(changeSceneString);
        }

    }

    IEnumerator SkipCutscene()
    {
        skipText.SetActive(true);
        yield return new WaitForSeconds(5);
        skipText.SetActive(false);
        isPressed = false;
        yield break;
    }

    IEnumerator CutSceneChangeScene()
    {
        yield return new WaitForSeconds(CutsceneTimer);
        SceneManager.LoadScene(changeSceneString);
    }
}
