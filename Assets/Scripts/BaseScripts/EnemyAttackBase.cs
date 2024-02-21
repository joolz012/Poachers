using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBase : MonoBehaviour
{
    public Animator enemyAnimator;
    public float enemyDamage;
    private BaseHealth baseHealth;
    public Transform baseTransform;

    // Shot timing
    private float timeUntilNextShot;
    public float timeBetweenShots = 1.0f;
    public bool isNotAttacking = false;

    public float fireRadius;


    private void Start()
    {
        timeUntilNextShot = timeBetweenShots;
    }
    private void Update()
    {
        float distance = Vector3.Distance(baseTransform.position, transform.position);

        if (distance <= fireRadius)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        RaycastHit hitBase;

        // Direction from enemy to base
        Vector3 directionToBase = (baseTransform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, directionToBase, out hitBase, fireRadius))
        {
            enemyAnimator.Play("Attack");
            Debug.DrawRay(transform.position, directionToBase * hitBase.distance, Color.green);
            if (timeUntilNextShot <= 0 && hitBase.transform.CompareTag("Base"))
            {
                baseHealth = hitBase.collider.GetComponent<BaseHealth>();
                baseHealth.TakeDamage(enemyDamage);
                //AnimatorControl.isHit = true;
                timeUntilNextShot = timeBetweenShots;
            }
            else
            {
                //AnimatorControl.isHit = false;
                timeUntilNextShot -= Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("Not Attacking");
            Debug.DrawRay(transform.position, directionToBase * fireRadius, Color.red);
        }
    }
}
