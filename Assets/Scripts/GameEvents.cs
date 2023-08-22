using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public delegate void gameAction();
    public static event gameAction startGame;
    public static event gameAction endGame;

    public static void StartGame()
    {
        if (startGame!= null)
            startGame();
    }

    public static void EndGame()
    {
        if (endGame != null)
            endGame();
    }
}
