using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UtilLib.Scripts;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float jump = 10;
    public int maxJumps = 1;
    public float VoidThreshold = 0;
    public float gravity = 0.1f;
    public Transform JumpAnimation;
    public AudioClip jumpAudio;
    UIUtils ui;
    ArcadeController cabinet;
    

    private float radius;
    private int jumps;
    private float boost = 0;
    private Vector3 forward_force;
    private Vector3 right_force;
    private Rigidbody rb;
    AudioSource audioSource;
    ParticleSystem.EmissionModule em;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ui = GameObject.Find("UI").GetComponent<UIUtils>();
        if(GameObject.Find("arcade cabinet/Casing") != null)
        {
            //it exists
            cabinet = GameObject.Find("arcade cabinet/Casing").GetComponent<ArcadeController>();
        }
        else{
            cabinet = null;
        }
        
        em = JumpAnimation.GetComponent<ParticleSystem> ().emission;
        audioSource = GetComponent<AudioSource>();
        radius = GetComponent<Collider>().bounds.extents.y; // Gets radius from hitbox
        rb.angularDrag = 0.85F;
        em.enabled = false;
        jumps = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        // Note: checking velY <= 0 prevents bugged double-jumps, but may make you unable
        //       to jump on inclined surfaces
        // Possible solutions:
        //   1. add state (ex. walking up slope, walking on flat ground, etc.),
        //   2. add "ground" objects and use collisions
        //   3. replace rigidbody with character controller (actually pretty doable, since we already have custom gravity)
        // Key Events
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            ui.TogglePaused();
        }
        if(Input.GetButtonDown("Jump") && jumps > 0) {
            rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
            jumps -= 1;
            em.enabled = true;
            StartCoroutine (WaitForJump ());
        }
        // Player state
        if(IsGrounded() && rb.velocity.y <= 9) {
            jumps = maxJumps;
        }
        if(transform.position.y < VoidThreshold && ui.timeFinished == 0f){
            Cursor.lockState = CursorLockMode.None;
		    Cursor.visible = true;
            SceneManager.LoadScene("GameOver");
        }
        // Movement
        if(!cabinet || (cabinet && !cabinet.is_in || cabinet.is_done)){
            forward_force = Camera.main.transform.forward * Input.GetAxis("Vertical");
            right_force = Camera.main.transform.right * Input.GetAxis("Horizontal");
        } else{
            rb.velocity = Vector3.zero;
        }

        if(cabinet && cabinet.is_done){
            maxJumps = 2;
        }

        forward_force.y = 0;
        right_force.y = 0;
        rb.AddForce((forward_force + right_force).normalized * speed * Time.deltaTime * 60);
        // Particles
        JumpAnimation.transform.position = transform.position;
        JumpAnimation.transform.eulerAngles = new Vector3(70, -15, 15);
        if (boost > 0) {
            boost -= Time.deltaTime * 2;
            rb.velocity += new Vector3(0, boost * Time.deltaTime * 60, 0);
        }
        if (boost < 0) { boost = 0; }
    }

    private IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(.1f);
        em.enabled = false;
        audioSource.PlayOneShot(jumpAudio, StateController.Get<float>("SFX", 0.5f)*0.65f); // second parameter is the volume
    }

    void FixedUpdate()
    {
        if (!IsGrounded())
        {
            var p = 1.225f;
            var cd = .34f;
            var a = Mathf.PI * radius * radius;
            var v = rb.velocity.magnitude;

            var direction = -rb.velocity.normalized;
            direction.y = 0;
            var forceAmount = (p * v * v * cd * a) / 2;
            rb.AddForce(direction * forceAmount * Time.fixedDeltaTime * 60);
        }
    }

    

    private bool IsGrounded()
    {
        // Checks if collider is colliding with ground using downward ray (from center to just below collider)
        return Physics.Raycast(transform.position, -Vector3.up, radius + 0.1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trampoline"){
            audioSource.PlayOneShot(jumpAudio, StateController.Get<float>("SFX", 0.5f)*0.55f);
        }
        else if (collision.gameObject.tag == "Zoom"){
            Debug.Log("Ur dumb");
            rb.AddForce(collision.transform.forward * 5000);
        }
    }

    void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Boost")
            {
                boost = 2;
            }
        }
}
