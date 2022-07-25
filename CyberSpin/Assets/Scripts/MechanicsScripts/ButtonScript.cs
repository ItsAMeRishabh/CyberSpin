using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public static ButtonScript instanceButtonScript;

    private float countdownTimer;
    public bool canActivate;
    public bool isActivated;

    private float level1Countdown;

    private void Awake()
    {
        instanceButtonScript = this;
    }

    private void Start()
    {
        countdownTimer = 0f;
        canActivate = false;
        isActivated = false;

        level1Countdown = 10f;
    }
    private void Update()
    {
        if(countdownTimer>0)
        {
            canActivate = true;
            countdownTimer -= Time.deltaTime;
        }
        else
        {
            canActivate = false;
            countdownTimer = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Button")
        {
            countdownTimer = level1Countdown;
        }

        if (collision.name == "ButtonTrigger")
        {
            if(canActivate)
            {
                isActivated = true;
            }
        }
    }
}
