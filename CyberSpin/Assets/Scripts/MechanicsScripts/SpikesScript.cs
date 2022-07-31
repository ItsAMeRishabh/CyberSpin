using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelManager.instanceLevelManager.toNextPos();

            if(LevelManager.currentLevel == 3)
            {
                ButtonScript.instanceButtonScript.level3Countdown = 5f;
                ButtonScript.instanceButtonScript.countdownTimer = 0f;

                ButtonScript.instanceButtonScript.canActivate = false;
                ButtonScript.instanceButtonScript.isActivated = false;
            }
            

        }
    }
}
