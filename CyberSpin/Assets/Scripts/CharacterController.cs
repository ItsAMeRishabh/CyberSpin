using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float airSpeed;

    [SerializeField] private float accel;
    [SerializeField] private float deccel;
    [SerializeField] private float velPow;

    private float moveHorizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        //Player control Mid-Air
        if(CharacterJump.instanceCharacterJump.isJumping)
        {
            if (rb.velocity.y < 50.0f)
            {
                BetterMovement(airSpeed);
            }
        }
        //Player Control On Ground
        else
        {
            BetterMovement(moveSpeed);
        }
    }

    //Horizontal Movement Script
    void BetterMovement(float speed)
    {
        float targetSpeed = moveHorizontal * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

}


//EMERGENCY CODE

//Previous Movement
/*void HorizontalMove(float speed)
{
    if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
    {
        rb.AddForce(new Vector2(moveHorizontal * speed, 0f), ForceMode2D.Force);
    }
}*/