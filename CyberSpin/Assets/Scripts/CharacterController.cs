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

    public bool gravityVertical;

    private void Awake()
    {
        insCharCont = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gravityVertical = true;
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if(gravityVertical)
        {
            //Player control Mid-Air
            if (!CharacterJump.instanceCharacterJump.isGroundedVertical())
            {
                if (rb.velocity.y < 50.0f)
                {
                    BetterMovementX(moveHorizontal, airSpeed);
                }
            }
            //Player Control On Ground
            else
            {
                BetterMovementX(moveHorizontal, moveSpeed);
            }
        }

        
        if (!gravityVertical)
        {
            //Player control Mid-Air
            if (!CharacterJump.instanceCharacterJump.isGroundedHorizontal())
            {
                if (rb.velocity.x < 50.0f)
                {
                    BetterMovementY(moveHorizontal, airSpeed);
                }
            }
            //Player Control On Ground
            else
            {
                BetterMovementY(moveHorizontal, moveSpeed);
            }
        }
    }

    //Horizontal Movement Script
    public void BetterMovementX(float moveDirection, float speed)
    {
        float targetSpeed = moveDirection * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    public void BetterMovementY(float moveDirection, float speed)
    {
        float targetSpeed = moveDirection * speed;

        float speedDif = targetSpeed + rb.velocity.y;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.down);
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