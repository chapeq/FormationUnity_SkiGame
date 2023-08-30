using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool raceStart = false;
    public TextMeshProUGUI timerText;
    public static float time = 0f;
    private TimeSpan timePlaying;
    public AudioClip startEndSound;
    private AudioSource audio;

    private void OnEnable()
    {
        GameEvents.startRace += StartTimer;
        GameEvents.endRace += EndTimer;
    }

    private void OnDisable()
    {
        GameEvents.startRace -= StartTimer;
        GameEvents.endRace -= EndTimer;

    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void StartTimer()
    {   
        time = 0f;
        audio.PlayOneShot(startEndSound);
        StartCoroutine("IncreaseTime");
        raceStart = true;      
    }

    private void EndTimer()
    {
        if (raceStart)
        {
            audio.PlayOneShot(startEndSound);
            StopCoroutine("IncreaseTime");
        }
    }

    private IEnumerator IncreaseTime()
    {
        while (true)
        {
            time += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(time);
            timerText.text = timePlaying.ToString(@"mm\:ss\:ff");
            yield return null;
        }

    }

}
