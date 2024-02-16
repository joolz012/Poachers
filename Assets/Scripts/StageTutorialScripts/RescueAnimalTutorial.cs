using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueAnimalTutorial : MonoBehaviour
{
    public GameObject rescueCanvas;
    public GameObject tutorialDialog;
    // Start is called before the first frame update
    void Start()
    {
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
            GameObject parentGameObject = parentTransform.gameObject;
            tutorialDialog.GetComponent<TutorialDialog>().ContinueDialogue();
            parentGameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("This GameObject has no parent!");
        }
    }
}
