using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider slider;
    public Transform healthbar, isoCam;
    public bool nextWave = false;
    // Start is called before the first frame update
    void Start()
    {
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
        if (health <= 0)
        {
            //death
            Destroy(gameObject);
        }
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

    private void OnDestroy()
    {
    }
}
