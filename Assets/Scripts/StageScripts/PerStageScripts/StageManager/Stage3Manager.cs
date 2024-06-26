using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3Manager : MonoBehaviour
{
    public int animalCounter;
    public GameObject[] animalsGameObjects;
    public GameObject bjornGameObject;
    public string bjornPlayerPrefs = "bjornPlayerPrefs";
    public GameObject[] gateGameObjects;
    public int defendTimer;
    public bool raidingBase;

    public QuestManager3 questManager3;
    public TimerScript timerScript;


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
            timerScript.TimerDuration(defendTimer);
            StartCoroutine(BackToBase(defendTimer));
            raidingBase = true;
        }
        //Debug.Log(PlayerPrefs.GetInt("animalCounter"));
        GatePass();

        ChangeScene();
    }

    void ChangeScene()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("Base");
        }

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
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("FinalCutscene");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("GondarDeath");
        }
    }

    IEnumerator BackToBase(float timer)
    {
        yield return new WaitForSeconds(timer * 60);
        //show being raided;
        SceneManager.LoadScene("Base");
    }


    void GatePass()
    {
        if (!animalsGameObjects[0].activeInHierarchy && questManager3.questCounter == 0)
        {
            gateGameObjects[0].SetActive(false);
        }
        if (questManager3.questCounter == 1)
        {
            gateGameObjects[1].SetActive(false);
        }
        if (questManager3.questCounter == 2)
        {
            gateGameObjects[2].SetActive(false);
        }
    }
}
