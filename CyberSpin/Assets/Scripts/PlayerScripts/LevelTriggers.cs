using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTriggers : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Level1Trigger")
        {
            LevelManager.currentLevel = 1;
        }
        if (collision.name == "Level2Trigger")
        {
            LevelManager.currentLevel = 2;
        }
        if (collision.name == "Level3Trigger")
        {
            LevelManager.currentLevel = 3;
        }
        if (collision.name == "Level4Trigger")
        {
            LevelManager.currentLevel = 4;
        }
        if (collision.name == "Level3DeathTrigger")
        {
            LevelManager.instanceLevelManager.toNextPos();
        }
    }
}
