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

            DeathCount.instanceDeathCount.currentDeaths++;
            DeathCount.instanceDeathCount.UpdateDeaths();
        }
    }
}
