using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject objectToInstantiate;
    public Transform[] spawnPoints;
    public int maxInstantiations;
    private int instantiations = 0;
    private bool canInstantiate = true;
    public float repeatRate;
    public bool raidingBase;


    public int defendTimer;
    public WeaponManager weaponManager;
    public TimerScript timerScript;
    void Start()
    {
        raidingBase = false;
    }

    private void Update()
    {
        Debug.Log("Animal: " + PlayerPrefs.GetInt("animalCounter"));
        Debug.Log("Raid: " + PlayerPrefs.GetInt("raid"));

        if (PlayerPrefs.GetInt("animalCounter") > 0 && PlayerPrefs.GetInt("raid") == 0 && !raidingBase)
        {
            //Debug.Log("Timer Start");
            defendTimer = Random.Range(10, 15);
            timerScript.TimerDuration(defendTimer);
            StartCoroutine(DefendRaid(defendTimer));
            raidingBase = true;
        }

        if (PlayerPrefs.GetInt("raid") >= 1 && !raidingBase)
        {
            PlayerPrefs.SetInt("raid", 0);
            weaponManager.isCoroutineRunning = true;
            StartCoroutine(InstantiateObjects());
            raidingBase = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            timerScript.isRunning = false;
            raidingBase = false;
        }
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
        yield return new WaitForSeconds(60.0f);
        weaponManager.isCoroutineRunning = false;
        instantiations = 0;
        raidingBase = false;
        yield break;
        //Debug.Log("Stopped instantiating objects.");
    }

    // You can call this method to stop instantiating objects if needed.
    public void StopInstantiating()
    {
        weaponManager.isCoroutineRunning = false;
    }
}
