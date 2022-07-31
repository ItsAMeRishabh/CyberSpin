using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Script Instance
    public static CharacterController insCharCont;
    private Rigidbody2D rb;
    [SerializeField] private GameObject childObject;

    //Move Variables
    private float currentMoveSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedUpMoveSpeed;
    [SerializeField] private float airSpeed;

    [SerializeField] private float accel;
    [SerializeField] private float deccel;
    [SerializeField] private float velPow;
    private float moveMagnitude;
    private float moveHorizontal;

    //Bools
    public bool gravityVertical;
    
    //Character Animator 
    [SerializeField] private Animator charAnimator;

    public Vector3 offsetPosition;

    public TrailRenderer ballTrailRenderer;

    private void Awake()
    {
        insCharCont = this;
        rb = GetComponent<Rigidbody2D>();
        ballTrailRenderer = gameObject.GetComponent<TrailRenderer>();
    }

    void Start()
    {
        gravityVertical = true;

        currentMoveSpeed = moveSpeed;

        //Executed when script enabled from Level Select Scene
        if(LevelManager.hasToUpdate)
        {
            LevelManager.instanceLevelManager.toNextPos();              //Setting Level
            ButtonScript.instanceButtonScript.canActivate = false;      //ButtonScript Var
            ButtonScript.instanceButtonScript.isActivated = false;      //ButtonScript Var
            LevelManager.hasToUpdate = false;                           //Reverting Bool to false
        }
    }

    void Update()
    {
        //Input
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        //if normal grounded or horizontal grounded on right wall
        if(CharacterJump.instanceCharacterJump.isGroundedVerticalUp() || CharacterJump.instanceCharacterJump.isGroundedHorizontalRight())
        {
            moveMagnitude = -1;
        }
        else
        {
            moveMagnitude = 1;
        }

        //Animation Trigger - Moving
        if(rb.velocity.x > 15.0f || rb.velocity.x < -15.0f)
        {
            charAnimator.SetBool("isMoving", true);
        }
        else
        {
            charAnimator.SetBool("isMoving", false);
        }

        //Animation Trigger - Boosting
        if (CharacterJump.instanceCharacterJump.isBoosting)
        {
            charAnimator.SetBool("isBoosting", true);
        }
        else
        {
            charAnimator.SetBool("isBoosting", false);
        }

        
    }

    private void FixedUpdate()
    {   

        if (gravityVertical)
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

    private void LateUpdate()
    {
        //childObject.transform.localScale = gameObject.transform.localScale;
        childObject.transform.position = gameObject.transform.position + offsetPosition;
    }

    //Movement Script Up-Down
    public void BetterMovementX(float moveDirection, float speed)
    {
        float targetSpeed = moveDirection * speed;

        float speedDif = targetSpeed - rb.velocity.x * moveMagnitude;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right * moveMagnitude);
    }

    //Movement Script Left-Right
    public void BetterMovementY(float moveDirection, float speed)
    {
        float targetSpeed = moveDirection * speed;

        float speedDif = targetSpeed + rb.velocity.y * moveMagnitude;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.down * moveMagnitude);
    }

    //SpeedRamp Collision
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


#region EMERGENCY CODE

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

#endregion