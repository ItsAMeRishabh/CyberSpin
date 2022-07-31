using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public static ButtonScript instanceButtonScript;

    public float countdownTimer;
    public bool canActivate;
    public bool isActivated;

    private float level2Countdown;
    public float level3Countdown;

    private float level4Countdown1;
    private float level4Countdown2;

    private bool secondButtonPressed;

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

        level4Countdown1 = 4f;
        level4Countdown2 = 20f;

        secondButtonPressed = false;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(LevelManager.currentLevel == 2)
        {
            if (collision.name == "BL2S")
            {
                countdownTimer = level2Countdown;
            }

            if (collision.name == "BL2E")
            {
                if (canActivate)
                {
                    isActivated = true;
                    canActivate = false;
                }
            }
        }


        if (LevelManager.currentLevel == 3)
        {
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

        if (LevelManager.currentLevel == 4)
        {
            if (collision.name == "BL4S")
            {
                countdownTimer = level4Countdown1;
            }

            if (collision.name == "BL4M")
            {
                if (canActivate)
                {
                    countdownTimer = level4Countdown2;
                    secondButtonPressed = true;
                }
            }

            if (collision.name == "BL4E")
            {
                if (canActivate && secondButtonPressed)
                {
                    isActivated = true;
                    canActivate = false;
                }
            }
        }
    }
}
