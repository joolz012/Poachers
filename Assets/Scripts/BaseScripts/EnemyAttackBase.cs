using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBase : MonoBehaviour
{
    public float enemyDmg;
    private BaseHealth baseHp;
    public Transform baseTrans;

    //shot timing
    private float shotTimer;
    public float shotBtwTimer = 1.0f;
    public bool notAttacking = false;

    public float fireRadius;

    private void Update()
    {
        float distance = Vector3.Distance(baseTrans.position, transform.position);

        if (distance <= fireRadius)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        RaycastHit hitBase;
        Ray basePos = new Ray(transform.position, transform.forward);

        if (Physics.SphereCast(basePos, 0.5f, out hitBase, fireRadius))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitBase.distance, Color.green);
            if (shotTimer <= 0 && hitBase.transform.CompareTag("Base"))
            {
                baseHp = hitBase.collider.GetComponent<BaseHealth>();
                //Debug.Log("Attacking");
                baseHp.TakeDamage(enemyDmg);
                //AnimatorControl.isHit = true;
                shotTimer = shotBtwTimer;
            }
            else
            {
                //AnimatorControl.isHit = false;
                shotTimer -= Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("Not Attacking");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.red);
        }
    }
}
