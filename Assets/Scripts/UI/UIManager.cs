using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPage;
    public GameObject levelSelectPage;
    public GameObject creditsPage;
    public GameObject playButton;
    public GameObject levelSelectedButton;
    public GameObject creditsBackButton;
    public EventSystem eventSystem;
    public GameObject SubmitNavigationEffect;
    public GameObject BaseNavigationEffect;
    public GameObject NavigationEffectContainer;

    public void OnButtonSelect()
    {
        Instantiate(BaseNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());
    }
    
    public void OnPlayButtonClick()
    {
        Instantiate(SubmitNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());

        mainMenuPage.SetActive(false);

        levelSelectPage.SetActive(true);

        eventSystem.SetSelectedGameObject(levelSelectedButton);
    }

    public void OnCreitsButtonClick()
    {
        Instantiate(SubmitNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());

        mainMenuPage.SetActive(false);

        creditsPage.SetActive(true);

        eventSystem.SetSelectedGameObject(creditsBackButton);
    }

    public void OnCreditsBackButtonClick()
    {
        Instantiate(SubmitNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());

        creditsPage.SetActive(false);

        mainMenuPage.SetActive(true);

        eventSystem.SetSelectedGameObject(playButton);
    }

    public void OnQuitButtonClick()
    {
        Instantiate(SubmitNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());

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
        Instantiate(SubmitNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());

        SceneManager.LoadScene(selectedLevelName);
    }

    public void OnLevelSelectBackButtonClick()
    {
        Instantiate(SubmitNavigationEffect, NavigationEffectContainer.GetComponent<Transform>());

        levelSelectPage.SetActive(false);

        mainMenuPage.SetActive(true);

        eventSystem.SetSelectedGameObject(playButton);
    }
}
