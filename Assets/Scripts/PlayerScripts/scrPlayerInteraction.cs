using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerInteraction : MonoBehaviour
{
    public scrEnemyBehavior scriptEnemyBehavior;
    public scrDroneBehavior scriptDroneBehavior;
    public scrHighScore scriptHighScore;
    public scrHighScore2 scriptHighScore2;
    public scrpontuacao scriptPontuacao;
    public scrGerenciadorSons scriptGerenciaSons;
    public Player scriptPlayer;
    public Animator animPlayer;
    public Transform enemyParentReference;

    public bool canBackstab = false;
    public bool isHidden = false;
    public float invincibilityTimer;

    public GameObject stealthIndicator;
    public GameObject stealthInstance;

    void Awake()
    {
        animPlayer = gameObject.GetComponent<Animator>();
        scriptPlayer = gameObject.GetComponent<Player>();
        scriptPontuacao = GameObject.Find("Gerenciador").gameObject.GetComponent<scrpontuacao>();
        scriptHighScore = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrHighScore>();
        scriptHighScore.GetSceneReferences();
        scriptHighScore2 = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrHighScore2>();
        scriptHighScore2.GetSceneReferences();
        scriptGerenciaSons = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrGerenciadorSons>();
    }

    void Start()
    {
        isHidden = false;
        scriptGerenciaSons.playTheSoundBGM(10);
        scriptGerenciaSons.bgmSoundEffects[9].Stop();
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
            scriptPlayer.isShooting = false;
            scriptGerenciaSons.playTheSoundPlayer(0);
        }

        if (target.gameObject.tag == "EnemyBackside")
        {
            scriptEnemyBehavior = target.gameObject.GetComponentInParent<scrEnemyBehavior>();
            if (scriptEnemyBehavior.enemyState != 4) canBackstab = true;
            stealthInstance = Instantiate(stealthIndicator, new Vector3(this.transform.position.x, this.transform.position.y +5f, this.transform.position.z), transform.rotation);
        }

        if (target.gameObject.tag == "pegaveis")
        {
            Destroy(target.gameObject);
            scriptPontuacao.pontos += 200f;
            scriptPontuacao.UpdateUI();
            Debug.Log("Pontos: " + scriptPontuacao.pontos);
            scriptGerenciaSons.playTheSoundMisc(7);
        }

        if (target.gameObject.tag == "HealthUp")
        {
            Destroy(target.gameObject);
            scriptPlayer.playerHealth++;
            scriptPontuacao.UpdateUI();
            Debug.Log("Health: " + scriptPlayer.playerHealth);
            scriptGerenciaSons.playTheSoundMisc(6);
        }

        if (target.gameObject.tag == "AmmoUp")
        {
            Destroy(target.gameObject);
            scriptPlayer.playerbalas += 6;
            scriptPontuacao.UpdateUI();
            Debug.Log("Ammo: " + scriptPlayer.playerbalas);
            scriptGerenciaSons.playTheSoundMisc(8);
        }
    }

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.gameObject.tag == "EnemyVision" && !isHidden)
        {
            scriptEnemyBehavior = target.gameObject.GetComponentInParent<scrEnemyBehavior>();
            scriptEnemyBehavior.enemyState = 4;
            scriptPontuacao.wasUnseen = false;
            scriptGerenciaSons.playTheSoundEnemy(3);
        }

        if (target.gameObject.tag == "DroneVision" && !isHidden)
        {
            scriptDroneBehavior = target.gameObject.GetComponentInParent<scrDroneBehavior>();
            scriptDroneBehavior.droneState = 4;
            scriptPontuacao.wasUnseen = false;
            Debug.Log("pegou");
        }

        if (target.gameObject.tag == "EnemyHitbox")
        {
            if (Time.time > invincibilityTimer)
            {
                scriptPlayer.playerHealth--;
                invincibilityTimer = Time.time + 2f;
                animPlayer.SetInteger("animPlayer", 4);
                scriptPlayer.isHooked = false;
                scriptPontuacao.UpdateUI();
                scriptGerenciaSons.playTheSoundPlayer(0);
            }
        }

        if (target.gameObject.tag == "Cover" && !isHidden)
        {
            isHidden = true;
            //Debug.Log("escondeu");
        }

        if (canBackstab && Input.GetKey(KeyCode.E) && target.gameObject.tag == "EnemyBackside" && target.gameObject.tag != "EnemyVision")
        {
            animPlayer.SetInteger("animPlayer", 5);
            var morteAtual = Instantiate(scriptEnemyBehavior.enemyDogDeathAnim);
            morteAtual.transform.position = target.transform.position;
            enemyParentReference = target.transform.parent;
            enemyParentReference.gameObject.SetActive(false);
            scriptPontuacao.pontos += 200f;
            scriptPontuacao.UpdateUI();
            scriptGerenciaSons.playTheSoundPlayer(2);
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
            Destroy(stealthInstance);
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
