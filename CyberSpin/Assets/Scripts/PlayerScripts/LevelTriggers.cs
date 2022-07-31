using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTriggers : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //LEVEL 1
        if(collision.name == "Level1Trigger")
        {
            LevelManager.currentLevel = 1;
        }

        //LEVEL 2
        if (collision.name == "Level2Trigger")
        {
            LevelManager.currentLevel = 2;
        }

        //LEVEL 3
        if (collision.name == "Level3Trigger")
        {
            LevelManager.currentLevel = 3;
        }

        //LEVEL 4
        if (collision.name == "Level4Trigger")
        {
            LevelManager.currentLevel = 4;
        }

        //Level 3 Death Reset Trigger
        if (collision.name == "Level3DeathTrigger")
        {
            LevelManager.instanceLevelManager.toNextPos();

            DeathCount.instanceDeathCount.currentDeaths++;
            DeathCount.instanceDeathCount.UpdateDeaths();
        }

        if (collision.name == "Level4DeathTrigger")
        {
            LevelManager.instanceLevelManager.toNextPos();

            DeathCount.instanceDeathCount.currentDeaths++;
            DeathCount.instanceDeathCount.UpdateDeaths();
        }
    }
}
