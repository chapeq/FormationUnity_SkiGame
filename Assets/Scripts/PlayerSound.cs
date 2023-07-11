using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip collisionSound;
    private AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerEvents.OnHit += PlayCollisionSound; 
    }

    private void OnDisable()
    {
        PlayerEvents.OnHit -= PlayCollisionSound;
    }
  
    private void PlayCollisionSound()
    {
        audiosource.PlayOneShot(collisionSound);
    }
}
