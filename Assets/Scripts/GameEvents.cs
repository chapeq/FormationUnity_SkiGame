using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public delegate void gameAction();
    public static event gameAction startRace;
    public static event gameAction endRace;
    public static event gameAction reload;
    public static event gameAction nexlevel;

    public delegate IEnumerator quitGame();
    public static event quitGame quit;

    public static void StartRace()
    {
        if (startRace!= null)
            startRace();
    }

    public static void EndRace()
    {
        if (endRace != null)
            endRace();
    }

    public void ReloadLevel()
    {
        if (reload != null)
            reload();
    }

    public void NextLevel()
    {
        if (nexlevel != null)
            nexlevel();
    }

    public void QuitGame()
    {
        if( quit != null)
        {
            StartCoroutine(quit());
        }
    }
}
