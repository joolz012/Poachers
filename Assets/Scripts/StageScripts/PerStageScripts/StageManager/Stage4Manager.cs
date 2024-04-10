using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage4Manager : MonoBehaviour
{
    public int animalCounter;
    public int defendTimer;
    public bool raidingBase;


    // Start is called before the first frame update
    void Start()
    {
        raidingBase = false;
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("animalCounter") >= 0)
        {
            defendTimer = Random.Range(7, 8);
            StartCoroutine(BackToBase(defendTimer));
        }
        animalCounter = PlayerPrefs.GetInt("animalCounter", animalCounter);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("animalCounter") > 0 && !raidingBase)
        {
            PlayerPrefs.SetInt("raid", 1);
            defendTimer = Random.Range(7, 8);
            StartCoroutine(BackToBase(defendTimer));
            raidingBase = true;
        }
        ChangeScene();
    }

    void ChangeScene()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManager.LoadScene("Stage1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayerPrefs.SetFloat("gondarBattle", 0);
            SceneManager.LoadScene("Stage2");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.SetFloat("gondarBattle", 1);
            SceneManager.LoadScene("Stage2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayerPrefs.SetFloat("bjornBattle", 0);
            SceneManager.LoadScene("Stage3");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerPrefs.SetFloat("bjornBattle", 1);
            SceneManager.LoadScene("Stage3");
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayerPrefs.SetFloat("ragnarBattle", 0);
            SceneManager.LoadScene("Stage4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.SetFloat("ragnarBattle", 1);
            SceneManager.LoadScene("Stage4");
        }
    }

    IEnumerator BackToBase(float timer)
    {
        yield return new WaitForSeconds(timer * 60);
        //show being raided;
        SceneManager.LoadScene("Base");
    }
}
