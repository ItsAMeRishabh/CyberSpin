using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    CharacterController scriptCharacterController;

    [SerializeField]private float fallMultiplier;

    [SerializeField]private float boosterForce;
    [SerializeField]private float jumpForce;

    private float currentStamina;
    [SerializeField]private float maxStamina;

    private bool isJumping;
    private bool isBoosting;

    private float airTime;

    private float currentGravity;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        scriptCharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        isJumping = false;

        rb.gravityScale = currentGravity;

        currentStamina = maxStamina;
        StaminaBar.instanceStaminaBar.SetMaxStamina(maxStamina);
    }

    private void Update()
    {
        if(rb.velocity.y < 0 && !isBoosting)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
        }

        if(isJumping)
        {
            airTime += Time.deltaTime;
        }

        Debug.Log(currentStamina);
        Debug.Log(airTime);
    }

    private void FixedUpdate()
    {
        rb.gravityScale = currentGravity;

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && !isJumping)
        {

            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }

        else if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) && (currentStamina > 0) && isJumping && airTime > 0.3f)
        {
            currentGravity = 1f;
            currentStamina -= Time.deltaTime;
            StaminaBar.instanceStaminaBar.SetStamina(currentStamina);
            rb.AddForce(Vector2.up * boosterForce, ForceMode2D.Force);
            isBoosting = true;
        }
        else
        {
            isBoosting = false;
            currentGravity = 9.8f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            currentStamina = maxStamina;
            StaminaBar.instanceStaminaBar.SetStamina(currentStamina);
            airTime = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
    }
}


// TO DO

//Decelerate when boosting
//Better Mid Air Character control

//JUMP LENGTHS TEST INI

//velocity 26 -> 1.5b
//any vel -> 1.2 Jump Height
//sta -> 1.2 Jump Height
//low ini Vel -> 0.4b