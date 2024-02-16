using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorfinTutorialScript : MonoBehaviour
{
    Collider thorfinCollider;
    public GameObject defendDialog;
    public GameObject talkCanvas;
    public DefendManagerScript defendManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        thorfinCollider = GetComponent<Collider>();
        talkCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            talkCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            talkCanvas.SetActive(false);
        }
    }
    public void ThorfinTalk()
    {
        if(defendDialog != null)
        {
            defendManagerScript.AddIndex();
            talkCanvas.SetActive(false);
            defendDialog.GetComponent<DefendDialogScript>().ContinueDialogue();
            thorfinCollider.enabled = false; 
        }
    }
}
