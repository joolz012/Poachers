using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    public Slider healthslider;
    public float baseMaxHealth;
    public float baseHealth;
    public Transform healthbar, isoCam;

    // Start is called before the first frame update
    void Start()
    {
        baseHealth = baseMaxHealth;
        healthslider.maxValue = baseMaxHealth;
    }


    // Update is called once per frame
    void Update()
    {


        healthbar.LookAt(isoCam.position);
        healthslider.value = baseHealth;
        if (baseHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }


    public void TakeDamage(float damage)
    {
        baseHealth -= damage;
    }
}
