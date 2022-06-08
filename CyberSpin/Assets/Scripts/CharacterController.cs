using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    CharacterJump scriptCharacterJump;

    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;

    private float moveHorizontal;
    private float moveVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        scriptCharacterJump = GetComponent<CharacterJump>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if(moveHorizontal > 0.1f || moveHorizontal< -0.1f)
        {
            rb.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Force);
        }
    }

}

//TO DO

// Deceleration and Acceleration for player horizontal movement.
