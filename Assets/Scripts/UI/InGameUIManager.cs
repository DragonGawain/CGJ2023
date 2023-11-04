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
    public GameObject SubmitNavigationEffect;
    public GameObject BaseNavigationEffect;
    public GameObject levelClearEffect;
    public GameObject NavigationEffectContainer;

    public void OnButtonSelect()
    {
        Instantiate(BaseNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());
    }

    public void OnPausePressed(InputAction.CallbackContext callbackContext)
    {
        inGameUI.SetActive(false);

        pauseScreenPage.SetActive(true);

        eventSystem.SetSelectedGameObject(pauseDefaultSelected);

        Time.timeScale = 0.0f;
    }
    
    public void OnResumeButtonClick()
    {
        Instantiate(SubmitNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());

        pauseScreenPage.SetActive(false);

        inGameUI.SetActive(true);

        Time.timeScale = 1.0f;
    }

    public void OnLevelClear(float referenceTime, float currentTimeElapsed, int tier)
    {
        Instantiate(levelClearEffect, NavigationEffectContainer.GetComponent<Transform>());

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
        Instantiate(SubmitNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OnNextButtonClick(string nextSceneName)
    {
        Instantiate(SubmitNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());

        SceneManager.LoadScene(nextSceneName);
    }
}
