using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        [Header("Movement Settings")]
        [Tooltip("The player's current speed.")]
        public float speed;

        [Tooltip("The fastest speed the player can go.")]
        public float speedMaximum;

        [Tooltip("The slowest speed the player can drop to.")]
        public float speedMinimum;

        [Tooltip("How fast the player turns left and right.")]
        public float turnSpeed;

        //TODO (Will): Remove for Course 2 Session 2. Added in video.
        [Tooltip("How much speed the player picks up as they're turning towrds the center.")]
        public float turnAcceleration;

        //TODO (Will): Remove for Course 2 Session 2.
        [Tooltip("How much speed the player drops as they're turning towards the sides.")]
        public float turnDeceleration;

    }

    public Stats playerStats;

    [Tooltip("Keyboard controls for steering left and right.")]
    public KeyCode left, right;

    [Tooltip("Whether the player is moving down hill or not.")]
    public bool isMoving;

    [Tooltip("Child GameObject to check if we are on the ground")]
    public Transform groundCheck;

    [Tooltip("Layermask to hold layers to check for being grounded")]
    public LayerMask groundLayers;
    
    private Rigidbody rb;
 
    private Animator anim;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (!isGrounded)
            return;

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        float y = Mathf.Clamp(transform.eulerAngles.y, 90, 260);
        
        rb.velocity = (transform.forward * vertical) * speed ;
        transform.Rotate((transform.up * horizontal) * rotationSpeed);

        transform.rotation = Quaternion.Euler(0f, y, 0f);
        Debug.Log(rb.velocity);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
        }
    }
    // This function is a callback for when the collider is no longer in contact with a previously collided object.
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = false;
        }
    }
}

