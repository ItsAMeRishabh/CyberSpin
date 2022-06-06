using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMovement : MonoBehaviour
{
    //Rotation Variables
    [SerializeField] private float angularTorqueVal;
    [SerializeField] private float maxAngularVelocity;
    private float currAngularVelocity;

    //Acceleration Variables
    public float accelRatePerSec;
    public float maxSpeed;
    public float ballVelocity;

    //Deceleration Variables
    public float decelRatePerSec;

    //Deceleration Rotation Variables
    public float decelRotRatePerSec;

    private Rigidbody2D rb;

    public bool inputGiven;

    //Time Variables
    public float movetimeMaxToZero;
    public float movetimeZeroToMax;
    public float rottimeMaxToZero;

    //Up Boost Variables
    [SerializeField] private float upBoostSpeed;
    private bool upBoostOn;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputGiven = false;
        upBoostOn = false;

        accelRatePerSec = maxSpeed / movetimeZeroToMax;
        decelRotRatePerSec = -maxAngularVelocity / rottimeMaxToZero;

        //Debugging Decel
        decelRatePerSec = -maxSpeed / movetimeMaxToZero;

        ballVelocity = 0f;
    }

    private void Update()
    {
        currAngularVelocity = rb.angularVelocity;

        if(inputGiven)
        {
            rb.velocity = Vector2.right * ballVelocity;
        }
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A) && !upBoostOn)
        {

            MovementCode(1);

        }
        else if (Input.GetKey(KeyCode.D) && !upBoostOn)
        {

            MovementCode(-1);

        }
        else
        {
            inputGiven = false;
        }

        UpBoostMovement();

    }

    void MovementCode(float mag)
    {
        //Rotation Code
        if (currAngularVelocity < maxAngularVelocity && currAngularVelocity > -maxAngularVelocity)
        {
            rb.AddTorque(angularTorqueVal * mag);
        }

        //Move Script
        ballVelocity += -accelRatePerSec * Time.deltaTime * mag;
        ballVelocity = Mathf.Clamp(ballVelocity, -maxSpeed, maxSpeed);

        inputGiven = true;
    }

    void UpBoostMovement()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            //Debugging Region !

            //rb.velocity = Vector2.zero;
            rb.velocity += Vector2.up * upBoostSpeed;
            upBoostOn = true;
        }
        else
        {
            upBoostOn = false;
        }
    }

    private void LateUpdate()
    {
        if(!inputGiven)
        {
            if (rb.angularVelocity > 0)
            {
                currAngularVelocity += decelRotRatePerSec * Time.deltaTime;
                currAngularVelocity = Mathf.Max(currAngularVelocity, 0f);
                rb.angularVelocity = currAngularVelocity;
            }

            if (rb.angularVelocity < 0)
            {
                currAngularVelocity += -decelRotRatePerSec * Time.deltaTime;
                currAngularVelocity = Mathf.Min(currAngularVelocity, 0f);
                rb.angularVelocity = currAngularVelocity;
            }

            // Decel incomplete
           /* if (rb.velocity.x > 0)
            {
                Debug.Log("Positive");

                ballVelocity += decelRatePerSec * Time.deltaTime;
                ballVelocity = Mathf.Max(ballVelocity, 0f);
                rb.velocity = transform.right * ballVelocity;
            }*/
        }

    }
}
