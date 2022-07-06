using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPads : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    
    void Update()
    {
        transform.Rotate(0, 0, -360* speed * Time.deltaTime);
    }
}
