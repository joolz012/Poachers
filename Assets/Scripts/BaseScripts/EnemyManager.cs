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
    void Start()
    {
        //PlayerPrefs.SetInt("animalCounter", 2);
        raidingBase = false;
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
            timerScript.TimerDuration(0.16666667f);
            PlayerPrefs.SetInt("raid", 0);
            weaponManager.isCoroutineRunning = true;
            StartCoroutine(InstantiateObjects());
            raidingBase = true;
        }

        taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (taggedObjects.Length <= 0)
        {
            if(PlayerPrefs.GetFloat("gondarPlayerPrefs") == 2)
            {
                SceneManager.LoadScene("gondarDeath");
            }
        }

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
        BossBattle();
        yield return new WaitForSeconds(10);
        backgroundMusic.PoacherAttacking();
        while (instantiations < maxInstantiations)
        {
            yield return new WaitForSeconds(repeatRate); // Wait for 4 seconds before the next instantiation.
            if (canInstantiate)
            {
                // Get a random index from the spawnPoints array
                int randomIndex = Random.Range(0, spawnPoints.Length);

                // Get the random spawn point's position
                Vector3 randomPosition = spawnPoints[randomIndex].position;
                Instantiate(objectToInstantiate, spawnPoints[randomIndex].position, transform.rotation);
                instantiations++;
            }
        }
        //yield return new WaitForSeconds(60.0f);
        //weaponManager.isCoroutineRunning = false;
        //instantiations = 0;
        //raidingBase = false;
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
        if (PlayerPrefs.GetFloat("ragnarPlayerPrefs") == 1)
        {
            int randomIndexBoss = Random.Range(0, spawnPoints.Length);
            Instantiate(boss[2], spawnPoints[randomIndexBoss].position, transform.rotation);
        }
    }


    // You can call this method to stop instantiating objects if needed.
    public void StopInstantiating()
    {
        StopAllCoroutines();
    }
}
