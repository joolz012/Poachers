using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthslider;
    public float playerMaxHealth;
    public float playerHealth;
    public string playAgainScene;

    public GameObject deathCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        playerHealth = playerMaxHealth;
        healthslider.maxValue = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthslider.value = playerHealth;
        if(playerHealth <=  0)
        {
            deathCanvas.SetActive(true);
            Debug.Log("Dead");
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("Base");
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(playAgainScene);
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }
}
