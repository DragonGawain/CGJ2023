using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject pauseScreenPage;
    public GameObject pauseDefaultSelected;
    public EventSystem eventSystem;
    public GameObject clearScreenPage;
    public GameObject clearDefaultSelected;
    public GameObject timeDisplayText;

    public void OnPausePressed(InputAction.CallbackContext callbackContext)
    {
        inGameUI.SetActive(false);

        pauseScreenPage.SetActive(true);

        eventSystem.SetSelectedGameObject(pauseDefaultSelected);

        Time.timeScale = 0.0f;
    }
    
    public void OnResumeButtonClick()
    {
        pauseScreenPage.SetActive(false);

        inGameUI.SetActive(true);

        Time.timeScale = 1.0f;
    }

    public void OnLevelClear(float referenceTime, float currentTimeElapsed, int tier)
    {
        inGameUI.SetActive(false);

        var minutes = Mathf.Floor(referenceTime - currentTimeElapsed / 60);
        var seconds = Mathf.Ceil(referenceTime - currentTimeElapsed % 60);
        var timeDisplay = $"{minutes}:{seconds}";

        // Need to show rank image

        clearScreenPage.SetActive(true);

        eventSystem.SetSelectedGameObject(clearDefaultSelected);

        timeDisplayText.GetComponent<Text>().text = timeDisplay;

        Time.timeScale = 0.0f;
    }

    public void OnQuitButtonClick(string mainMenuSceneName)
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OnNextButtonClick(string nextSceneName)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
