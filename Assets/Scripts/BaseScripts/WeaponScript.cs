using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Weapon Stats")]
    public int currentLevel;
    public float attackDamage;
    public float attackRange;
    public float shotsPerSecond;

    [Header("Weapon Design")]
    public GameObject attackSoundGO;
    private AudioSource attackSound;
    public float rotationSpeed;
    public GameObject bulletPrefab;
    private GameObject target;
    private float timeSinceLastShot;

    public Transform baseTrans;

    private void Start()
    {
        GameObject soundObject = Instantiate(attackSoundGO, transform.position, Quaternion.identity);
        attackSound = soundObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        GameObject enemyNearBase = FindEnemyNearBase();

        if (enemyNearBase != null)
        {
            target = enemyNearBase;
        }
        else
        {
            FindNearestTarget();
        }

        if (target != null)
        {
            AttackTarget();
        }
    }

    GameObject FindEnemyNearBase()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distanceToBase = Vector3.Distance(baseTrans.position, enemy.transform.position);
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToBase < 1000 && distance < attackRange)
            {
                return enemy;
            }
        }

        return null;
    }

    void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float nearestDistance = attackRange;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        target = nearestEnemy;
    }

    void AttackTarget()
    {
        if (target == null)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackRange && Time.time - timeSinceLastShot >= 1 / shotsPerSecond)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            attackSound.Play();
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.SetTarget(target.transform);
                bulletController.SetDamage(attackDamage);
            }

            timeSinceLastShot = Time.time;
        }
    }
}
