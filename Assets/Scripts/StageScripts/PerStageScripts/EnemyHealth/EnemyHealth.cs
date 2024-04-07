using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [Header("Enemy Stats")]
    public Animator enemyAnim;

    private EnemyAttackPlayer attackPlayer;
    private EnemyPatrol enemyPatrol;

    public float health;
    public float maxHealth;
    public Slider slider;
    public Transform healthbar, isoCam;
    public bool nextWave = false;
    private bool dead;

    [Header("For Boss Only")]
    public string bossPlayerPrefs;
    public bool isBoss = false;

    [Header("Custom Script")]
    public string scriptName;
    public MonoBehaviour script;
    // Start is called before the first frame update

    private void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Stage1")
        {
            scriptName = "QuestManager";
        }
        else if (currentSceneName == "Stage2")
        {
            scriptName = "QuestManager2";
        }
        else if (currentSceneName == "Stage3")
        {
            scriptName = "QuestManager3";
        }
        else if (currentSceneName == "Stage4")
        {
            scriptName = "QuestManager4";
        }
    }
    void Start()
    {
        attackPlayer = GetComponent<EnemyAttackPlayer>();
        enemyPatrol = GetComponent<EnemyPatrol>();

        dead = false;
        health = maxHealth;
        slider.maxValue = maxHealth;

        GameObject questManager = GameObject.Find("QuestCanvas");
        script = questManager.GetComponent(scriptName) as MonoBehaviour;

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
        ScriptCheck();
        if (isBoss)
        {
            PlayerPrefs.SetFloat(bossPlayerPrefs, PlayerPrefs.GetFloat(bossPlayerPrefs) + 1);
            SceneManager.LoadScene("Base");
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

    void ScriptCheck()
    {
        if (script is QuestManager)
        {
            QuestManager questManager = script as QuestManager;
            if(questManager.currentEnemy < questManager.totalEnemy)
            {
                questManager.currentEnemy += 1;
            }
        }
        else if (script is QuestManager2)
        {
            QuestManager2 questManager2 = script as QuestManager2;
            if (questManager2.currentEnemy < questManager2.totalEnemy)
            {
                questManager2.currentEnemy += 1;
            }
        }
        else if (script is QuestManager3)
        {
            QuestManager3 questManager3 = script as QuestManager3;
            if (questManager3.currentEnemy < questManager3.totalEnemy)
            {
                questManager3.currentEnemy += 1;
            }
        }
        else if (script is QuestManager4)
        {
            QuestManager4 questManager4 = script as QuestManager4;
            if (questManager4.currentEnemy < questManager4.totalEnemy)
            {
                questManager4.currentEnemy += 1;
            }
        }
        else
        {
            Debug.LogError("Unexpected component type: " + script.GetType().Name);
        }
    }
}
