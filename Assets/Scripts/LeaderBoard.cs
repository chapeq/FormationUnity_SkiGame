using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    public int level = 0; 
    public string[] formattedTimes;
    public TextMeshProUGUI[] Scores;
    public TextMeshProUGUI ScoreTime;

    private List<float> savedTimes = new List<float>(new float[5]);

    private void OnEnable()
    {
        GameEvents.endRace += CheckRaceTime;
    }

    private void OnDisable()
    {
        GameEvents.endRace -= CheckRaceTime;

    }
    // Start is called before the first frame update
    void Start()
    {
        formattedTimes = new string[5];
        CheckIfPrefs();
        GetBestScores();
    }

    private void CheckIfPrefs()
    {
        for (int i = 4; i >= 0; i--)
        {
            if(!PlayerPrefs.HasKey("Score" + i+"_"+level))
            {
                PlayerPrefs.SetFloat("Score" + i + "_" + level, 0);
            }

        }
    }

    private void GetBestScores()
    {
        for (int i = 4; i >= 0; i--)
        {
            savedTimes[i] = PlayerPrefs.GetFloat("Score" + i + "_" + level, 0);
        }

        FormatTimesToString();
    }

    private void FormatTimesToString()
    {
        for (int i = 4; i >= 0; i--)
        {
            TimeSpan t = TimeSpan.FromSeconds(savedTimes[i]);
            formattedTimes[i] = t.ToString("mm':'ss':'ff");
        }
    }

    private void CheckRaceTime()
    {
        int scorePosition = int.MaxValue;
        bool highscore = false;

        for (int i = 4; i >= 0; i--)
        {
            if (Timer.time < savedTimes[i] || savedTimes[i] == 0)
            {
                highscore = true;
                if (i < scorePosition)
                    scorePosition = i;
            }
        }

        if (highscore)
        {
            savedTimes.Insert(scorePosition, Timer.time);
            SetBestTimes();
        }

        DisplayTimes(highscore,scorePosition);
    }

    private void DisplayTimes(bool highscore, int scorePosition)
    {
        TimeSpan t = TimeSpan.FromSeconds(Timer.time);
        ScoreTime.text = t.ToString("mm':'ss':'ff");
        for (int i = 4; i >= 0; i--)
        {
            Scores[i].text = formattedTimes[i];
        }
        if (highscore)
        {
            Scores[scorePosition].color = Color.yellow;
            Scores[scorePosition].fontStyle = FontStyles.Bold;
        }
    }

    private void SetBestTimes()
    {
        for (int i = 4; i >= 0; i--)
        {
            PlayerPrefs.SetFloat("Score" + i + "_" + level, savedTimes[i]);
        }
        FormatTimesToString();
    }
}
