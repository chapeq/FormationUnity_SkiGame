using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    [Tooltip("How much force will the player bounce back when he hit an obstacle")]
    public float bounceforce;
    public float recoveryTime;
    public bool isHurt = false;

    private Rigidbody rb;

    private void OnEnable()
    {
        PlayerEvents.OnHit += Knockout;
    }

    private void OnDisable()
    {
        PlayerEvents.OnHit -= Knockout;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Knockout()
    {
        if (!isHurt)
        {
            isHurt = true;
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * -bounceforce);
            rb.AddForce(transform.up * 500);
            Invoke("MoveAgain", recoveryTime);
        }

    }

    void MoveAgain()
    {
        isHurt = false;
    }
}
