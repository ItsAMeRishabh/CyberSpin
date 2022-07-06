using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomGate : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxAngle = 90f;
    void Update()
    {
        if (transform.rotation.eulerAngles.z == 90)
        {
            transform.Rotate(0, 0, -90 * speed * Time.deltaTime);
            Debug.Log("Boom");
        }
    }
}
