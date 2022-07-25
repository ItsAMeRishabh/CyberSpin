using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public static JumpPad instanceJumpPad;
    public float bounce = 16f;
    public float currentBounce;

    private void Awake()
    {
        instanceJumpPad = this;
    }

    private void Start()
    {
        currentBounce = bounce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * currentBounce, ForceMode2D.Impulse);
        }
    }
}