using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TrapScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;

    private bool canStun = true;

    [Header("Trap Stats")]
    public int currentLevel;
    public float trapDmg;
    public float trapCooldown;
    public float trapStunDuration;

    [Header("UI")]
    public Text levelText;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        levelText.text = currentLevel.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            EnemyHealthBase enemyHealth = other.gameObject.GetComponent<EnemyHealthBase>();

            if (enemyHealth != null && enemy != null && canStun)
            {
                audioSource.PlayOneShot(clip);
                enemy.stunned = true;
                enemy.StunEnemy(trapStunDuration);
                enemyHealth.TakeDamage(trapDmg);
                StartCoroutine(StunCooldown());
            }
            else
            {
                Debug.LogWarning("No EnemyMovement Script or No EnemyHealth Script on: " + other.gameObject.name);
            }
        }
    }

    IEnumerator StunCooldown()
    {
        canStun = false;
        yield return new WaitForSeconds(trapCooldown);
        canStun = true;
    }
}
