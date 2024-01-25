using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPlayer : MonoBehaviour
{
    public float enemyDmg;
    private PlayerHealth playerHp;

    //shot timing
    private float shotTimer;
    public float shotBtwTimer = 1.0f;
    public bool notAttacking = false;

    public float fireRadius;

    private void Update()
    {
        float distance = Vector3.Distance(PlayerMovement.playerPos, transform.position);

        if (distance <= fireRadius)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        RaycastHit hitPlayer;
        Ray playerPos = new Ray(transform.position, transform.forward);

        if (Physics.SphereCast(playerPos, 0.5f, out hitPlayer, fireRadius))
        {
            //Debug.Log("Attacking");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitPlayer.distance, Color.green);
            if (shotTimer <= 0 && hitPlayer.transform.CompareTag("Player"))
            {
                playerHp = hitPlayer.collider.GetComponent<PlayerHealth>();
                //Debug.Log("Hitting Kurt");
                playerHp.TakeDamage(enemyDmg);
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