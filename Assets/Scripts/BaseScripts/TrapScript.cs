using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private bool canStun;

    [Header("Trap Stats")]
    public int currentLevel;
    public float trapDmg;
    public float trapCooldown;
    public float trapStunDuration;
    // Start is called before the first frame update
    void Start()
    {
        canStun = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Stun");
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null && enemy != null && canStun)
            {
                Debug.Log("hit");
                enemyHealth.TakeDamage(trapDmg);
                enemy.StunEnemy(trapStunDuration);
                StartCoroutine(StunCooldown());
            }
            else
            {
                Debug.LogWarning("No EnemyMovement Scipt and No EnemyHealth Scipt");
            }
        }
    }

    IEnumerator StunCooldown()
    {
        canStun = false;
        yield return new WaitForSeconds(trapCooldown);
        canStun = true;
    }
}
