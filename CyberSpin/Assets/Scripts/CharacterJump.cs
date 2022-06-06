using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    CharacterController scriptCharacterController;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float boosterForce;
    public float jumpForce;

    public float currentStamina;
    public float maxStamina;

    public bool isJumping;

    public float airTime;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        scriptCharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        currentStamina = maxStamina;

        isJumping = false;
    }

    private void Update()
    {
        if(rb.velocity.y < 0)
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
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }

        if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) && (currentStamina > 0) && isJumping && airTime >0.5f)
        {
            currentStamina -= Time.deltaTime;
            rb.AddForce(Vector2.up * boosterForce, ForceMode2D.Force);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            currentStamina = maxStamina;
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
