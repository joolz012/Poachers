using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUpgrade : MonoBehaviour
{
    public BaseDefUpgrade[] upgrades; // Assign your upgrades in the inspector
    public int currentUpgradeLevel;

    public GameObject baseUpgradeCanvas, baseDetailsCanvas;
    public bool onCanvas, onDetails;

    public float upgradeCost;

    public Text baseHealthText;

    private void Start()
    {
        currentUpgradeLevel = PlayerPrefs.GetInt("baseLevel");
        BaseHealth baseHealth = GetComponent<BaseHealth>();
        if (currentUpgradeLevel < upgrades.Length)
        {
            BaseDefUpgrade currentUpgrade = upgrades[currentUpgradeLevel];

            baseHealth.baseLevel = currentUpgrade.level;
            baseHealth.baseMaxHealth = currentUpgrade.baseHealth;
            baseHealthText.text = currentUpgrade.baseHealth.ToString();
        }
        else
        {
            Debug.LogWarning("Didn't Set Level");
        }
    }

    public void UpgradeBase(BaseHealth baseHealth)
    {
        if(PlayerPrefs.GetFloat("currentMoney") >= upgradeCost)
        {
            PlayerPrefs.SetFloat("currentMoney", +PlayerPrefs.GetFloat("currentMoney") - upgradeCost);
            if (currentUpgradeLevel < upgrades.Length)
            {
                PlayerPrefs.SetInt("baseLevel", PlayerPrefs.GetInt("baseLevel") + 1);
                currentUpgradeLevel++;
                BaseDefUpgrade currentUpgrade = upgrades[currentUpgradeLevel];

                baseHealth.baseLevel = currentUpgrade.level;
                baseHealth.baseMaxHealth = currentUpgrade.baseHealth;

                baseHealthText.text = currentUpgrade.baseHealth.ToString();
                Debug.Log("Upgraded to level " + currentUpgrade.level);
            }
            else
            {
                Debug.LogWarning("All upgrades applied");
            }
        }
        else
        {
            Debug.LogWarning("No Money");
        }

    }

    public void DegradeWeapon(BaseHealth baseHealth)
    {
        if (currentUpgradeLevel < upgrades.Length)
        {
            PlayerPrefs.SetInt("baseLevel", currentUpgradeLevel);
            BaseDefUpgrade currentUpgrade = upgrades[currentUpgradeLevel];
            Debug.Log("Upgrade level degrade to: " + currentUpgradeLevel);

            baseHealth.baseLevel = currentUpgrade.level;
            baseHealth.baseHealth = currentUpgrade.baseHealth;

            baseHealthText.text = currentUpgrade.baseHealth.ToString();

            Debug.Log("Degraded to level " + currentUpgrade.level);
        }
        else
        {
            Debug.LogWarning("All degrade applied");
        }
    }

    private void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a collider of this GameObject
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Base"))
                {
                    // If the GameObject is clicked, activate the method
                    ActivateObject();
                }
                if (hit.collider.CompareTag("Weapon") || hit.collider.CompareTag("Trap"))
                {
                    // If the GameObject is clicked, activate the method
                    onCanvas = false;
                    baseUpgradeCanvas.SetActive(false);
                    baseUpgradeCanvas.SetActive(false);
                }
            }
        }
    }

    public void BaseDetails()
    {
        baseDetailsCanvas.SetActive(true);
    }

    private void ActivateObject()
    {
        if (!onCanvas)
        {
            onCanvas = true;
            baseUpgradeCanvas.SetActive(true);
        }
    }

    public void DeactivateObject()
    {
        onCanvas = false;
    }
}
