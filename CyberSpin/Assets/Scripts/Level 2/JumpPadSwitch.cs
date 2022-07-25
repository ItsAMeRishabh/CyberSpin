using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadSwitch : MonoBehaviour
{
    [SerializeField] private GameObject jumpPad1;
    [SerializeField] private GameObject jumpPad2;

    private void Start()
    {
        jumpPad1.GetComponent<JumpPad>().currentBounce = 0f;
        jumpPad2.GetComponent<JumpPad>().currentBounce = 0f;
    }

    private void Update()
    {
        if(ButtonScript.instanceButtonScript.isActivated)
        {
            jumpPad1.GetComponent<JumpPad>().currentBounce = jumpPad1.GetComponent<JumpPad>().bounce;
            jumpPad2.GetComponent<JumpPad>().currentBounce = jumpPad2.GetComponent<JumpPad>().bounce; ;
        }
        else
        {
            jumpPad1.GetComponent<JumpPad>().currentBounce = 0f;
            jumpPad2.GetComponent<JumpPad>().currentBounce = 0f;
        }
    }
}
