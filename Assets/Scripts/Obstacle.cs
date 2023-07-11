using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
  
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            HitPlayer();
        }
    }

    public virtual void HitPlayer()
    {
        PlayerEvents.PlayerHit();
        Debug.Log("Player hit !! ");
    }
}
