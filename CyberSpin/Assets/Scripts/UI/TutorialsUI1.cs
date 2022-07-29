using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialsUI1 : MonoBehaviour
{
    public static TutorialsUI1 instanceTutorialsUI;

    [SerializeField] private Animator LVL1tutorialsUIAnim;
    [SerializeField] private Animator LVL1tutorialsUIAnimJump;
    [SerializeField] private GameObject Level1MoveTut;
    [SerializeField] private GameObject Level1JumpTut;

    private float timer1;
    private float timer2;
    private float timer3;

    private float value;

    private bool moveDone;
    private bool jumpDone;

    private void Awake()
    {
        instanceTutorialsUI = this;
    }

    private void Start()
    {
        timer1 = 1.5f;
        timer2 = 5f;
        timer3 = 2f;

        value = 0;

        moveDone = false;
        jumpDone = true;
    }


    void Update()
    {
        if(!moveDone)
        {
            Level1MoveTut.SetActive(true);
            Level1JumpTut.SetActive(false);

            moveTutorial();
        }

        if (!jumpDone)
        {
            Level1JumpTut.SetActive(true);

            jumpTutorial();
        }
    }

    private void moveTutorial()
    {
        if (LevelManager.currentLevel == 1)
        {
            if (timer1 > 0)
            {
                timer1 -= Time.deltaTime;
            }
            else
            {
                LVL1tutorialsUIAnim.SetBool("isTurnedOn", true);
                LVL1tutorialsUIAnim.SetBool("isTurnedOff", false);
                timer1 = -1f;

                if (timer2 > 0)
                {
                    timer2 -= Time.deltaTime;
                }
                else
                {
                    if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
                    {
                        value = 1;
                        timer2 = -1;
                    }
                }
                
            }

        }

        if (value == 1)
        {
            LVL1tutorialsUIAnim.SetBool("isTurnedOn", false);
            LVL1tutorialsUIAnim.SetBool("isTurnedOff", true);

            if (timer3 > 0)
            {
                timer3 -= Time.deltaTime;
            }
            else
            {
                Level1MoveTut.SetActive(false);
                timer3 = 0;
                moveDone = true;
                jumpDone = false;
                ResetValues();
            }
        }
    }

    private void jumpTutorial()
    {
        if (LevelManager.currentLevel == 1)
        {
            if (timer1 > 0)
            {
                timer1 -= Time.deltaTime;
            }
            else
            {
                LVL1tutorialsUIAnimJump.SetBool("isTurnedOn", true);
                LVL1tutorialsUIAnimJump.SetBool("isTurnedOff", false);
                timer1 = -1f;
                
                if (timer2 > 0)
                {
                    timer2 -= Time.deltaTime;
                }
                else
                {
                    if ((Input.GetKey(KeyCode.A)) & (Input.GetKey(KeyCode.D)))
                    {
                        value = 1;
                        timer2 = -1f;
                    }
                    
                }
                
            }

        }

        if (value == 1)
        {
            LVL1tutorialsUIAnimJump.SetBool("isTurnedOn", false);
            LVL1tutorialsUIAnimJump.SetBool("isTurnedOff", true);

            if (timer3 > 0)
            {
                timer3 -= Time.deltaTime;
            }
            else
            {
                Level1JumpTut.SetActive(false);
                timer3 = 0;
                moveDone = true;
                jumpDone = true;
            }
        }
    }

    private void ResetValues()
    {
        timer1 = 2f;
        timer2 = 5f;
        timer3 = 2f;

        value = 0;
    }
}
