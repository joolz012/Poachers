using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public float currentMoney;
    public float addMoney;
    public float addMoneyTimer;
    public Text moneyText;
    private bool isCoroutineRunning = false;

    public Text attackDmg, attackRange, attackSpeed;

    public string targetTag = "Weapon";
    public string canvasName = "WeaponCanvas";
    [SerializeField] LayerMask mask;
    public GameObject currentUpgradeCanvas, detailsPanel;
    public bool onDetails;

    private void OnEnable()
    {
        detailsPanel.SetActive(false);
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
                }
            }
        }

        if (currentUpgradeCanvas != null)
        {
            Debug.Log(currentUpgradeCanvas.name + " Weapon");
        }
    }

    private void FundMechanics()
    {
        if (PlayerPrefs.GetInt("raid") > 0 && isCoroutineRunning)
        {
            StopCoroutine("GiveFund");
            isCoroutineRunning = false;
        }
        else if (PlayerPrefs.GetInt("raid") == 0 && !isCoroutineRunning)
        {
            StartCoroutine("GiveFund");
            isCoroutineRunning = true;
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

        if (weapon != null && currentMoney >= 100)
        {
            Debug.Log("Upgraded");
            currentMoney -= 100;
            weapon.attackDamage += 10;
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
}



