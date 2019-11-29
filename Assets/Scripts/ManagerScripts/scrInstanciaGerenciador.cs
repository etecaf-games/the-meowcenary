using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrInstanciaGerenciador : MonoBehaviour
{
    public GameObject GerenciadorGlobal;
    public GameObject StartButton;
    public GameObject ContinueButton;


    public void Start()
    {
        StartButton = GameObject.Find("StartButton");
        ContinueButton = GameObject.Find("ContinueButton");
        if (GameObject.Find("GerenciadorGlobal(Clone)") == null)
        {
            StartButton.SetActive(true);
            ContinueButton.SetActive(false);
        }
        else
        {
            StartButton.SetActive(false);
            ContinueButton.SetActive(true);
        }
    }

    public void InstanciaGerenciador()
    {
        Instantiate(GerenciadorGlobal);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
