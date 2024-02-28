using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //private EnemyHealth enemyHit;
    public Animator playerAnim;
    public float meleeDamage;
    public string enemyTag = "Enemy";

    [Header("Attack Settings")]
    public float attackCooldown;
    public float attackRange;
    public float detectionAngle;
    private float attackTimer;
    public bool attacking;


    // Update is called once per frame
    void Update()
    {
        if (!attacking && Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (attacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                DetectEnemy();
                attackTimer = 0;
                attacking = false;
            }
        }
    }

    public void Attack()
    {
        attacking = true;
    }

    void DetectEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<EnemyHealth>() != null)
            {
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                // Calculate direction to the enemy
                Vector3 directionToEnemy = collider.transform.position - transform.position;
                float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

                // Check if the enemy is within the specified angle range
                if (angleToEnemy <= detectionAngle * 0.5f)
                {
                    // Enemy is within range and angle, do something (e.g., attack, chase, etc.)
                    Debug.Log("Enemy detected!");

                    // You can add your logic to handle the detected enemy here
                    enemyHealth.TakeDamage(meleeDamage);
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        // Draw a wire sphere to visualize the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw a cone to visualize the attack angle
        DrawAttackAngleGizmo();
    }

    void DrawAttackAngleGizmo()
    {
        float halfAngle = detectionAngle * 0.5f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfAngle, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfAngle, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;

        Gizmos.DrawRay(transform.position, leftRayDirection * attackRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * attackRange);
        Gizmos.DrawLine(transform.position + leftRayDirection * attackRange, transform.position + rightRayDirection * attackRange);
    }
}
