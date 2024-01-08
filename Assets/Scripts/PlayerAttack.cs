using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private EnemyHealth enemyHit;
    public float meleeDamage;

    [Header("Raycast Attack")]
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
    private const string interactableTag = "Enemy";

    [Header("Attack Cooldown")]
    public float attackCooldown;
    private float attackTimer;
    private bool canHit = true;

    [Header("BoxCast Settings")]
    public Vector3 boxCastHalfExtents = new Vector3(0.25f, 0.25f, 1f); // Default size

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("raid"));
        HandleAttackInput();
        HandleAttackCooldown();
    }

    private void HandleAttackInput()
    {
        if (canHit)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitOnFloor;

            if (Physics.Raycast(ray, out hitOnFloor, Mathf.Infinity, layerMaskInteract))
            {
                Vector3 directionToMouse = (hitOnFloor.point - transform.position).normalized;

                Debug.DrawRay(transform.position, directionToMouse * rayLength, Color.blue);

                RaycastHit hit;

                int mask = LayerMask.GetMask(excludeLayerName) | layerMaskInteract.value;

                if (Physics.BoxCast(transform.position, boxCastHalfExtents, directionToMouse, out hit, Quaternion.identity, rayLength, mask))
                {
                    enemyHit = hit.collider.GetComponent<EnemyHealth>();
                    if (hit.collider.CompareTag(interactableTag))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            enemyHit.TakeDamage(meleeDamage);
                            canHit = false;
                            attackTimer = 0f;
                        }
                    }
                }
                else
                {
                    Debug.Log("Cannot Hit");
                }

                // Visualize the BoxCast
                Debug.DrawRay(transform.position, directionToMouse * rayLength, Color.red);
                Debug.DrawRay(transform.position + new Vector3(boxCastHalfExtents.x, boxCastHalfExtents.y, 0), directionToMouse * rayLength, Color.red);
                Debug.DrawRay(transform.position - new Vector3(boxCastHalfExtents.x, boxCastHalfExtents.y, 0), directionToMouse * rayLength, Color.red);
                Debug.DrawRay(transform.position + new Vector3(boxCastHalfExtents.x, -boxCastHalfExtents.y, 0), directionToMouse * rayLength, Color.red);
                Debug.DrawRay(transform.position - new Vector3(boxCastHalfExtents.x, -boxCastHalfExtents.y, 0), directionToMouse * rayLength, Color.red);
            }
        }
    }

    private void HandleAttackCooldown()
    {
        if (!canHit)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canHit = true;
                attackTimer = 0.0f;
            }
        }
    }
}
