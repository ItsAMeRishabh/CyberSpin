using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Level2CamSwitch : MonoBehaviour
{
    //instance
    public static Level2CamSwitch instanceLVL2CamSwitch;

    //VirtualCams
    [SerializeField] private CinemachineVirtualCamera section1;
    [SerializeField] private CinemachineVirtualCamera section2;

    //Variables
    public float currentCam;

    private void Awake()
    {
        instanceLVL2CamSwitch = this;
    }

    private void Update()
    {
        SwitchCamera();
    }

    //Cinemachine Camera Switch Script
    private void SwitchCamera()
    {
            if (currentCam == 1)
            {
                section1.Priority = 1;
                section2.Priority = 0;
            }

            else if(currentCam == 2)
            {
                section1.Priority = 0;
                section2.Priority = 1;
            }
    }
}
