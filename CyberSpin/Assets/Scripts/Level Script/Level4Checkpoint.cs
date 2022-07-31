using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Checkpoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            LevelManager.instanceLevelManager.lvl4CheckpointTaken = true;
        }
    }
}
