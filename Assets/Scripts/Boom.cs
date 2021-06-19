using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public Rigidbody rb;
    public float explosionForce = 10;
    
    // void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    // }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            rb.AddForce(collision.impulse * explosionForce);
        }
    }
}
