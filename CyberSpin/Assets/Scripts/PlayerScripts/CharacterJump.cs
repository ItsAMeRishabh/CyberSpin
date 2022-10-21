using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    //Script Instance
    public static CharacterJump instanceCharacterJump;

    //Character Components
    Rigidbody2D rb;
    [SerializeField] private CircleCollider2D circleCollider2D;

    //AIR SPEEDS
    [SerializeField] private float boosterForce;
    [SerializeField] private float jumpForceY;
    [SerializeField] private float jumpForceX;

    //MID AIR ACCEL & DECEL
    private float airTime;
    [SerializeField] private float accel;
    [SerializeField] private float deccel;
    [SerializeField] private float velPow;
    [SerializeField] private float fallMultiplier;
    public float currentGravity;

    //STAMINA
    private float currentStamina;
    [SerializeField] private float maxStamina;

    //ForGroundRayCast
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask gravityLayer;

    //Coyote Variables
    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    //BOOLS
    public bool canBoost;
    public bool isBoosting;

    public bool canJump;

    //Misc
    [SerializeField] private ParticleSystem boosterSystem;
    public Animator squashAnimator;

    //Input bools
    bool leftInput;
    bool rightInput;

    bool tooSoon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        instanceCharacterJump = this;
    }

    private void Start()
    {
        //isJumping = false;
        isBoosting = false;

        currentStamina = maxStamina;
        StaminaBar.instanceStaminaBar.SetMaxStamina(maxStamina);

        canBoost = false;
        canJump = true;
    }
    public void Right_FingerDown()
    {
        rightInput = true;
    }
    public void Left_FingerDown()
    {
        leftInput = true;
    }
    public void Right_FingerUp()
    {
        rightInput = false;
    }
    public void Left_FingerUp()
    {
        leftInput = false;
    }

    private void Update()
    {
        //Input
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            leftInput = Input.GetKey(KeyCode.A);
            rightInput = Input.GetKey(KeyCode.D);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {

        }
        //MODIFIED FALL Y
        if (rb.velocity.y < 0 && !isBoosting)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        //Air Time Y
        if (!isGroundedVertical() && GravityController.instanceGravityController.gravityDirection == 0)
        {
            airTime += Time.deltaTime;
            StaminaBar.instanceStaminaBar.SetStamina(currentStamina);
        }

        //Can boost if Level > 4
        if (LevelManager.currentLevel > 4)
        {
            canBoost = true;
        }

        //Coyote Time Restrict
        if (rb.velocity.y > 5f && leftInput && rightInput)
        {
            coyoteTimeCounter = 0f;
        }

        //BOOSTER PARTICLE POSITION SET
        boosterSystem.transform.position = rb.transform.position;
    }

    IEnumerator SoonerCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        tooSoon = false;
    }

    private void FixedUpdate()
    {
        //GRAVITY SCALE
        rb.gravityScale = currentGravity;

        //NORMAL JUMP
        #region NormalJump

        if (canJump && !tooSoon)
        {

            //vertical down Coyote Implemented
            if (leftInput && rightInput && coyoteTimeCounter > 0f && GravityController.instanceGravityController.gravityDirection == 0)
            {
                tooSoon = true;
                StartCoroutine(SoonerCoroutine());
                rb.AddForce(new Vector2(rb.velocity.x, jumpForceY), ForceMode2D.Impulse);
                squashAnimator.SetTrigger("Jumping");
                FindObjectOfType<AudioManager>().Play("Jump");
            }

            //horizontal left
            if (leftInput && rightInput && isGroundedHorizontal() && GravityController.instanceGravityController.gravityDirection == 1)
            {
                tooSoon = true;
                StartCoroutine(SoonerCoroutine());
                rb.AddForce(new Vector2(jumpForceX, rb.velocity.y), ForceMode2D.Impulse);

            }
            //vertical up
            if (leftInput && rightInput && isGroundedVerticalUp() && GravityController.instanceGravityController.gravityDirection == 2)
            {
                tooSoon = true;
                StartCoroutine(SoonerCoroutine());
                rb.AddForce(new Vector2(rb.velocity.x, -jumpForceX), ForceMode2D.Impulse);

            }
            //horizontal right
            if (leftInput && rightInput && isGroundedHorizontalRight() && GravityController.instanceGravityController.gravityDirection == 3)
            {
                tooSoon = true;
                StartCoroutine(SoonerCoroutine());
                rb.AddForce(new Vector2(-jumpForceX, rb.velocity.y), ForceMode2D.Impulse);

            }
        }

        #endregion


        //MID_AIR BOOSTING
        #region Mid-Air Boosting Check
        if ((leftInput && rightInput) && (currentStamina > 0) && !isGroundedVertical() && GravityController.instanceGravityController.gravityDirection == 0 && airTime > 0.5f && canBoost)
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

        //Implemented Coyote Time
        if (isGroundedVertical())
        {
            GravityController.instanceGravityController.gravityDirection = 0;
            CharacterController.insCharCont.gravityVertical = true;
            currentStamina = maxStamina;
            airTime = 0;

            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (isGroundedHorizontal())
        {
            //FindObjectOfType<AudioManager>().Play("Magnet Wall");
            GravityController.instanceGravityController.gravityDirection = 1;
            CharacterController.insCharCont.gravityVertical = false;
            currentStamina = maxStamina;
            airTime = 0;
        }

        if (isGroundedVerticalUp())
        {
            //FindObjectOfType<AudioManager>().Play("Magnet Wall");
            GravityController.instanceGravityController.gravityDirection = 2;
            CharacterController.insCharCont.gravityVertical = true;
            currentStamina = maxStamina;
            airTime = 0;
        }

        if (isGroundedHorizontalRight())
        {
            //FindObjectOfType<AudioManager>().Play("Magnet Wall");
            GravityController.instanceGravityController.gravityDirection = 3;
            CharacterController.insCharCont.gravityVertical = false;
            currentStamina = maxStamina;
            airTime = 0;
        }
        #endregion


        //IF NOT GROUNDED. RESET GRAVITY SCALE
        //Make Function
        #region Reset Gravity Scale When !Grounded
        if (!isGroundedHorizontal() && GravityController.instanceGravityController.gravityDirection == 1)
        {
            ResetGravityDirection();
        }

        if (!isGroundedVerticalUp() && GravityController.instanceGravityController.gravityDirection == 2)
        {
            ResetGravityDirection();
        }

        if (!isGroundedHorizontalRight() && GravityController.instanceGravityController.gravityDirection == 3)
        {
            ResetGravityDirection();
        }
        #endregion

        //More Angular Drag While Boosting
        if (isBoosting)
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

    //Down To Up Movement
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

    //Up To Down Movement
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

    //Left To Right Movement
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

    //Right To Left Movement
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "GravityWall")
        {
            FindObjectOfType<AudioManager>().Play("Magnet Wall");
        }
    }

    //Reset Direction of gravity when jumping from Non-Vertical Platforms
    //Accessed in FixedUpdate
    private void ResetGravityDirection()
    {
        GravityController.instanceGravityController.gravityDirection = 0;
        CharacterController.insCharCont.gravityVertical = true;
    }

    //Old GroundCheck Code
    #region BackUp
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
