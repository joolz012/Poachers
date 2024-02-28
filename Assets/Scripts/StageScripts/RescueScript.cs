using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RescueScript : MonoBehaviour
{
    private StageManager stageManager;
    public GameObject rescueCanvas;
    // Start is called before the first frame update
    void Start()
    {
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
            GameObject parentGameObject = parentTransform.gameObject;
            PlayerPrefs.SetInt("animalCounter", PlayerPrefs.GetInt("animalCounter") + 1);
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
