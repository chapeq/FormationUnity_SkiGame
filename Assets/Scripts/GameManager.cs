using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject UIMenu;
    public TextMeshProUGUI RaceNb;
    public Animator Transition;
    public GameObject QuitPanel;
    public Slider healthbar;


    private void OnEnable()
    {
        RaceData.numRaceIncrease += UpdateNumRace;
        GameEvents.endRace += ShowMenu;
        GameEvents.nexlevel += LoadNextLevel;
        GameEvents.reload += ReloadLevel;
        GameEvents.quit += QuitGame;
        PlayerEvents.OnHit += UpdateHealthBar;
    }

    private void OnDisable()
    {
        RaceData.numRaceIncrease -= UpdateNumRace;
        GameEvents.endRace -= ShowMenu;
        GameEvents.nexlevel -= LoadNextLevel;
        GameEvents.reload -= ReloadLevel;
        GameEvents.quit -= QuitGame;
        PlayerEvents.OnHit -= UpdateHealthBar;
    }

    private void Start()
    {
        UIMenu.SetActive(false);
        healthbar.value = 1;
    }

    private void ShowMenu()
    {
        UIMenu.SetActive(true);
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
    public void HideConfirmation()
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

    private void UpdateNumRace()
    {
        RaceNb.text = "Race number " + RaceData.Instance.getNbRace();
    }
    private void UpdateHealthBar()
    {
        healthbar.value -= 0.2f;
        if (healthbar.value <= 0)
            ReloadLevel();
    }
}