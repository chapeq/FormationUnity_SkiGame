using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public bool raceStart = false;
    public static float time = 0f;
    private TimeSpan timePlaying;

    private void OnEnable()
    {
        GameEvents.startGame += StartTimer;
        GameEvents.endGame += EndTimer;
    }

    private void OnDisable()
    {
        GameEvents.startGame -= StartTimer;
        GameEvents.endGame -= EndTimer;

    }

    private void StartTimer()
    {
        time = 0f;
        StartCoroutine("IncreaseTime");
        raceStart = true;
    }

    private void EndTimer()
    {
        if (raceStart)
        {
            StopCoroutine("IncreaseTime");
            Debug.Log("Race Time : " + timePlaying.ToString(@"mm\:ss\:ff"));
        }
    }

    private IEnumerator IncreaseTime()
    {
        while (true)
        {
            time += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(time);
            yield return null;
        }

    }

}
