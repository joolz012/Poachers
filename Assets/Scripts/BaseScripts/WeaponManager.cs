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

    [Header("Weapon Text")]
    public Text attackDmg;
    public Text attackRange;
    public Text attackSpeed;

    [Header("Trap Text")]
    public Text trapDmg;
    public Text trapCooldown;
    public Text trapStunDuration;



    [Header("Upgrade Design")]
    public string targetTag = "Weapon";
    public string targetTag2 = "Trap";
    public string targetTag3 = "Base";
    public string canvasName = "WeaponCanvas";
    [SerializeField] LayerMask mask;
    public GameObject currentUpgradeCanvas;
    public GameObject detailsPanel;
    public bool onDetails;

    public BaseHealth baseHealth;
    public bool isCoroutineRunning = false;
    bool startFund = true;

    private void Start()
    {
        currentMoney = PlayerPrefs.GetFloat("currentMoney");
    }
    private void OnEnable()
    {
        onDetails = false;
    }

    IEnumerator GiveFund()
    {
        while (true)
        {
            yield return new WaitForSeconds(addMoneyTimer);
            PlayerPrefs.SetFloat("currentMoney", +PlayerPrefs.GetFloat("currentMoney") + addMoney);
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
        currentMoney = PlayerPrefs.GetFloat("currentMoney");
        moneyText.text = currentMoney.ToString();
        FundMechanics();
        MouseRaycast();
    }

    void MouseRaycast()
    {
        //Debug.Log(currentMoney);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mask))
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (hit.collider.gameObject.CompareTag(targetTag))
                {
                    Debug.Log("Weapon");
                    GameObject clickedObject = hit.collider.gameObject;
                    if (currentUpgradeCanvas != null && currentUpgradeCanvas.transform.parent != clickedObject.transform)
                    {
                        DisableCanvas(currentUpgradeCanvas);
                    }
                    EnableCanvas(clickedObject);
                    DeclareVariableWeapon(clickedObject);
                }
                if (hit.collider.gameObject.CompareTag(targetTag2))
                {
                    Debug.Log("Trap");
                    GameObject clickedObject = hit.collider.gameObject;
                    if (currentUpgradeCanvas != null && currentUpgradeCanvas.transform.parent != clickedObject.transform)
                    {
                        DisableCanvas(currentUpgradeCanvas);
                    }
                    EnableCanvas(clickedObject);
                    DeclareVariableTrap(clickedObject);
                }
                if (hit.collider.gameObject.CompareTag(targetTag3))
                {
                    DisableCanvas(currentUpgradeCanvas);
                }
            }
        }
        if (currentUpgradeCanvas != null)
        {
            //Debug.Log(currentUpgradeCanvas.name + " Weapon");
        }
    }

    void DeclareVariableWeapon(GameObject currentGameObject)
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

    void DeclareVariableTrap(GameObject currentGameObject)
    {
        if (currentGameObject != null)
        {
            Transform detailsTrans = currentUpgradeCanvas.transform.Find("Details Panel");

            Transform trapDmgText = detailsTrans.Find("Trap Damage 1");
            Transform trapCooldownText = detailsTrans.Find("Trap Cooldown 1");
            Transform trapStunDurationText = detailsTrans.Find("Trap Stun Duration 1");

            detailsPanel = detailsTrans.gameObject;
            trapDmg = trapDmgText.GetComponent<Text>();
            trapCooldown = trapCooldownText.GetComponent<Text>();
            trapStunDuration = trapStunDurationText.GetComponent<Text>();
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
        if (baseHealth.baseLevel > weapon.currentLevel)
        {
            if (weapon != null && currentMoney >= upgradeCost)
            {
                Debug.Log("Upgrading weapon...");
                PlayerPrefs.SetFloat("currentMoney", +PlayerPrefs.GetFloat("currentMoney") - upgradeCost);
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
        else
        {
            Debug.LogWarning("Upgrade Base");
        }


    }

    public void UpgradeTrap(GameObject upgradeTrap)
    {
        TrapScript trapScript = upgradeTrap.GetComponent<TrapScript>();
        TrapUpgradeManager upgradeTrapManager = upgradeTrap.GetComponent<TrapUpgradeManager>();
        if (baseHealth.baseLevel > trapScript.currentLevel)
        {
            if (trapScript != null && currentMoney >= upgradeCost)
            {
                PlayerPrefs.SetFloat("currentMoney", +PlayerPrefs.GetFloat("currentMoney") - upgradeCost);
                upgradeTrapManager.UpgradeTrap(trapScript);

                trapDmg.text = trapScript.trapDmg.ToString();
                trapCooldown.text = trapScript.trapCooldown.ToString();
                trapStunDuration.text = trapScript.trapStunDuration.ToString();
            }
            else
            {
                Debug.LogWarning("WeaponScript component not found in parent of " + upgradeTrap.name);
            }
        }
        else
        {
            Debug.LogWarning("Upgrade Base");
        }

    }
    public void TrapDetails(GameObject detailsTrap)
    {
        onDetails = !onDetails;

        TrapScript trapDetails = detailsTrap.GetComponent<TrapScript>();
        trapDmg.text = trapDetails.trapDmg.ToString();
        trapCooldown.text = trapDetails.trapCooldown.ToString();
        trapStunDuration.text = trapDetails.trapStunDuration.ToString();
        if (trapDetails != null && onDetails)
        {
            detailsPanel.SetActive(true);
        }
        else
        {
            detailsPanel.SetActive(false);
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



