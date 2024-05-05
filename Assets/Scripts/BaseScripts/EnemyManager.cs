using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] boss;
    public GameObject objectToInstantiate;
    GameObject[] taggedObjects;
    public Transform[] spawnPoints;
    public int maxInstantiations;
    private int instantiations = 0;
    private bool canInstantiate = true;
    public float repeatRate;
    public bool raidingBase;


    public int defendTimer;
    public WeaponManager weaponManager;
    public BackgroundMusicBase backgroundMusic;
    public TimerScript timerScript;

    public bool miniDefence = false;
    void Start()
    {
        //PlayerPrefs.SetInt("animalCounter", 2);
        raidingBase = false;
        if (PlayerPrefs.GetFloat("ragnarBattle") == 1)
        {
            maxInstantiations = 22;
            miniDefence = true;
        }
    }

    private void Update()
    {
        Debug.Log("Animal: " + PlayerPrefs.GetInt("animalCounter"));
        //Debug.Log("Raid: " + PlayerPrefs.GetInt("raid"));

        if (PlayerPrefs.GetInt("animalCounter") > 0 && PlayerPrefs.GetInt("raid") == 0 && !raidingBase)
        {
            //Debug.Log("Timer Start");
            defendTimer = Random.Range(7, 8);
            timerScript.TimerDuration(defendTimer);
            StartCoroutine(DefendRaid(defendTimer));
            raidingBase = true;
        }

        if (PlayerPrefs.GetInt("raid") >= 1 && !raidingBase)
        {
            //back to base
            timerScript.TimerDuration(1f);
            PlayerPrefs.SetInt("raid", 0);
            StartCoroutine(InstantiateObjects());
            raidingBase = true;
        }

        taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (taggedObjects.Length <= 0)
        {
            if(PlayerPrefs.GetFloat("gondarPlayerPrefs") == 2)
            {
                SceneManager.LoadScene("GondarDeath");
            }
            if (PlayerPrefs.GetFloat("bjornPlayerPrefs") == 2)
            {
                SceneManager.LoadScene("GondarDeath");
            }
            if (PlayerPrefs.GetFloat("ragnarPlayerPrefs") == 2)
            {
                SceneManager.LoadScene("GondarDeath");
            }
        }
        //if (taggedObjects.Length <= 0)
        //{
        //    if (PlayerPrefs.GetFloat("bjornPlayerPrefs") == 2)
        //    {
        //        SceneManager.LoadScene("bjornDeath");
        //    }
        //}
        //if (taggedObjects.Length <= 0)
        //{
        //    if (PlayerPrefs.GetFloat("ragnarPlayerPrefs") == 2)
        //    {
        //        SceneManager.LoadScene("ragnarDeath");
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    timerScript.isRunning = false;
        //    raidingBase = false;
        //}
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    timerScript.isRunning = true;
        //    raidingBase = false;
        //}

    }

    IEnumerator DefendRaid(float timer)
    {
        //Debug.LogWarning("Raiding!");
        yield return new WaitForSeconds(timer * 60);
        //show being raided;
        raidingBase = false;
        PlayerPrefs.SetInt("raid", 1);
        PlayerPrefs.SetInt("timerCheck", 0);
        yield break;
    }

    IEnumerator InstantiateObjects()
    {
        Debug.Log("Wait");
        yield return new WaitForSeconds(60);
        BossBattle();
        weaponManager.StopFund();
        backgroundMusic.PoacherAttacking();
        while (instantiations < maxInstantiations)
        {
            yield return new WaitForSeconds(repeatRate);
            if (canInstantiate)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);

                Vector3 randomPosition = spawnPoints[randomIndex].position;
                Instantiate(objectToInstantiate, spawnPoints[randomIndex].position, transform.rotation);
                instantiations++;
            }
        }
        yield return new WaitForSeconds(20.0f);
        //weaponManager.isCoroutineRunning = false;
        //instantiations = 0;
        //raidingBase = false;
        if (miniDefence)
        {
            PlayerPrefs.SetFloat("ragnarBattle", 1);
            SceneManager.LoadScene("Stage4");
            miniDefence = false;
        }
        backgroundMusic.DefaultBgm();
        yield break;
        //Debug.Log("Stopped instantiating objects.");
    }

    void BossBattle()
    {
        if(PlayerPrefs.GetFloat("gondarPlayerPrefs") == 1)
        {
            int randomIndexBoss = Random.Range(0, spawnPoints.Length);
            Instantiate(boss[0], spawnPoints[randomIndexBoss].position, transform.rotation);
        }
        if (PlayerPrefs.GetFloat("bjornPlayerPrefs") == 1)
        {
            int randomIndexBoss = Random.Range(0, spawnPoints.Length);
            Instantiate(boss[1], spawnPoints[randomIndexBoss].position, transform.rotation);
        }
        //if (PlayerPrefs.GetFloat("ragnarPlayerPrefs") == 1)
        //{
        //    int randomIndexBoss = Random.Range(0, spawnPoints.Length);
        //    Instantiate(boss[2], spawnPoints[randomIndexBoss].position, transform.rotation);
        //}
    }


    public void StopInstantiating()
    {
        StopAllCoroutines();
        weaponManager.StartFund();
    }
}
