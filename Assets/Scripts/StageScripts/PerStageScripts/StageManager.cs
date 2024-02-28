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


    // Start is called before the first frame update
    void Start()
    {
        raidingBase = false;
        Time.timeScale = 1;
        if(PlayerPrefs.GetInt("animalCounter") >= 0)
        {
            defendTimer = Random.Range(7, 8);
            StartCoroutine(BackToBase(defendTimer));
        }
        animalCounter = PlayerPrefs.GetInt("animalCounter", animalCounter);

        //if (animalCounter <= 3)
        //{
        //    foreach (GameObject gameObject in animalsGameObjects)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //    if (animalCounter <= 2)
        //    {
        //        animalsGameObjects[2].SetActive(true);

        //        if (animalCounter <= 1)
        //        {
        //            animalsGameObjects[2].SetActive(true);
        //            animalsGameObjects[1].SetActive(true);

        //            if (animalCounter <= 0)
        //            {
        //                foreach (GameObject gameObject in animalsGameObjects)
        //                {
        //                    gameObject.SetActive(true);
        //                }
        //            }
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("animalCounter") > 0 && !raidingBase)
        {
            defendTimer = Random.Range(10, 16);
            StartCoroutine(BackToBase(defendTimer));
            raidingBase = true;
        }
        //Debug.Log(PlayerPrefs.GetInt("animalCounter"));
        GatePass();
    }

    IEnumerator BackToBase(float timer)
    {
        yield return new WaitForSeconds(timer * 60);
        //show being raided;
        PlayerPrefs.SetInt("raid", 1);
        SceneManager.LoadScene("SampleScene");
    }


    void GatePass()
    {
        if (!animalsGameObjects[0].activeInHierarchy)
        {
            gateGameObjects[0].SetActive(false);
        }
        if (!animalsGameObjects[1].activeInHierarchy)
        {
            gateGameObjects[1].SetActive(false);
        }
        if (!animalsGameObjects[2].activeInHierarchy)
        {
            gateGameObjects[2].SetActive(false);
        }
    }
}
