using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public Transform JumpAnimation;

    ParticleSystem.EmissionModule em;


    void Start()
    {
        em = JumpAnimation.GetComponent<ParticleSystem> ().emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(0.4f);
        em.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"){
            em.enabled = true;
            StartCoroutine (WaitForJump ());
        }
    }
}
