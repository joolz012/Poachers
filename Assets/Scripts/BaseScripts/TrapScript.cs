using System.Collections;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private bool canStun = true;

    [Header("Trap Stats")]
    public int currentLevel;
    public float trapDmg;
    public float trapCooldown;
    public float trapStunDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            EnemyHealthBase enemyHealth = other.gameObject.GetComponent<EnemyHealthBase>();

            if (enemyHealth != null && enemy != null && canStun)
            {
                enemy.stunned = true;
                enemy.StunEnemy(trapStunDuration);
                enemyHealth.TakeDamage(trapDmg);
                StartCoroutine(StunCooldown());
            }
            else
            {
                Debug.LogWarning("No EnemyMovement Script or No EnemyHealth Script on: " + other.gameObject.name);
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
