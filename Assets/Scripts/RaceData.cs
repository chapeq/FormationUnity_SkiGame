using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceData : MonoBehaviour
{
    private static RaceData instance;
    private int NbRace =0 ;

    public delegate void RaceDataAction();
    public static event RaceDataAction numRaceIncrease;
    public static RaceData Instance { get => instance; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);          
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this); 
        }

    }
    private void OnEnable()
    {
        GameEvents.startRace += CountRace;
    }

    private void OnDisable()
    {
        GameEvents.startRace -= CountRace;
    }

    private void CountRace()
    {
        NbRace += 1;
        numRaceIncrease();
    }

    public int getNbRace()
    {
        return NbRace;
    }
}
