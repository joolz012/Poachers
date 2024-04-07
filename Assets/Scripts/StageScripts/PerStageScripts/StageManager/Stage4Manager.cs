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
        //Debug.Log(PlayerPrefs.GetInt("animalCounter"));
    }

    IEnumerator BackToBase(float timer)
    {
        yield return new WaitForSeconds(timer * 60);
        //show being raided;
        SceneManager.LoadScene("Base");
    }
}
