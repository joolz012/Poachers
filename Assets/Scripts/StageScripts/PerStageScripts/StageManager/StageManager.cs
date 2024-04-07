using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public int animalCounter;
    public GameObject[] animalsGameObjects;
    public GameObject[] gateGameObjects;
    public int defendTimer;
    public bool raidingBase;

    public QuestManager questManager;
    public TimerScript timerScript;

    // Start is called before the first frame update
    void Start()
    {
        raidingBase = false;
        Time.timeScale = 1;
        if(PlayerPrefs.GetInt("animalCounter") > 0)
        {
            defendTimer = Random.Range(7, 8);
            StartCoroutine(BackToBase(defendTimer));
        }
        animalCounter = PlayerPrefs.GetInt("animalCounter", animalCounter);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetInt("animalCounter"));
        if(PlayerPrefs.GetInt("animalCounter") > 0 && !raidingBase)
        {
            PlayerPrefs.SetInt("raid", 1);
            defendTimer = Random.Range(7, 8);
            timerScript.TimerDuration(defendTimer);
            StartCoroutine(BackToBase(defendTimer));
            raidingBase = true;
        }


        GatePass();
    }

    IEnumerator BackToBase(float timer)
    {
        yield return new WaitForSeconds(timer * 60);
        SceneManager.LoadScene("Base");
    }


    void GatePass()
    {
        if (!animalsGameObjects[0].activeInHierarchy)
        {
            gateGameObjects[0].SetActive(false);
        }
        if (questManager.questCounter == 1)
        {
            gateGameObjects[1].SetActive(false);
        }
        if (questManager.questCounter == 2)
        {
            gateGameObjects[2].SetActive(false);
        }
    }
}
