using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerInteraction : MonoBehaviour
{
    public scrEnemyBehavior scriptEnemyBehavior;
    public scrDroneBehavior scriptDroneBehavior;
    public scrHighScore scriptHighScore;
    public scrpontuacao scriptPontuacao;
    public Player scriptPlayer;
    public Animator animPlayer;
    public Transform enemyParentReference;

    public bool canBackstab = false;
    public bool isHidden = false;

    void Awake()
    {
        animPlayer = gameObject.GetComponent<Animator>();
        scriptPlayer = gameObject.GetComponent<Player>();
        scriptPontuacao = GameObject.Find("Gerenciador").gameObject.GetComponent<scrpontuacao>();
        scriptHighScore = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrHighScore>();
        scriptHighScore.GetSceneReferences();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "EnemyBullet")
        {
            scriptPlayer.playerHealth--;
            Destroy(target.gameObject);
            scriptPontuacao.UpdateUI();
            animPlayer.SetInteger("animPlayer", 4);
            scriptPlayer.isHooked = false;

        }

        if (target.gameObject.tag == "EnemyHitbox")
        {
            scriptPlayer.playerHealth = scriptPlayer.playerHealth - scriptPlayer.playerHealth;
            scriptPontuacao.UpdateUI();
            animPlayer.SetInteger("animPlayer", 4);
        }

        if (target.gameObject.tag == "EnemyBackside")
        {
            scriptEnemyBehavior = target.gameObject.GetComponentInParent<scrEnemyBehavior>();
            if (scriptEnemyBehavior.enemyState != 4) canBackstab = true;
        }

        if (target.gameObject.tag == "pegaveis")
        {
            Destroy(target.gameObject);
            scriptPontuacao.pontos += 200f;
            scriptPontuacao.UpdateUI();
            Debug.Log("Pontos: " + scriptPontuacao.pontos);
        }

        if (target.gameObject.tag == "HealthUp")
        {
            Destroy(target.gameObject);
            scriptPlayer.playerHealth++;
            scriptPontuacao.UpdateUI();
            Debug.Log("Health: " + scriptPlayer.playerHealth);
        }

        if (target.gameObject.tag == "AmmoUp")
        {
            Destroy(target.gameObject);
            scriptPlayer.playerbalas += 6;
            scriptPontuacao.UpdateUI();
            Debug.Log("Ammo: " + scriptPlayer.playerbalas);
        }
    }

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.gameObject.tag == "EnemyVision" && !isHidden)
        {
            scriptEnemyBehavior = target.gameObject.GetComponentInParent<scrEnemyBehavior>();
            scriptEnemyBehavior.enemyState = 4;
            scriptPontuacao.wasUnseen = false;
        }

        if (target.gameObject.tag == "DroneVision" && !isHidden)
        {
            scriptDroneBehavior = target.gameObject.GetComponentInParent<scrDroneBehavior>();
            scriptDroneBehavior.droneState = 4;
            scriptPontuacao.wasUnseen = false;
            Debug.Log("pegou");
        }

        if (target.gameObject.tag == "Cover" && !isHidden)
        {
            isHidden = true;
            //Debug.Log("escondeu");
        }

        if (canBackstab && Input.GetKey(KeyCode.R) && target.gameObject.tag == "EnemyBackside" && target.gameObject.tag != "EnemyVision")
        {
            var morteAtual = Instantiate(scriptEnemyBehavior.enemyDogDeathAnim);
            morteAtual.transform.position = target.transform.position;
            enemyParentReference = target.transform.parent;
            enemyParentReference.gameObject.SetActive(false);
            scriptPontuacao.pontos += 200f;
            scriptPontuacao.UpdateUI();
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.gameObject.tag == "EnemyVision" && scriptEnemyBehavior.enemyState == 4 && target.gameObject.activeInHierarchy == true)
        {
            scriptEnemyBehavior = target.gameObject.GetComponentInParent<scrEnemyBehavior>();
            scriptEnemyBehavior.enemyState = 3;
        }

        if (target.gameObject.tag == "EnemyBackside")
        {
            canBackstab = false;
        }

        if (target.gameObject.tag == "DroneVision" && scriptDroneBehavior.droneState == 4)
        {
            scriptDroneBehavior = target.gameObject.GetComponentInParent<scrDroneBehavior>();
            scriptDroneBehavior.droneState = 1;
        }

        if (target.gameObject.tag == "Cover" && isHidden)
        {
            isHidden = false;
            //Debug.Log("saiu do bagui");
        }
    }

}
