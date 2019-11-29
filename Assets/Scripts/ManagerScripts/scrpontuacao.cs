using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrpontuacao : MonoBehaviour
{
    public bool pegavel = false;
    public bool wasUnseen = true;
    public float pontos = 0;
    public float timer = 0;
    public GameObject[] enemyQuantity;
    public GameObject[] coletavelQuantity;

    public float missionTargetPontos;
    public float missionTargetTimer;

    public Text txtHP;
    public Text txtAmmo;
    public Text txtPoints;
    public Text txtTimer;
    public TimeSpan uh;

    public scrPlayerInteraction scriptPlayerInteraction;
    public Player scriptPlayer;

    //public static TimeSpan FromSeconds(float timer);

    void Start()
    {
        scriptPlayerInteraction = GameObject.Find("Player").gameObject.GetComponent<scrPlayerInteraction>();
        scriptPlayer = GameObject.Find("Player").gameObject.GetComponent<Player>();
        txtHP = GameObject.Find("HPText").gameObject.GetComponent<Text>();
        txtAmmo = GameObject.Find("AmmoText").gameObject.GetComponent<Text>();
        txtPoints = GameObject.Find("PointsText").gameObject.GetComponent<Text>();
        txtTimer = GameObject.Find("TimerText").gameObject.GetComponent<Text>();
        pontos = 0;
        timer = 0;
        wasUnseen = true;
        UpdateUI();
    }

    public void FixedUpdate()
    {
        uh = TimeSpan.FromSeconds(timer);
        //Debug.Log(uh);
        timer += Time.deltaTime;
        txtTimer.text = uh.ToString("mm':'ss':'ff");
    }

    public void UpdateUI()
    {
        txtHP.text = scriptPlayer.playerHealth.ToString();
        txtAmmo.text = scriptPlayer.playerbalas.ToString();
        txtPoints.text = pontos.ToString();
    }
}
