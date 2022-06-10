using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private float gravityDirection;
    //INPUTS -> +1       -9.8y  //
    //          -1        9.8y 
    //          +2       -9.8x
    //          -2        9.8x

    [SerializeField] private bool movingHori;
    private float moveVertical;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(gravityDirection == 1)
            {
                Physics2D.gravity = new Vector2(0, 9.8f);
                movingHori = true;
                ChangeHoriInput(movingHori);
            }
            if (gravityDirection == 2)
            {
                Physics2D.gravity = new Vector2(-9.8f, 0);
                movingHori = false;
                ChangeHoriInput(movingHori);
            }
            if (gravityDirection == -2)
            {
                Physics2D.gravity = new Vector2(9.8f, 0);
                movingHori = false;
                ChangeHoriInput(movingHori);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                Physics2D.gravity = new Vector2(0, -9.8f);
                movingHori = true;
                ChangeHoriInput(movingHori);     
        }
    }

    void ChangeHoriInput(bool movingTowards)
    {
        if(!movingTowards)
        {
            CharacterController.insCharCont.BetterMovement(moveVertical, moveSpeed);
        }
    }
}

//TO DO

//change horizontal controls
// "       vertical controls
