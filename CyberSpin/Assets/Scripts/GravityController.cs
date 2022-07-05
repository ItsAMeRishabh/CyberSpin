using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public static GravityController instanceGravityController;

    public float gravityDirection;

    private void Start()
    {
        instanceGravityController = this;

        gravityDirection = 0;    
    }

    private void Update()
    {
        if(gravityDirection == 0)
        {
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
        if (gravityDirection == 1)
        {
            Physics2D.gravity = new Vector2(-9.8f, 0);
        }
    }
}

//TO DO

//change horizontal controls
// "       vertical controls
