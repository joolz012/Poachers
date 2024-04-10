using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Transform lightTrans;
    public float speed;
    private bool goRight;
    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetFloat("newGame", 0);
        goRight = true;
        StartCoroutine(LightMovement());
    }

    // Update is called once per frame
    void Update()
    {
        if (goRight)
        {
            lightTrans.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            lightTrans.Rotate(0, -speed * Time.deltaTime, 0);
        }

        if (PlayerPrefs.GetFloat("newGame") == 1)
        {
            continueButton.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("newGame") == 0)
        {
            continueButton.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerPrefs.SetFloat("newGame", 0);
        }
    }

    IEnumerator LightMovement()
    {
        while(true)
        {
            yield return new WaitForSeconds(15);
            Debug.Log("Left");
            goRight = false;
            yield return new WaitForSeconds(15);
            Debug.Log("Right");
            goRight = true;
        }
    }

    public void StartGame()
    {
        RestartAll();
        SceneManager.LoadScene("CutScene1");
    }
    public void ContinueGame()
    {
        if(PlayerPrefs.GetFloat("newGame") == 1)
        {
            SceneManager.LoadScene("Base");
        }
    }

    void RestartAll()
    {
        PlayerPrefs.SetFloat("newGame", 0);
        //defending
        //crocodile
        PlayerPrefs.SetInt("crocodile", 0);
        PlayerPrefs.SetInt("crocodileMax", 0);
        PlayerPrefs.SetInt("crocodileEssence", 0);
        PlayerPrefs.SetFloat("stunDuration", 2);

        //tamaraw
        PlayerPrefs.SetInt("tamaraw", 0);
        PlayerPrefs.SetInt("tamarawMax", 0);
        PlayerPrefs.SetInt("tamarawEssence", 0);
        PlayerPrefs.SetFloat("buffDuration", 10);
        PlayerPrefs.SetFloat("tamarawDmg", 0.5f);
        PlayerPrefs.SetFloat("Talisman2Def", 0);

        //pangolin
        PlayerPrefs.SetInt("pangolin", 0);
        PlayerPrefs.SetInt("pangolinMax", 0);
        PlayerPrefs.SetInt("pangolinEssence", 0);
        PlayerPrefs.SetFloat("pangolinHp", 100);
        PlayerPrefs.SetFloat("Talisman3Def", 0);

        //attaking
        //tarsier
        PlayerPrefs.SetInt("tarsier", 0);
        PlayerPrefs.SetInt("tarsierMax", 0);
        PlayerPrefs.SetInt("tarsierEssence", 0);
        PlayerPrefs.SetFloat("tarsierVision", 20);

        //haribon
        PlayerPrefs.SetInt("haribon", 0);
        PlayerPrefs.SetInt("haribonMax", 0);
        PlayerPrefs.SetInt("haribonEssence", 0);
        PlayerPrefs.SetFloat("haribonAtk", 10);
        PlayerPrefs.SetFloat("Talisman2Atk", 0);

        //python
        PlayerPrefs.SetInt("turtle", 0);
        PlayerPrefs.SetInt("turtleMax", 0);
        PlayerPrefs.SetInt("turtleEssence", 0);
        PlayerPrefs.SetFloat("turtleHeal", 20);
        PlayerPrefs.SetFloat("Talisman3Atk", 0);

        //Boss
        PlayerPrefs.SetFloat("gondarPlayerPrefs", 0);
        PlayerPrefs.SetFloat("bjornPlayerPrefs", 0);
        PlayerPrefs.SetFloat("ragnarPlayerPrefs", 0);

        PlayerPrefs.SetInt("keySave", 0);
        PlayerPrefs.SetInt("animalCounter", 0);
        PlayerPrefs.SetInt("raid", 0);
        PlayerPrefs.SetInt("essence", 0);
        PlayerPrefs.SetInt("talisman", 0);
        PlayerPrefs.SetInt("baseLevel", 0);
        PlayerPrefs.SetFloat("currentMoney", 100);


        PlayerPrefs.SetFloat("gondarBattle", 0);
        PlayerPrefs.SetFloat("bjornBattle", 0);
        PlayerPrefs.SetFloat("ragnarBattle", 0);

        //PlayerPrefs.SetInt("animalCounter", 1);
        PlayerPrefs.SetInt("crocodileMax", 0);
        PlayerPrefs.SetInt("tamarawMax", 0);
        PlayerPrefs.SetInt("pangolinMax", 0);
        PlayerPrefs.SetInt("tarsierMax", 0);
        PlayerPrefs.SetInt("haribonMax", 0);
        PlayerPrefs.SetInt("turtleMax", 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
