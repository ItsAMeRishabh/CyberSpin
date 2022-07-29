using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialsUI : MonoBehaviour
{
    public static TutorialsUI instanceTutorialsUI;

    [SerializeField] private Animator LVL1tutorialsUIAnim;
    [SerializeField] private GameObject Level1MoveTut;

    private float timer1;
    private float timer2;

    private void Awake()
    {
        instanceTutorialsUI = this;

        timer1 = 3f;
        timer2 = 5f;
    }


    void Update()
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
                timer1 = 0;
            }

            if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
            {
                if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
                {
                    if(timer2 > 0)
                    {
                        timer2 -= Time.deltaTime;
                    }
                    else
                    {
                        LVL1tutorialsUIAnim.SetBool("isTurnedOn", false);
                        LVL1tutorialsUIAnim.SetBool("isTurnedOff", true);
                        timer2 = 0;
                    }
                }
            }
        }
    }
}
