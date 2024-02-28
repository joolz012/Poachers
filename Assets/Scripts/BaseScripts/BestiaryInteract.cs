using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestiaryInteract : MonoBehaviour
{
    public GameObject bestiaryCanvas, bestiaryButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BestiaryButton()
    {
        bestiaryCanvas.SetActive(true);
        bestiaryButton.SetActive(false);
    }
    public void CloseButton()
    {
        bestiaryCanvas.SetActive(false);
        bestiaryButton.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bestiaryButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bestiaryCanvas.SetActive(false);
            bestiaryButton.SetActive(false);
        }
    }
}
