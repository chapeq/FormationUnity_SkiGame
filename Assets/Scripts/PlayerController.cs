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

        [Tooltip("How much speed can be added when boost is activated.")]
        public float boostSpeed;

        [Tooltip("How much time do the player have to wait between each boost activation.")]
        public float boostTimer;

    }

    public Stats playerStats;

    [Tooltip("Keyboard controls for steering left and right.")]
    public KeyCode left, right, boost;

    [Tooltip("Whether the player is moving down hill or not.")]
    public bool isMoving;

    [Tooltip("Child GameObject to check if we are on the ground")]
    public Transform groundCheck;

    [Tooltip("Layermask to hold layers to check for being grounded")]
    public LayerMask groundLayers;

    private Rigidbody rb;

    private Animator anim;

    private PlayerDamage damage;

    private float timer;

    private float initMaxSpeed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        damage = gameObject.GetComponent<PlayerDamage>();
        timer = 0;
        initMaxSpeed = playerStats.speedMaximum;
    }

    private void OnEnable()
    {
        PlayerEvents.OnBoost += TriggerBoost;
    }

    private void OnDisable()
    {
        PlayerEvents.OnBoost -= TriggerBoost;
    }

    private void Update()
    {
      
        if (timer > 0)
            timer -= Time.deltaTime;

        if (isMoving && !damage.isHurt)
        {
            bool OnGround = Physics.Linecast(transform.position, groundCheck.position, groundLayers);

            if (OnGround)
            {
                if (Input.GetKey(left))
                {
                    TurnLeft();
                }
                if (Input.GetKey(right))
                {
                    TurnRight();
                }
                if (Input.GetKey(boost) && (timer <= 0))
                {
                    timer = playerStats.boostTimer;
                    playerStats.speedMaximum = playerStats.speedMaximum * 2;
                    StartCoroutine(BoostUp());
                }
            }
        }
    }

    void FixedUpdate()
    {
        ControlSpeed();

        if (isMoving && !damage.isHurt)
        {
            // increase or decrease the players speed depending on how much they are facing downhill
            float turnAngle = Mathf.Abs(180 - transform.eulerAngles.y);
            playerStats.speed += Remap(0, 90, playerStats.turnAcceleration, -playerStats.turnDeceleration, turnAngle);

            Vector3 velocity = (transform.forward) * playerStats.speed * Time.deltaTime;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }

        anim.SetFloat("playerSpeed", playerStats.speed);
    }

    private void TurnLeft()
    {
        if (transform.eulerAngles.y > 91)
        {
            transform.Rotate(new Vector3(0, -playerStats.turnSpeed, 0) * Time.deltaTime, Space.Self);
        }
    }

    private void TurnRight()
    {
        if (transform.eulerAngles.y < 269)
        {
            transform.Rotate(new Vector3(0, playerStats.turnSpeed, 0) * Time.deltaTime, Space.Self);
        }
    }
    private void TriggerBoost()
    {
        playerStats.speedMaximum = playerStats.speedMaximum * 2;
        StartCoroutine(BoostUp());
    }

    private IEnumerator BoostUp()
    {
        rb.AddForce((transform.forward) * (playerStats.speed + playerStats.boostSpeed));
        yield return new WaitForSeconds(3.0f);
        playerStats.speedMaximum = initMaxSpeed;
    }

    private void ControlSpeed()
    {
        if (playerStats.speed > playerStats.speedMaximum)
        {
            playerStats.speed = playerStats.speedMaximum;
        }
        if (playerStats.speed < playerStats.speedMinimum)
        {
            playerStats.speed = playerStats.speedMinimum;
        }
    }

    // remaps a number from a given range into a new range
    private float Remap(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        return (NewValue);
    }

 
  
}

