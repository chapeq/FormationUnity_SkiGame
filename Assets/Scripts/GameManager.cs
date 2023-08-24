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
    public Animator Transition;
    public GameObject QuitPanel;


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
    }

    private void ShowMenu()
    {
        UIMenu.SetActive(true);
        TimeSpan time =  TimeSpan.FromSeconds(Timer.time);
        timerText.text = time.ToString(@"mm\:ss\:ff");
    }

    private void ReloadLevel()
    {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
     
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex +1 ));
    }

    private IEnumerator LoadLevel(int id)
    {
        UIMenu.SetActive(false);
        Transition.SetTrigger("Start");
        yield return new  WaitForSeconds(1.1f);
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

}
