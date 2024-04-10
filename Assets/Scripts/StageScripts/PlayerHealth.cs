using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Animator playerAnim;
    public PlayerMovementStage playerMovementStage;
    public PlayerAttack playerAttack;

    public Slider healthslider;
    public float playerMaxHealth;
    public float playerHealth;

    public string sceneName;
    public GameObject deathCanvas;
    private bool doOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        playerHealth = playerMaxHealth;
        healthslider.maxValue = playerMaxHealth;
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        healthslider.value = playerHealth;
        if(playerHealth <=  0 && !doOnce)
        {
            playerMovementStage.enabled = false;
            playerAttack.enabled = false;
            playerAnim.Play("Death");
            StartCoroutine(Death());
            doOnce = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("Base");
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        deathCanvas.SetActive(true);
        Debug.Log("Dead");
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }
}
