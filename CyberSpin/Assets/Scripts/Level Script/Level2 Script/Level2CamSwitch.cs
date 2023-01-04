using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Level2CamSwitch : MonoBehaviour
{
    //instance
    public static Level2CamSwitch instanceCamSwitch;

    //VirtualCams
    [SerializeField] private CinemachineVirtualCamera zoomOutCam;
    [SerializeField] private CinemachineVirtualCamera playerCam;

    //Variables
    private float timer;
    private bool isSwitching;

    //GameObjects
    [SerializeField] private GameObject player;
    public GameObject victoryScreen;

    private void Awake()
    {
        instanceCamSwitch = this;
    }

    private void Update()
    {
        SwitchCamera();
    }

    //Cinemachine Camera Switch Script
    private void SwitchCamera()
    {
        if (isSwitching)
        {
            //While Timer is positive
            if (timer > 0)
            {
                zoomOutCam.Priority = 1;                                //Switch to Zoomed Out Camera
                playerCam.Priority = 0;                                 //Switch off from Player Camera
                timer -= Time.deltaTime;                                //Deducting time for every Time.deltaTime
            }

            else
            {
                LevelManager.currentLevel++;                            //Switch to next level
                zoomOutCam.Priority = 0;                                //Switch off from Zoomed out cam
                playerCam.Priority = 1;                                 //Switch to Player Cam
                LevelManager.instanceLevelManager.toNextPos();          //Update Character Pos to Next Level
                isSwitching = false;

                ButtonScript.instanceButtonScript.isActivated = false;
                ButtonScript.instanceButtonScript.canActivate = false;

                player.GetComponent<CharacterController>().enabled = true;   //Turn Player Scripts on
                player.GetComponent<CharacterJump>().enabled = true;

                timer = 0;
            }
        }

    }
}
