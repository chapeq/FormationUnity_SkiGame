using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject UIMenu;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI RaceNb;
    public TextMeshProUGUI[] Scores;
    public Animator Transition;
    public GameObject QuitPanel;

    private float[] BestScores;

    private void OnEnable()
    {
        GameEvents.endRace += ShowMenu;
        GameEvents.nexlevel += LoadNextLevel;
        GameEvents.reload += ReloadLevel;
        GameEvents.quit += QuitGame;
    }

    private void OnDisable()
    {
        GameEvents.endRace -= ShowMenu;
        GameEvents.nexlevel -= LoadNextLevel;
        GameEvents.reload -= ReloadLevel;
        GameEvents.quit -= QuitGame;
    }

    private void Start()
    {
        UIMenu.SetActive(false);
        GetSavedScores();
    }

    private void ShowMenu()
    {
        UIMenu.SetActive(true);
        TimeSpan time = TimeSpan.FromSeconds(Timer.time);
        timerText.text = time.ToString(@"mm\:ss\:ff");
        RaceNb.text = "Race num : " + RaceData.Instance.getNbRace();
        UpdateScores();
        ShowScores();
        SaveScores();
    }

    private void ReloadLevel()
    {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));

    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int id)
    {
        UIMenu.SetActive(false);
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(id);
    }

    public void AskConfirmation()
    {
        QuitPanel.SetActive(true);
    }
    public void HideConfiration()
    {
        QuitPanel.SetActive(false);
    }

    private IEnumerator QuitGame()
    {
        QuitPanel.SetActive(false);
        UIMenu.SetActive(false);
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Game has quit");
        Application.Quit();
    }

    private void GetSavedScores()
    {
        BestScores = new float[5];
        for (int i = 0; i < 5; i++)
        {
            BestScores[i] = PlayerPrefs.GetFloat("Score" + i, 0);
        }
        Debug.Log(BestScores);
    }

    private void UpdateScores()
    {
        for (int i = 0; i < 5; i++)
        {
            if ((BestScores[i] == 0) || (Timer.time < BestScores[i]))
            {
                if (i < 4)
                    BestScores[i + 1] = BestScores[i];

                BestScores[i] = Timer.time;
                break;
            }
        }
    }


    private void ShowScores()
    {
        for (int i = 0; i < 5; i++)
        {
            Scores[i].text = TimeSpan.FromSeconds(BestScores[i]).ToString(@"mm\:ss\:ff");
        }
    }

    private void SaveScores()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat("Score" + i, BestScores[i]);
        }
    }

}
