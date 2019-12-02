using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrInGameMenu : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject PauseMenu;
    public GameObject MenuTutorialPage;
    public GameObject MenuTutorialPage2;

    void Start() 
    {
        Time.timeScale = 1f;
        PauseButton = GameObject.Find("1stMenu");
        PauseMenu = GameObject.Find("2ndMenu");
        MenuTutorialPage = GameObject.Find("TutorialPage");
        MenuTutorialPage2 = GameObject.Find("TutorialPage2");
        PauseMenu.SetActive(false);
        MenuTutorialPage.SetActive(false);
        MenuTutorialPage2.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GameTutorial()
    {
        MenuTutorialPage.SetActive(true);
        MenuTutorialPage2.SetActive(false);
    }

    public void GameTutorial2()
    {
        MenuTutorialPage.SetActive(false);
        MenuTutorialPage2.SetActive(true);
    }

    public void ExitTutorial()
    {
        MenuTutorialPage.SetActive(false);
        MenuTutorialPage2.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}