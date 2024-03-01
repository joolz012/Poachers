using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManagerBase : MonoBehaviour
{
    public int keyCounter;
    public GameObject[] stages;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject stage in stages)
        {
            stage.SetActive(false);
        }
        keyCounter = PlayerPrefs.GetInt("keySave");
    }

    // Update is called once per frame
    void Update()
    {
        if(keyCounter >= 1)
        {
            stages[0].SetActive(true);
        }
        if (keyCounter >= 2)
        {
            stages[1].SetActive(true);
        }
    }
}
