using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrHighScore2 : MonoBehaviour
{
    public scrpontuacao scriptPontuacao;
    public Player scriptPlayer;
    public scrAnimMenuControl scriptAnimMenu;

    public float HPHighScore;
    public float pontosHighScore;
    public float timerHighScore;

    public float HPCurrentScore;
    public float pontosCurrentScore;
    public float timerCurrentScore;

    //set these values for each Mission in the Editor
    public float pontosTargetScore;
    public float timerTargetScore;

    public Text txtHPHighScore;
    public Text txtPontosHighScore;
    public Text txtTimerHighScore;

    public Text txtHPCurrentScore;
    public Text txtPontosCurrentScore;
    public Text txtTimerCurrentScore;

    public Text txtHPGrade;
    public Text txtPontosGrade;
    public Text txtTimerGrade;

    public TimeSpan uh;

    public bool currentWasUnseen;
    public bool alltimeWasUnseen = false;

    public GameObject instanceRef;

    public void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (instanceRef != this)
            Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        timerHighScore = 420;
        pontosHighScore = 0;
        HPHighScore = 0;
        timerCurrentScore = 420;
        pontosCurrentScore = 0;
        HPCurrentScore = 0;
        pontosTargetScore = 420;
        timerTargetScore = 0;
    }

    public void GetSceneReferences()
    {
        scriptPlayer = GameObject.Find("Player").gameObject.GetComponent<Player>();
        scriptPontuacao = GameObject.Find("Gerenciador").gameObject.GetComponent<scrpontuacao>();
        pontosTargetScore = GameObject.Find("Gerenciador").gameObject.GetComponent<scrpontuacao>().missionTargetPontos;
        timerTargetScore = GameObject.Find("Gerenciador").gameObject.GetComponent<scrpontuacao>().missionTargetTimer;
    }

    public void GetMenuReferences()
    {
        scriptAnimMenu = GameObject.Find("MenuFundo").gameObject.GetComponent<scrAnimMenuControl>();
        txtHPHighScore = GameObject.Find("HPHighScore2").gameObject.GetComponent<Text>();
        txtPontosHighScore = GameObject.Find("PointsHighScore2").gameObject.GetComponent<Text>();
        txtTimerHighScore = GameObject.Find("TimeHighScore2").gameObject.GetComponent<Text>();
    }

    public void GatherResults()
    {
        HPCurrentScore = scriptPlayer.playerHealth;
        pontosCurrentScore = scriptPontuacao.pontos;
        timerCurrentScore = scriptPontuacao.timer;
        currentWasUnseen = scriptPontuacao.wasUnseen;
    }

    public void CompareResults()
    {
        if (HPCurrentScore > HPHighScore)
        {
            HPHighScore = HPCurrentScore;
            Debug.Log("new hp record: " + HPHighScore);
        }
        if (pontosCurrentScore > pontosHighScore)
        {
            pontosHighScore = pontosCurrentScore;
            Debug.Log("new points record: " + pontosHighScore);
        }
        if (timerCurrentScore < timerHighScore)
        {
            timerHighScore = timerCurrentScore;
            Debug.Log("new time record: " + timerHighScore);
        }
        if (currentWasUnseen == true && alltimeWasUnseen == false)
        {
            alltimeWasUnseen = true;
        }
    }

    public void GradeResults()
    {
        //checking HP values
        if (HPCurrentScore >= 6)
        {
            scriptAnimMenu.HPMedalAnimator2.SetInteger("medalAnim", 2);
            //Debug.Log("Hp: Gold");
        }
        else if (HPCurrentScore >= 4)
        {
            scriptAnimMenu.HPMedalAnimator2.SetInteger("medalAnim", 1);
            //Debug.Log("Hp: Silver");
        }
        else if (HPCurrentScore >= 2)
        {
            scriptAnimMenu.HPMedalAnimator2.SetInteger("medalAnim", 0);
            //Debug.Log("Hp: Bronze");
        }
        else
        {
            scriptAnimMenu.HPMedalAnimator2.SetInteger("medalAnim", 0);
            //Debug.Log("Hp: Trash");
        }

        //checking point values
        /*if (currentWasUnseen == true)
        {
            //Debug.Log("Was Unseen");
        }
        else
        {
            //Debug.Log("Was Seen");
            //pontosCurrentScore *= 0.8f;
        }*/
        if (pontosCurrentScore >= pontosTargetScore)
        {
            scriptAnimMenu.pointsMedalAnimator2.SetInteger("medalAnim", 2);
            //Debug.Log("Score: Gold");
        }
        else if (pontosCurrentScore >= (pontosTargetScore * 0.66f))
        {
            scriptAnimMenu.pointsMedalAnimator2.SetInteger("medalAnim", 1);
            //Debug.Log("Score: Silver");
        }
        else if (pontosCurrentScore >= (pontosTargetScore * 0.33f))
        {
            scriptAnimMenu.pointsMedalAnimator2.SetInteger("medalAnim", 0);
            //Debug.Log("Score: Bronze");
        }
        else
        {
            scriptAnimMenu.pointsMedalAnimator2.SetInteger("medalAnim", 0);
            //Debug.Log("Score: Trash");
        }

        //checking timer values
        if (timerCurrentScore <= timerTargetScore)
        {
            scriptAnimMenu.timerMedalAnimator2.SetInteger("medalAnim", 2);
            //Debug.Log("Timer: Gold");
        }
        else if (timerCurrentScore <= (timerTargetScore + 30f))
        {
            scriptAnimMenu.timerMedalAnimator2.SetInteger("medalAnim", 1);
            //Debug.Log("Timer: Silver");
        }
        else if (timerCurrentScore <= (timerTargetScore + 60f))
        {
            scriptAnimMenu.timerMedalAnimator2.SetInteger("medalAnim", 0);
            //Debug.Log("Timer: Bronze");
        }
        else
        {
            scriptAnimMenu.timerMedalAnimator2.SetInteger("medalAnim", 0);
            //Debug.Log("Timer: Trash");
        }
        txtHPHighScore.text = HPHighScore.ToString();
        txtPontosHighScore.text = pontosHighScore.ToString();
        uh = TimeSpan.FromSeconds(timerHighScore);
        txtTimerHighScore.text = uh.ToString("mm':'ss':'ff");
    }
}