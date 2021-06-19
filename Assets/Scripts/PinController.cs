using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilLib.Scripts;

public class PinController : MonoBehaviour
{
    public AudioClip pinAudio;
    public Transform JumpAnimation;
    
    AudioSource audioSource;
    InterfaceController gameInterface;
    private bool knockedDown;
    private bool onGround;
    private float voidThreshold;
    public float tiltThreshold = 20;
    ParticleSystem.EmissionModule em;

    // Start is called before the first frame update
    void Start()
    {
        gameInterface = GameObject.Find("UI/Interface").GetComponent<InterfaceController>();
        gameInterface.UpdatePinsLeft(1);
        audioSource = GetComponent<AudioSource>();
        onGround = false;
        knockedDown = false;
        voidThreshold = GameObject.Find("Player").GetComponent<PlayerController>().VoidThreshold; //this is stupid, but it works for now

        em = JumpAnimation.GetComponent<ParticleSystem> ().emission;
        em.enabled = false;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (onGround){
            audioSource.PlayOneShot(pinAudio, StateController.Get<float>("SFX", 0.5f)*0.01f);
        }
        else{
            onGround = true;
        }
    }
    
    void Update() {
        if(((transform.eulerAngles.x > tiltThreshold && transform.eulerAngles.x < (360-tiltThreshold) || transform.eulerAngles.z > tiltThreshold && transform.eulerAngles.z < (360-tiltThreshold)) || transform.position.y < voidThreshold) && !knockedDown) {
            knockedDown = true;
            gameInterface.UpdatePinsLeft(-1);

            em.enabled = true;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
