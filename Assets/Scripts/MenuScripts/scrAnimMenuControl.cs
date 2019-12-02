using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrAnimMenuControl : MonoBehaviour
{
    public GameObject Menu1stPage;
    public GameObject Menu2ndPage;
    public GameObject FirstMissionSelected;
    public GameObject SecondMissionSelected;
    public GameObject FirstMissionStart;
    public GameObject SecondMissionStart;
    public GameObject MenuTutorialPage;
    public GameObject MenuTutorialPage2;
    public GameObject ScorePage;
    public GameObject ScorePage2;
    public Animator menuAnimator;
    public scrHighScore scriptHighScore;
    public scrHighScore2 scriptHighScore2;
    public scrGerenciadorSons scriptGerenciaSons;

    public Animator HPMedalAnimator;
    public Animator pointsMedalAnimator;
    public Animator timerMedalAnimator;

    public Animator HPMedalAnimator2;
    public Animator pointsMedalAnimator2;
    public Animator timerMedalAnimator2;

    void Awake()
    {
        scriptHighScore = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrHighScore>();
        scriptHighScore2 = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrHighScore2>();
        scriptGerenciaSons = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrGerenciadorSons>();
        scriptHighScore.GetMenuReferences();
        scriptHighScore.GradeResults();
        scriptHighScore2.GetMenuReferences();
        scriptHighScore2.GradeResults();
    }

    void Start()
    {
        Time.timeScale = 1f;
        Menu1stPage = GameObject.Find("1stPage");
        Menu2ndPage = GameObject.Find("2ndPage");
        FirstMissionSelected = GameObject.Find("btnSelect1stMission");
        SecondMissionSelected = GameObject.Find("btnSelect2ndMission");
        FirstMissionStart = GameObject.Find("btnStart1stMission");
        SecondMissionStart = GameObject.Find("btnStart2ndMission");
        MenuTutorialPage = GameObject.Find("TutorialPage");
        MenuTutorialPage2 = GameObject.Find("TutorialPage2");
        ScorePage = GameObject.Find("scorePage");
        ScorePage2 = GameObject.Find("scorePage2");
        menuAnimator = gameObject.GetComponent<Animator>();
        menuAnimator.SetInteger("animMenu", 0);
        Menu1stPage.SetActive(true);
        Menu2ndPage.SetActive(false);
        MenuTutorialPage.SetActive(false);
        MenuTutorialPage2.SetActive(false);
        FirstMissionSelected.SetActive(false);
        SecondMissionSelected.SetActive(false);
        FirstMissionStart.SetActive(false);
        SecondMissionStart.SetActive(false);
        ScorePage.SetActive(false);
        ScorePage2.SetActive(false);
        scriptGerenciaSons.bgmSoundEffects[9].Stop();
        scriptGerenciaSons.bgmSoundEffects[10].Stop();
        scriptGerenciaSons.playTheSoundBGM(9);
    }

    /*void FixedUpdate()
    {
        scriptHighScore.GradeResults();
    }*/

    public void StartGame()
    {
        SceneManager.LoadScene("FirstMissionScene");
    }

    public void StartSecondMission()
    {
        SceneManager.LoadScene("SecondMissionScene");
    }

    public void ReturnToFirstMenu()
    {
        SceneManager.LoadScene("FirstScreenScene");
    }

    public void MissionSelect()
    {
        menuAnimator.SetInteger("animMenu", 1);
        Menu1stPage.SetActive(false);
    }

    public void ReturnToMissionMenu()
    {
        menuAnimator.SetInteger("animMenu", 2);
        Menu2ndPage.SetActive(false);
        FirstMissionSelected.SetActive(false);
        SecondMissionSelected.SetActive(false);
        FirstMissionStart.SetActive(false);
        SecondMissionStart.SetActive(false);
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

    public void Select1stMission()
    {
        FirstMissionStart.SetActive(true);
        ScorePage.SetActive(true);
        FirstMissionSelected.SetActive(false);
        SecondMissionSelected.SetActive(false);
        SecondMissionStart.SetActive(false);
        scriptHighScore.GradeResults();
    }

    public void Exit1stMissionSelect()
    {
        FirstMissionStart.SetActive(false);
        ScorePage.SetActive(false);
        FirstMissionSelected.SetActive(true);
        SecondMissionSelected.SetActive(true);
    }

    public void Select2ndMission()
    {
        SecondMissionStart.SetActive(true);
        ScorePage2.SetActive(true);
        SecondMissionSelected.SetActive(false);
        FirstMissionSelected.SetActive(false);
        FirstMissionStart.SetActive(false);
        scriptHighScore2.GradeResults();
    }

    public void Exit2ndMissionSelect()
    {
        SecondMissionStart.SetActive(false);
        ScorePage2.SetActive(false);
        FirstMissionSelected.SetActive(true);
        SecondMissionSelected.SetActive(true);
    }

    public void FirstPageTrigger()
    {
        Menu2ndPage.SetActive(true);
        FirstMissionSelected.SetActive(true);
        SecondMissionSelected.SetActive(true);
    }

    public void SecondPageTrigger()
    {
        Menu1stPage.SetActive(true);
        FirstMissionSelected.SetActive(false);
        SecondMissionSelected.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
