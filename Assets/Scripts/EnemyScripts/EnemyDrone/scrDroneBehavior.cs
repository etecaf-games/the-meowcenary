using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDroneBehavior : MonoBehaviour
{
    public int droneState = 0;
    /*Enemy States:
     droneState = 0 - idle
     droneState = 1 - patrolling
     droneState = 4 - player spotted
    */
    public bool timerStarted = false;

    public float droneSpeed = 50f;
    public float testTimer;
    public float playerSpotted;
    public float playerSpottedCooldown = 5f;
    public float originalY;

    public GameObject[] enemyGroup;
    public scrEnemyBehavior enemyBehavior;
    public int enemyGroupLenght;

    public Animator animDrone;
    public Rigidbody2D droneRigidbody2D;

    public scrPlayerInteraction scriptPlayerInteraction;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        animDrone = GetComponent<Animator>();
        droneRigidbody2D = GetComponent<Rigidbody2D>();
        originalY = this.transform.position.y;
        enemyGroup = GameObject.FindGameObjectsWithTag("EnemyDog");
        enemyGroupLenght = enemyGroup.Length;

        scriptPlayerInteraction = GameObject.FindWithTag("Player").gameObject.GetComponent<scrPlayerInteraction>();
        playerTransform = GameObject.FindWithTag("Player").gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.transform.position.y != originalY)
        //{
        //    this.transform.position = new Vector2(transform.position.x, originalY);
        //}

        testTimer = Time.time;

        if (droneState == 0)
        {
            animDrone.SetInteger("enemyState", 0);
            //Debug.Log("idle");
        }

        if (droneState == 1)
        {
            animDrone.SetInteger("enemyState", 1);
        }

        if (droneState == 4)
        {
            timerStarted = false;
            FlipPlayerBased();
            enemyGroup = GameObject.FindGameObjectsWithTag("EnemyDog");
            enemyGroupLenght = enemyGroup.Length;

            for (int i = 0; i < enemyGroupLenght; i++)
            {
                enemyBehavior = enemyGroup[i].GetComponent<scrEnemyBehavior>();
                enemyBehavior.enemyState = 3;
            }
            /*
            enemyGroup = GameObject.FindGameObjectsWithTag("EnemyDog");
            enemyGroupLenght = enemyGroup.Length;
            enemyBehavior = enemyGroup[enemyGroupLenght].GetComponents<scrEnemyBehavior>();
            enemyBehavior[].enemyState = 3;
            */

            animDrone.SetInteger("enemyState", 4);
            if (droneState != 4) return;
            if (Time.time > playerSpotted && timerStarted)
            {
                timerStarted = false;
            }
            if (timerStarted) return;

            PlayerSpottedTimer();
            //Debug.Log("engaged");

            /*
            if (enemyState == 3)
            {
                //esse código faz o inimigo seguir o player e flipar de acordo com o posicionamento. o inimigo não atira nesse modo
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
                FlipPlayerBased();
                animEnemy.SetInteger("enemyState", 3);

                //esse código inicializa um timer para mudança do estado quando o player consegue evadir da visão do inimigo por determinado tempo
                if (enemyState != 3) return;
                if (Time.time > highAlert && timerStarted)
                {
                    enemyState = 2;
                    timerStarted = false;
                }
                if (timerStarted) return;

                HighAlertTimer();
                enemyVisionCollider[1].size = new Vector2(30, 20);
                //Debug.Log("high alert");
            }
            */

        }
    }

    void FlipPlayerBased()
    {
        var localScale = transform.localScale;

        if (transform.position.x > playerTransform.position.x && localScale.x > 0)
            localScale.x *= -1;
        else if (transform.position.x < playerTransform.position.x && localScale.x < 0)
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    void PlayerSpottedTimer()
    {
        playerSpotted = Time.time + playerSpottedCooldown;
        timerStarted = true;
    }
}
