using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPage;
    public GameObject levelSelectPage;
    public GameObject playButton;
    public GameObject levelSelectedButton;
    public EventSystem eventSystem;

    public void OnPlayButtonClick()
    {
        mainMenuPage.SetActive(false);

        levelSelectPage.SetActive(true);

        eventSystem.SetSelectedGameObject(levelSelectedButton);
    }

    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }

    public void OnLevelSelectButtonClick(string selectedLevelName)
    {
        SceneManager.LoadScene(selectedLevelName);
    }

    public void OnLevelSelectBackButtonClick()
    {
        levelSelectPage.SetActive(false);

        mainMenuPage.SetActive(true);

        eventSystem.SetSelectedGameObject(playButton);
    }
}
