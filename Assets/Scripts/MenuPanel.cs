using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    public GameObject UIMenu;
    public TextMeshProUGUI timerText;


    private void OnEnable()
    {
        GameEvents.endGame += ShowMenu;
    }

    private void OnDisable()
    {
        GameEvents.endGame -= ShowMenu;
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

    public void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

}
