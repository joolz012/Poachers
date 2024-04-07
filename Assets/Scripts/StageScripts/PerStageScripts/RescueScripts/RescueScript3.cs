using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueScript3 : MonoBehaviour
{
    private StageManager stageManager;
    public QuestManager3 questManager3;
    public int questAdd;

    public string whatAnimal;
    public GameObject rescueCanvas;

    [Header("Per Stage Animal (Don't Fill)")]
    public string animal1;
    public string animal2;
    public string animal3;
    // Start is called before the first frame update
    void Start()
    {
        questManager3 = GameObject.Find("QuestCanvas").GetComponent<QuestManager3>();
        animal1 = questManager3.animal1;
        animal2 = questManager3.animal2;
        animal3 = questManager3.animal3;
        //PlayerPrefs.SetInt("animalCounter", 0);
        rescueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetInt("animalCounter"));
        //Debug.Log(PlayerPrefs.GetInt("raid"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rescueCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rescueCanvas.SetActive(false);
        }
    }

    public void RescueAnimal()
    {
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            if (whatAnimal == animal1)
            {
                Debug.Log("Add Python");
                questManager3.currentAnimal += 1;
            }
            else if (whatAnimal == animal2)
            {
                Debug.Log("Add Crocodile");
                questManager3.currentAnimal2 += 1;
            }
            else if (whatAnimal == animal3)
            {
                Debug.Log("Add Pangolin");
                questManager3.currentAnimal3 += 1;
            }
            Debug.Log("Add Animal");
            GameObject parentGameObject = parentTransform.gameObject;
            PlayerPrefs.SetInt("animalCounter", PlayerPrefs.GetInt("animalCounter") + 1);
            PlayerPrefs.SetInt(whatAnimal, PlayerPrefs.GetInt(whatAnimal) + 1);
            PlayerPrefs.SetInt("raid", 1);
            PlayerPrefs.SetInt("essence", PlayerPrefs.GetInt("essence") + 1);
            parentGameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("This GameObject has no parent!");
        }
    }
}
