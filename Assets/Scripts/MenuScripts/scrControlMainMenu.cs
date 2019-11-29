using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrControlMainMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    

    public void StartGame()
    {
        SceneManager.LoadScene("FirstMissionScene");
    }

    public void GameSettings()
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
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
