using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {      
        if(other.gameObject.tag == "Player")
        {
            StartGame();
        }
    }

    public virtual void StartGame()
    {
        GameEvents.StartRace();
    }
}
