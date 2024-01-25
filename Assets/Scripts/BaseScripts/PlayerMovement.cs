using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Vector3 playerPos;

    //basic
    public Transform cam;
    CharacterController playerCont;
    float turnTime = 0.05f;
    float turnVelocity;

    //movement
    Vector2 movement;
    public float walkSpeed;
    public float sprintSpeed;
    float trueSpeed;

    //jumping
    public float jumpHeight;
    public float gravity;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        playerCont = GetComponent<CharacterController>();
        trueSpeed = walkSpeed;
        StartCoroutine(TrackPlayer());
    }
    IEnumerator TrackPlayer()
    {
        while (true)
        {
            playerPos = gameObject.transform.position;
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (playerCont.isGrounded)
        {
            velocity.y = -2f; // Set to a negative value to ensure immediate contact with the.
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        playerCont.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = walkSpeed;
        }

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerCont.Move(moveDirection.normalized * trueSpeed * Time.deltaTime);
        }
    }
}