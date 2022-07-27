using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    public static CharacterJump instanceCharacterJump;

    Rigidbody2D rb;
    [SerializeField] private CircleCollider2D circleCollider2D;

    //AIR SPEEDS
    [SerializeField] private float boosterForce;
    [SerializeField] private float jumpForceY;
    [SerializeField] private float jumpForceX;


    //MID AIR ACCEL & DECEL
    private float airTime;
    [SerializeField]private float accel;
    [SerializeField]private float deccel;
    [SerializeField]private float velPow;
    [SerializeField]private float fallMultiplier;

    //STAMINA
    private float currentStamina;
    [SerializeField] private float maxStamina;

    //ForGroundRayCast
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask gravityLayer;


    //BOOLS
    public bool canBoost;
    public bool isBoosting;

    public float currentGravity;

    public ParticleSystem boosterSystem;

    [SerializeField] private Animator squashAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        instanceCharacterJump = this;
    }

    private void Start()
    {
        //isJumping = false;
        isBoosting = false;

        //rb.gravityScale = currentGravity;

        currentStamina = maxStamina;
        StaminaBar.instanceStaminaBar.SetMaxStamina(maxStamina);

        canBoost = true;
    }

    private void Update()
    {
        //Fall Modifiers
        #region FallModifiers
        //MODIFIED FALL Y
        if (rb.velocity.y < 0 && !isBoosting)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        #endregion


        //Air Time Y
        if (!isGroundedVertical() && GravityController.instanceGravityController.gravityDirection == 0)
        {
            airTime += Time.deltaTime;
            StaminaBar.instanceStaminaBar.SetStamina(currentStamina);
        }

        //BOOSTER PARTICLE POSITION SET
        boosterSystem.transform.position = rb.transform.position;

        //if(LevelManager.currentLevel > 3)
        //{
        //    canBoost = true;
        //}

    }

    private void FixedUpdate()
    {
        //GRAVITY SCALE
        rb.gravityScale = currentGravity;


        //NORMAL JUMP
        #region NormalJump
        //vertical down
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && isGroundedVertical() && GravityController.instanceGravityController.gravityDirection == 0)
        {

            rb.AddForce(new Vector2(rb.velocity.x, jumpForceY), ForceMode2D.Impulse);
            squashAnimator.SetTrigger("Jumping");

        }

        //horizontal left
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && isGroundedHorizontal() && GravityController.instanceGravityController.gravityDirection == 1)
        {

            rb.AddForce(new Vector2(jumpForceX, rb.velocity.y), ForceMode2D.Impulse);
            squashAnimator.SetTrigger("Jumping");

        }
        //vertical up
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && isGroundedVerticalUp() && GravityController.instanceGravityController.gravityDirection == 2)
        {

            rb.AddForce(new Vector2(rb.velocity.x, -jumpForceX), ForceMode2D.Impulse);
            squashAnimator.SetTrigger("Jumping");
        }
        //horizontal right
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && isGroundedHorizontalRight() && GravityController.instanceGravityController.gravityDirection == 3)
        {

            rb.AddForce(new Vector2(-jumpForceX, rb.velocity.y), ForceMode2D.Impulse);
            squashAnimator.SetTrigger("Jumping");
        }
        #endregion


        //MID_AIR BOOSTING
        #region Mid-Air Boosting Check
        else if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) && (currentStamina > 0) && !isGroundedVertical() && GravityController.instanceGravityController.gravityDirection == 0 && airTime > 0.5f && canBoost)
        {
            currentStamina -= Time.deltaTime;
            StaminaBar.instanceStaminaBar.SetStamina(currentStamina);
            isBoosting = true;

            BetterBoostMovement();

            boosterSystem.Play();
        }

        else
        {
            isBoosting = false;
            boosterSystem.Stop();
        }
        #endregion


        //Ground Check Bool Updates
        #region Grounded Check
        if (isGroundedVertical())
        {
            GravityController.instanceGravityController.gravityDirection = 0;
            CharacterController.insCharCont.gravityVertical = true;
            currentStamina = maxStamina;
            airTime = 0;
        }

        if (isGroundedHorizontal())
        {
            GravityController.instanceGravityController.gravityDirection = 1;
            CharacterController.insCharCont.gravityVertical = false;
            currentStamina = maxStamina;
            airTime = 0;
        }

        if (isGroundedVerticalUp())
        {
            GravityController.instanceGravityController.gravityDirection = 2;
            CharacterController.insCharCont.gravityVertical = true;
            currentStamina = maxStamina;
            airTime = 0;
        }

        if (isGroundedHorizontalRight())
        {
            GravityController.instanceGravityController.gravityDirection = 3;
            CharacterController.insCharCont.gravityVertical = false;
            currentStamina = maxStamina;
            airTime = 0;
        }
        #endregion


        //IF NOT GROUNDED. RESET GRAVITY SCALE
        #region Reset Gravity Scale When !Grounded
        if (!isGroundedHorizontal() && GravityController.instanceGravityController.gravityDirection == 1)
        {
            GravityController.instanceGravityController.gravityDirection = 0;
            CharacterController.insCharCont.gravityVertical = true;
        }

        if (!isGroundedVerticalUp() && GravityController.instanceGravityController.gravityDirection == 2)
        {
            GravityController.instanceGravityController.gravityDirection = 0;
            CharacterController.insCharCont.gravityVertical = true;
        }

        if (!isGroundedHorizontalRight() && GravityController.instanceGravityController.gravityDirection == 3)
        {
            GravityController.instanceGravityController.gravityDirection = 0;
            CharacterController.insCharCont.gravityVertical = true;
        }
        #endregion

        if(isBoosting)
        {
            rb.angularDrag = 2f;
        }
        else
        {
            rb.angularDrag = 0.05f;
        }

    }

    //BOOST MOVEMENT SCRIPT
    #region BoostMovement
    void BetterBoostMovement()
    {
        float targetSpeed = boosterForce;

        float speedDif = targetSpeed - rb.velocity.y;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.up);
    }
    #endregion

    //RAYCAST BASED GROUND CHECKs
    #region New GroundChecks
    public bool isGroundedVertical()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(circleCollider2D.bounds.center, Vector2.down, circleCollider2D.bounds.extents.y + extraHeight, groundLayer);
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(circleCollider2D.bounds.center, Vector2.down * (circleCollider2D.bounds.extents.y + extraHeight));
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    public bool isGroundedVerticalUp()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(circleCollider2D.bounds.center, Vector2.up, circleCollider2D.bounds.extents.y + extraHeight, gravityLayer);
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(circleCollider2D.bounds.center, Vector2.up * (circleCollider2D.bounds.extents.y + extraHeight));
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    public bool isGroundedHorizontal()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(circleCollider2D.bounds.center, Vector2.left, circleCollider2D.bounds.extents.y + extraHeight, gravityLayer);
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(circleCollider2D.bounds.center, Vector2.left * (circleCollider2D.bounds.extents.y + extraHeight));
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    public bool isGroundedHorizontalRight()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(circleCollider2D.bounds.center, Vector2.right, circleCollider2D.bounds.extents.y + extraHeight, gravityLayer);
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(circleCollider2D.bounds.center, Vector2.right * (circleCollider2D.bounds.extents.y + extraHeight));
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }
    #endregion

    //Old GroundCheck Code
    #region
    /*private void OnTriggerEnter2D(Collider2D collision)
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
   }*/
    #endregion
}
