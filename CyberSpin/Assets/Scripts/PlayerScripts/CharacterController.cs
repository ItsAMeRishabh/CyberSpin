using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController insCharCont;
    private Rigidbody2D rb;

    private float currentMoveSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedUpMoveSpeed;
    [SerializeField] private float airSpeed;

    [SerializeField] private float accel;
    [SerializeField] private float deccel;
    [SerializeField] private float velPow;

    private float moveMagnitude;

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

        currentMoveSpeed = moveSpeed;
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        if(CharacterJump.instanceCharacterJump.isGroundedVerticalUp() || CharacterJump.instanceCharacterJump.isGroundedHorizontalRight())
        {
            moveMagnitude = -1;
        }
        else
        {
            moveMagnitude = 1;
        }
    }

    private void FixedUpdate()
    {
        if(gravityVertical)
        {
            //Player control Mid-Air normal
            if (!CharacterJump.instanceCharacterJump.isGroundedVertical() && GravityController.instanceGravityController.gravityDirection ==0)
            {
                if (rb.velocity.y < 50.0f * moveMagnitude)
                {
                    BetterMovementX(moveHorizontal, airSpeed);
                }
            }
            //Player Control On Ground
            if(CharacterJump.instanceCharacterJump.isGroundedVertical())
            {
                BetterMovementX(moveHorizontal, currentMoveSpeed);
            }

            if(CharacterJump.instanceCharacterJump.isGroundedVerticalUp() && GravityController.instanceGravityController.gravityDirection == 2)
            {
                BetterMovementX(moveHorizontal, currentMoveSpeed);
            }
        }

        
        if (!gravityVertical)
        {

            //Player Control On Ground
            if(CharacterJump.instanceCharacterJump.isGroundedHorizontal())
            {
                BetterMovementY(moveHorizontal, currentMoveSpeed);
            }

            
            if(CharacterJump.instanceCharacterJump.isGroundedHorizontalRight() && GravityController.instanceGravityController.gravityDirection == 3)
            {
                BetterMovementY(moveHorizontal, currentMoveSpeed);
            }
        }
    }

    //Horizontal Movement Script
    public void BetterMovementX(float moveDirection, float speed)
    {
        float targetSpeed = moveDirection * speed;

        float speedDif = targetSpeed - rb.velocity.x * moveMagnitude;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right * moveMagnitude);
    }

    public void BetterMovementY(float moveDirection, float speed)
    {
        float targetSpeed = moveDirection * speed;

        float speedDif = targetSpeed + rb.velocity.y * moveMagnitude;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.down * moveMagnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SpeedRamp")
        {
            currentMoveSpeed = speedUpMoveSpeed;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }
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

/*//Player control Mid-Air normal
if (!CharacterJump.instanceCharacterJump.isGroundedHorizontal() && GravityController.instanceGravityController.gravityDirection == 1)
{
    if (rb.velocity.x < 50.0f * moveMagnitude)
    {
        BetterMovementY(moveHorizontal, airSpeed);
    }
}*/