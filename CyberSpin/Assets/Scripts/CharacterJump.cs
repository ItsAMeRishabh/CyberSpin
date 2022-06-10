using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    public static CharacterJump instanceCharacterJump;

    Rigidbody2D rb;

    //AIR SPEEDS
    [SerializeField]private float boosterForce;
    [SerializeField]private float jumpForce;

    //MID AIR ACCEL & DECEL
    private float airTime;
    [SerializeField]private float accel;
    [SerializeField]private float deccel;
    [SerializeField]private float velPow;
    [SerializeField]private float fallMultiplier;

    //STAMINA
    private float currentStamina;
    [SerializeField]private float maxStamina;

    //BOOLS
    public bool isJumping;
    private bool isBoosting;

    public float currentGravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        instanceCharacterJump = this;
    }

    private void Start()
    {
        isJumping = false;
        isBoosting = false;

        rb.gravityScale = currentGravity;

        currentStamina = maxStamina;
        StaminaBar.instanceStaminaBar.SetMaxStamina(maxStamina);
    }

    private void Update()
    {
        //MODIFIED FALL
        if (rb.velocity.y < 0 && !isBoosting)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (isJumping)
        {
            airTime += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        
        //GRAVITY SCALE
        rb.gravityScale = currentGravity;

        //NORMAL JUMP
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && !isJumping)
        {

            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);

        }

        //MID_AIR BOOSTING
        else if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) && (currentStamina > 0) && isJumping && airTime > 0.3f)
        {
            //currentGravity = 1f;
            currentStamina -= Time.deltaTime;
            StaminaBar.instanceStaminaBar.SetStamina(currentStamina);
            isBoosting = true;

            BetterBoostMovement();
            //rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            isBoosting = false;
        }

    }

    void BetterBoostMovement()
    {
        float targetSpeed = boosterForce;

        float speedDif = targetSpeed - rb.velocity.y;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.up);
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
