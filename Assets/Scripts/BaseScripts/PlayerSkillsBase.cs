using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkillsBase : MonoBehaviour
{
    public EnemyMovement[] enemyMovements;

    // Find all GameObjects with the "Enemy" tag
    GameObject[] enemyObjects;

    [Header("Talisman One")]
    public float talismanOneDefaultCd;
    public float stunDuration;
    private float talismanOneCd;
    private bool talismanOneBool;

    [Header("Talisman Two")]
    public float talismanTwoDefaultCd;
    public float talismanTwoDuration;
    private float talismanTwoCd;
    private bool talismanTwoBool;

    [Header("Talisman Three")]
    public float talismanThreeDefaultCd;
    public float talismanThreeDuration;
    private float talismanThreeCd;
    private bool talismanThreeBool;

    private void Start()
    {
        talismanOneCd = talismanOneDefaultCd;
        talismanTwoCd = talismanTwoDefaultCd;
        talismanThreeCd = talismanThreeDefaultCd;
    }
    void Update()
    {
        TalismanController();
        TalismanCooldown();

    }

    void TalismanController()
    {
        CheckEnemy();
        if (Input.GetKeyDown(KeyCode.Alpha1) && talismanOneCd >= talismanOneDefaultCd)
        {
            talismanOneBool = true;
        }
        if (talismanOneBool)
        {
            foreach(EnemyMovement movement in enemyMovements)
            {
                if (movement != null)
                {
                    movement.stunned = true;
                    movement.StunEnemy(stunDuration);
                }
                else
                {
                    Debug.Log("No Enemy");
                }
            }
            talismanOneCd = 0;
            talismanOneBool = false;
        }
    }

    void TalismanCooldown()
    {
        if (talismanOneCd <= talismanOneDefaultCd)
        {
            talismanOneCd += Time.deltaTime;
        }
    }

    void CheckEnemy()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");

        // List to hold found scripts
        List<EnemyMovement> foundScripts = new List<EnemyMovement>();

        foreach (GameObject obj in taggedObjects)
        {
            EnemyMovement script = obj.GetComponent<EnemyMovement>();
            if (script != null)
            {
                foundScripts.Add(script);
            }
        }

        // Convert list to array
        enemyMovements = foundScripts.ToArray();
    }

}
