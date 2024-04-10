using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    public int essenceCounter;
    public GameObject craftOpener, craftPanel;
    public GameObject[] enableGameObjects, disableGameObjects;

    public Text crodocileText, tamarawText, pangolinText, tarsierText, haribonText, turtleText;
    // Start is called before the first frame update
    void Start()
    {
        essenceCounter = PlayerPrefs.GetInt("essence");
        //Debug.Log("Essence: " + essenceCounter);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Tarsier: " + PlayerPrefs.GetInt("tarsier"));
        AnimalsEssence();

    }

    void AnimalsEssence()
    {
        crodocileText.text = PlayerPrefs.GetInt("crocodile2").ToString();
        tamarawText.text = PlayerPrefs.GetInt("tamaraw2").ToString();
        pangolinText.text = PlayerPrefs.GetInt("pangolin2").ToString();
        tarsierText.text = PlayerPrefs.GetInt("tarsier2").ToString();
        haribonText.text = PlayerPrefs.GetInt("haribon2").ToString();
        turtleText.text = PlayerPrefs.GetInt("turtle2").ToString();
    }

    public void UpdateEssenceCounter(int newValue)
    {
        essenceCounter = newValue;
        PlayerPrefs.SetInt("essence", essenceCounter);
        //PlayerPrefs.Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            craftOpener.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach(GameObject enable in enableGameObjects)
            {
                enable.SetActive(true);
            }
            foreach (GameObject disable in disableGameObjects)
            {
                disable.SetActive(false);
            }
        }
    }
}
