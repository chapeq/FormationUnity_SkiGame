using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostFlags : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerEvents.PlayerBoost();
        }
    }

}
