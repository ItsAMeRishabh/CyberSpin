using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitch : MonoBehaviour
{
    //instance
    public static CinemachineSwitch instanceCineSwitch;

    //VirtualCams
    [SerializeField] private CinemachineVirtualCamera zoomOutCam;
    [SerializeField] private CinemachineVirtualCamera playerCam;

    //Variables
    private float timer;
    private bool isSwitching;
    public int currentLevel;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private Transform[] levelPos;


    private void Start()
    {
        instanceCineSwitch = this;
        timer = 0f;
    }

    private void Update()
    {
        SwitchCamera();
    }

    private void SwitchCamera()
    {
        if(isSwitching)
        {
            if (timer > 0)
            {
                zoomOutCam.Priority = 1;
                playerCam.Priority = 0;
                timer -= Time.deltaTime;
            }

            else
            {
                currentLevel++;
                zoomOutCam.Priority = 0;
                playerCam.Priority = 1;
                isSwitching = false;
                toNextPos();
                player.SetActive(true);
            }
        }
        
    }
    
    //Called when Next Level Button Pressed
    public void StartTimer()
    {
        timer = 2.0f;
        isSwitching = true;
        victoryScreen.SetActive(false);
        player.SetActive(false);
    }

    //Updating Pos of Player
    public void toNextPos()
    {
        player.transform.position = levelPos[currentLevel - 1].position;
    }
    public void RetryLevel()
    {
        victoryScreen.SetActive(false);
        toNextPos();
    }
}
