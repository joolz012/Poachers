using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueAnimalTutorial : MonoBehaviour
{
    public GameObject player;
    //private PlayerSkills playerSkills;

    public GameObject rescueCanvas;
    public GameObject tutorialDialog;

    public TutorialSounds2 tutorialSounds2;
    // Start is called before the first frame update
    void Start()
    {
        //playerSkills = GetComponent<PlayerSkills>();

        rescueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
            PlayerPrefs.SetInt("animalCounter", PlayerPrefs.GetInt("animalCounter") + 1);
            PlayerPrefs.SetInt("tarsier", PlayerPrefs.GetInt("tarsier") + 1);
            GameObject parentGameObject = parentTransform.gameObject;
            tutorialDialog.GetComponent<TutorialDialog>().ContinueDialogue();
            player.GetComponent<PlayerMovementStage>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;
            player.GetComponent<PlayerHealth>().enabled = false;
            parentGameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("This GameObject has no parent!");
        }
    }
}
