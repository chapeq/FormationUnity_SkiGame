using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceData : MonoBehaviour
{
    private static RaceData instance;
    private int NbRace =0 ;

    public static RaceData Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject); ;
    }
    private void OnEnable()
    {
        GameEvents.endRace += CountRace;
    }

    private void OnDisable()
    {
        GameEvents.endRace -= CountRace;
    }

    private void CountRace()
    {
        NbRace += 1;
    }

    public int getNbRace()
    {
        return NbRace;
    }
}
