using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Animator enemyAnim;

    private EnemyAttackPlayer attackPlayer;
    private EnemyPatrol enemyPatrol;

    public float health;
    public float maxHealth;
    public Slider slider;
    public Transform healthbar, isoCam;
    public bool nextWave = false;
    private bool dead;

    public string bossPlayerPrefs;
    public bool isBoss = false;
    // Start is called before the first frame update
    void Start()
    {
        attackPlayer = GetComponent<EnemyAttackPlayer>();
        enemyPatrol = GetComponent<EnemyPatrol>();

        dead = false;
        health = maxHealth;
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
            attackPlayer.enabled = false;
            enemyPatrol.enabled = false;
            StartCoroutine(Death());
            dead = true;
        }
    }

    IEnumerator Death()
    {
        enemyAnim.Play("Death");
        yield return new WaitForSeconds(1.0f);
        if (isBoss)
        {
            PlayerPrefs.SetFloat(bossPlayerPrefs, PlayerPrefs.GetFloat(bossPlayerPrefs) + 1);
        }
        Destroy(gameObject);
        yield break;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void TakeDamage(float minus)
    {
        //Debug.Log("Hit");
        health -= minus;
    }
}
