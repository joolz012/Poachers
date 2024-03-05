using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject getKeyCanvas;
    public string keySave = "keySave";
    public float unlockKeyCost;
    public int checkKey = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("keySave") >= checkKey)
        {
            Transform parent = transform.parent;
            parent.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            getKeyCanvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            getKeyCanvas.SetActive(false);
        }
    }

    public void GetKey()
    {
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            GameObject parentGameObject = parentTransform.gameObject;
            PlayerPrefs.SetFloat("keySave", PlayerPrefs.GetFloat("keySave") + unlockKeyCost);
            parentGameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("This GameObject has no parent!");
        }
    }
}
