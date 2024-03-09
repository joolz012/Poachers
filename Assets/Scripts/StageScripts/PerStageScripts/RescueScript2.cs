using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueScript2 : MonoBehaviour
{
    private StageManager stageManager;
    public QuestManager2 questManager2;
    public int questAdd;

    public string whatAnimal;
    public GameObject rescueCanvas;

    [Header("Per Stage Animal (Don't Fill)")]
    public string animal1;
    public string animal2;
    // Start is called before the first frame update
    void Start()
    {
        questManager2 = GameObject.Find("QuestCanvas").GetComponent<QuestManager2>();
        animal1 = questManager2.animal1;
        animal2 = questManager2.animal2;
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
                Debug.Log("Add Tamaraw");
                questManager2.currentAnimal += 1;
            }
            else if (whatAnimal == animal2)
            {
                Debug.Log("Add Haribon");
                questManager2.currentAnimal2 += 1;
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
