using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutSceneManager : MonoBehaviour
{
    public float CutsceneTimer;
    public string changeSceneString;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CutSceneChangeScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CutSceneChangeScene()
    {
        yield return new WaitForSeconds(CutsceneTimer);
        SceneManager.LoadScene(changeSceneString);
    }
}
