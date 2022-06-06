using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    CharacterJump scriptCharacterJump;

    private Rigidbody2D rb;

    public float moveSpeed;

    public float moveHorizontal;
    public float moveVertical;

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
