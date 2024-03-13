using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorfinTutorialScript : MonoBehaviour
{
    Collider thorfinCollider;
    public GameObject defendDialog;
    public GameObject[] talkCanvas;
    public DefendManagerScript defendManagerScript;
    public TutorialSounds3 tutorialSounds3;
    // Start is called before the first frame update
    void Start()
    {
        thorfinCollider = GetComponent<Collider>();
        talkCanvas[0].SetActive(false);
        talkCanvas[1].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            talkCanvas[0].SetActive(true);
            talkCanvas[1].SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            talkCanvas[0].SetActive(false);
            talkCanvas[1].SetActive(true);
        }
    }
    public void ThorfinTalk()
    {
        if(defendDialog != null)
        {
            tutorialSounds3.indexSounds += 1;
            tutorialSounds3.isPlaying = false;
            defendManagerScript.AddIndex();
            talkCanvas[2].SetActive(false);
            defendDialog.GetComponent<DefendDialogScript>().ContinueDialogue();
            thorfinCollider.enabled = false; 
        }
    }
}
