using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBase : MonoBehaviour
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
            if (collider.GetComponent<EnemyHealthBase>() != null)
            {
                EnemyHealthBase enemyHealthBase = collider.GetComponent<EnemyHealthBase>();
                Vector3 directionToEnemy = collider.transform.position - transform.position;
                float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

                if (angleToEnemy <= detectionAngle * 0.5f)
                {
                    Debug.Log("Enemy detected!");

                    enemyHealthBase.TakeDamage(meleeDamage);
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

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
