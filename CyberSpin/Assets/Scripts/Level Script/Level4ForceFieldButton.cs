using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4ForceFieldButton : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Level4Script.instanceLvl4.forceFieldOn = true;
            Level4Script.instanceLvl4.forceFieldCountdown = 10f;
        }
    }
}
