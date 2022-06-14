using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController insCharCont;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float airSpeed;

    [SerializeField] private float accel;
    [SerializeField] private float deccel;
    [SerializeField] private float velPow;

    private float moveHorizontal;


    private void Awake()
    {
        insCharCont = this;
    }

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
        if(!CharacterJump.instanceCharacterJump.isGrounded())
        {
            if (rb.velocity.y < 50.0f)
            {
                BetterMovement(moveHorizontal, airSpeed);
            }
        }
        //Player Control On Ground
        else
        {
            BetterMovement(moveHorizontal, moveSpeed);
        }
    }

    //Horizontal Movement Script
    public void BetterMovement(float moveDirection, float speed)
    {
        float targetSpeed = moveDirection * speed;

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