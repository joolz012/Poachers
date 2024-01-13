using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [Header("Money")]
    public float currentMoney;
    public float addMoney;
    public float addMoneyTimer;
    public float upgradeCost;
    public Text moneyText;

    [Header("Money Text")]
    public Text attackDmg;
    public Text attackRange;
    public Text attackSpeed;



    [Header("Upgrade Design")]
    public string targetTag = "Weapon";
    public string canvasName = "WeaponCanvas";
    [SerializeField] LayerMask mask;
    public GameObject currentUpgradeCanvas;
    public GameObject detailsPanel;
    public bool onDetails;


    public bool isCoroutineRunning = false;
    bool startFund = true;

    private void OnEnable()
    {
        onDetails = false;
    }

    IEnumerator GiveFund()
    {
        while (true)
        {
            yield return new WaitForSeconds(addMoneyTimer);
            currentMoney += addMoney;
        }
    }
    public void FundMechanics()
    {
        if (isCoroutineRunning && !startFund)
        {
            Debug.Log("Stop");
            StopCoroutine("GiveFund");
            startFund = true;
        }
        else if (!isCoroutineRunning && startFund)
        {
            Debug.Log("Start");
            StartCoroutine("GiveFund");
            startFund = false;
        }
    }
    void Update()
    {
        FundMechanics();

        moneyText.text = currentMoney.ToString();

        //Debug.Log(currentMoney);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mask))
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (hit.collider.gameObject.CompareTag(targetTag))
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    if (currentUpgradeCanvas != null && currentUpgradeCanvas.transform.parent != clickedObject.transform)
                    {
                        DisableCanvas(currentUpgradeCanvas);
                    }
                    EnableCanvas(clickedObject);
                    DeclareVariable(clickedObject);
                }
            }
        }
        if (currentUpgradeCanvas != null)
        {
            //Debug.Log(currentUpgradeCanvas.name + " Weapon");
        }
    }
    void DeclareVariable(GameObject currentGameObject)
    {
        if (currentGameObject != null)
        {
            Transform detailsTrans = currentUpgradeCanvas.transform.Find("Details Panel");

            Transform attackDmgText = detailsTrans.Find("Attack Damage (1)");
            Transform attackRangeText = detailsTrans.Find("Attack Range (1)");
            Transform attackSpeedText = detailsTrans.Find("Attack Speed (1)");

            detailsPanel = detailsTrans.gameObject;
            attackDmg = attackDmgText.GetComponent<Text>();
            attackRange = attackRangeText.GetComponent<Text>();
            attackSpeed = attackSpeedText.GetComponent<Text>();
        }
    }

    public void DisableCanvas(GameObject canvasObject)
    {
        if (canvasObject != null)
        {
            // Only set currentUpgradeCanvas to null if it matches the canvasObject.
            if (currentUpgradeCanvas == canvasObject)
            {
                detailsPanel.SetActive(false);
                onDetails = false;
                currentUpgradeCanvas = null;
            }

            canvasObject.SetActive(false);
        }
    }

    void EnableCanvas(GameObject parentObject)
    {
        Transform canvasTransform = parentObject.transform.Find(canvasName);

        if (canvasTransform != null)
        {
            currentUpgradeCanvas = canvasTransform.gameObject;
            currentUpgradeCanvas.SetActive(true);
            // Debug.Log("Enabled canvas for object: " + parentObject.name);
        }
        else
        {
            Debug.LogWarning("Canvas not found on object: " + parentObject.name);
        }
    }

    public void UpgradeWeapon(GameObject upgradeWeapon)
    {
        WeaponScript weapon = upgradeWeapon.GetComponent<WeaponScript>();
        WeaponUpgradeManager upgradeManager = upgradeWeapon.GetComponent<WeaponUpgradeManager>();

        if (weapon != null && currentMoney >= upgradeCost)
        {
            Debug.Log("Upgrading weapon...");
            currentMoney -= upgradeCost;
            upgradeManager.UpgradeWeapon(weapon);

            attackDmg.text = weapon.attackDamage.ToString();
            attackRange.text = weapon.attackRange.ToString();
            attackSpeed.text = weapon.shotsPerSecond.ToString();
        }
        else
        {
            Debug.LogWarning("WeaponScript component not found in parent of " + upgradeWeapon.name);
        }
    }

    public void WeaponDetails(GameObject detailsWeapon)
    {
        onDetails = !onDetails;

        WeaponScript weaponDetails = detailsWeapon.GetComponent<WeaponScript>();
        attackDmg.text = weaponDetails.attackDamage.ToString();
        attackRange.text = weaponDetails.attackRange.ToString();
        attackSpeed.text = weaponDetails.shotsPerSecond.ToString();
        if (weaponDetails != null && onDetails)
        {
            detailsPanel.SetActive(true);
        }
        else
        {
            detailsPanel.SetActive(false);
        }
    }

    public void UpdateDetails(GameObject detailsUpdate)
    {
        Transform parentGameObject = currentUpgradeCanvas.transform.parent;
        detailsUpdate = parentGameObject.gameObject;
        WeaponScript weaponDetails = detailsUpdate.GetComponent<WeaponScript>();
        if(weaponDetails != null)
        {
            Debug.Log(detailsUpdate);
            attackDmg.text = weaponDetails.attackDamage.ToString();
            attackRange.text = weaponDetails.attackRange.ToString();
            attackSpeed.text = weaponDetails.shotsPerSecond.ToString();
        }
        else
        {
            Debug.LogWarning("WeaponScript component not found ");
        }
    }
}



