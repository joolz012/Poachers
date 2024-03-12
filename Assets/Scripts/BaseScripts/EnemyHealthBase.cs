using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBase : MonoBehaviour
{
    public Animator enemyAnim;
    Collider enemyCollider;

    private EnemyMovement enemyMovement;
    private EnemyAttackBase enemyAttackBase;

    public float health;
    public float maxHealth;
    public Slider slider;
    public Transform healthbar, isoCam;
    public bool nextWave = false;
    private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<Collider>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttackBase = GetComponent<EnemyAttackBase>();

        dead = false;
        slider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextWave)
        {
            maxHealth += maxHealth / 2.0f;
            nextWave = false;
        }

        SetHealth(health);
        healthbar.LookAt(-isoCam.position);
        if (health <= 0 && !dead)
        {
            enemyCollider.enabled = false;
            gameObject.tag = "Untagged";
            enemyMovement.navMeshAgent.SetDestination(transform.position);
            enemyMovement.enabled = false;
            enemyAttackBase.enabled = false;
            StartCoroutine(Death());
            dead = true;
        }
    }

    IEnumerator Death()
    {
        enemyAnim.Play("Death");
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
        yield break;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void TakeDamage(float minus)
    {
        Debug.Log("Hit");
        health -= minus;
    }
}
