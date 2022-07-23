using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTriggers : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Level1Trigger")
        {
            CinemachineSwitch.instanceCineSwitch.currentLevel = 1;
        }
        if (collision.tag == "Level2Trigger")
        {
            CinemachineSwitch.instanceCineSwitch.currentLevel = 2;
        }
        if (collision.tag == "Level3Trigger")
        {
            CinemachineSwitch.instanceCineSwitch.currentLevel = 3;
        }
        if (collision.tag == "Level4Trigger")
        {
            CinemachineSwitch.instanceCineSwitch.currentLevel = 4;
        }
    }
}
