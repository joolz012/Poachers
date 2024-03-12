using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject getKeyCanvas;
    public string keySave = "keySave";
    public float unlockKeyCost;
    public int checkKey; //unlock nextstage
    public int defaultKeySave;

    [Header("Quest")]
    public QuestManager questManager;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetFloat(keySave) < checkKey)
        {
            PlayerPrefs.SetFloat(keySave, defaultKeySave);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetFloat(keySave) >= checkKey)
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
            questManager.currentKey += 1;
            parentGameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("This GameObject has no parent!");
        }
    }
}
