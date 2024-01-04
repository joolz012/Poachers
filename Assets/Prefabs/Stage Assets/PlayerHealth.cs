using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthslider;
    public float playerMaxHealth;
    public float playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        healthslider.maxValue = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthslider.value = playerHealth;
        if(playerHealth <=  0)
        {
            Debug.Log("Dead");
        }
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }
}
