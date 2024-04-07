using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    private float damage;
    private Transform target;


    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }


    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private float startTime;
    private float journeyLength;

    void Start()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        startTime = Time.time;


        journeyLength = Vector3.Distance(transform.position, target.position);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        //float distanceCovered = (Time.time - startTime) * speed;


        //float journeyFraction = distanceCovered / journeyLength;


        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target.position);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == target && collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealthBase enemy = target.GetComponent<EnemyHealthBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
