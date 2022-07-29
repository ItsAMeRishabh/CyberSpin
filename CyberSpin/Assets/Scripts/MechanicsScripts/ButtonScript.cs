using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public static ButtonScript instanceButtonScript;

    private float countdownTimer;
    public bool canActivate;
    public bool isActivated;

    private float level2Countdown;
    private float level3Countdown;

    private void Awake()
    {
        instanceButtonScript = this;
    }

    private void Start()
    {
        countdownTimer = 0f;
        canActivate = false;

        isActivated = false;

        level2Countdown = 10f;
        level3Countdown = 5f;
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

        if (canActivate)
        {
            CharacterController.insCharCont.ballTrailRenderer.enabled = true;
        }
        else
        {
            CharacterController.insCharCont.ballTrailRenderer.enabled = false;
        }
        if(isActivated)
        {
            CharacterController.insCharCont.ballTrailRenderer.enabled = false;
        }
        Debug.Log(isActivated); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "BL2S")
        {
            countdownTimer = level2Countdown;
        }

        if (collision.name == "BL2E")
        {
            if(canActivate)
            {
                isActivated = true;
                canActivate = false;
            }
        }

        if (collision.name == "BL3S")
        {
            countdownTimer = level3Countdown;
        }

        if (collision.name == "BL3E")
        {
            if (canActivate)
            {
                isActivated = true;
                canActivate = false;
            }
        }
    }
}
