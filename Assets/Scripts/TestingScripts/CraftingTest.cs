using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTest : MonoBehaviour
{
    public int essenceCounter;
    public int craftEssenceCost;
    public int upgradeTalismanCost;
    public GameObject craftOpener, craftPanel;

    public Text essenceText;
    // Start is called before the first frame update
    void Start()
    {
        essenceCounter = PlayerPrefs.GetInt("essence");
        Debug.Log("Essence: " + essenceCounter);
    }

    // Update is called once per frame
    void Update()
    {
        essenceText.text = PlayerPrefs.GetInt("essence").ToString();

    }
    public void UpdateEssenceCounter(int newValue)
    {
        essenceCounter = newValue;
        PlayerPrefs.SetInt("essence", essenceCounter);
        //PlayerPrefs.Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            craftOpener.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            craftOpener.SetActive(false);
            craftPanel.SetActive(false);
        }
    }
}
