using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingScript : MonoBehaviour
{
   public float radius = 0.0F;
     public float power = 2000.0F;
     public float upwardsModifier = 0.0F;
 
     Vector3 epicentro;
 
     void Start() {
 
         Vector3 explosionPos = transform.position;
         epicentro = explosionPos;
 
     }
 
     void OnCollisionEnter(Collision collision) {
         if (collision.gameObject.tag == "Player") {
             GameObject Player = GameObject.FindWithTag("Player");
             Rigidbody player = Player.GetComponent<Rigidbody>();
             player.AddExplosionForce (power, epicentro, radius, upwardsModifier);
             Debug.Log("Player has collided with Bouncer");
         }
     }
}
