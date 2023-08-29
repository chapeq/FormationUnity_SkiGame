using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Animator Transition;
    public CanvasGroup quitDialog;
    public CanvasGroup Buttons;
    public CanvasGroup HelpDialog;
    public AudioSource audio;
    public Image mute;
    public Image unmute;


    public void ClickPlay()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ClickQuit()
    {
        StartCoroutine(QuitGame());
    }

    private IEnumerator LoadLevel(int id)
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(id);
    }
    private IEnumerator QuitGame()
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Game has quit");
        Application.Quit();
    }

    public void askHideConfirmation()
    {
        SwitchCanvasGroup(Buttons);
        SwitchCanvasGroup(quitDialog);
    }

    public void askHideHelp()
    {
        SwitchCanvasGroup(Buttons);
        SwitchCanvasGroup(HelpDialog);
    }

    public void switchMute()
    {
        if (mute.isActiveAndEnabled)
        {
            mute.enabled = false;
            unmute.enabled = true;
            audio.Pause();
        }          
        else
        {
            mute.enabled = true;
            unmute.enabled = false;
            audio.Play();
        }
    }

    private void SwitchCanvasGroup(CanvasGroup cv)
    {
        cv.alpha = cv.alpha == 1 ? 0 : 1;
        cv.interactable = !cv.interactable;
        cv.blocksRaycasts = !cv.blocksRaycasts;

    }

    public void ChangeTitle(TextMeshProUGUI text)
    {
        StartCoroutine(Animtext(text));
    }

    private IEnumerator Animtext(TextMeshProUGUI text)
    {
        while (text.fontSize < 180)
        {
            yield return new WaitForSeconds(0.1f);
            text.fontSize += 20;
        }
        yield return new WaitForSeconds(1f);
        while (text.fontSize > 90)
        {
            yield return new WaitForSeconds(0.1f);
            text.fontSize -= 20;
        }

    }
}
