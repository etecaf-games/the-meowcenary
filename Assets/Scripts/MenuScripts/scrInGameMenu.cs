using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrInGameMenu : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject PauseMenu;

    void Start() 
    {
        Time.timeScale = 1f;
        PauseButton = GameObject.Find("1stMenu");
        PauseMenu = GameObject.Find("2ndMenu");
        PauseMenu.SetActive(false);
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

    /*public void GameSettings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void ExitSettings()
    {
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void GameTutorial()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }

    public void ExitTutorial()
    {
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }*/

    public void ExitGame()
    {
        Application.Quit();
    }
}