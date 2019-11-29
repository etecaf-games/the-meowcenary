using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPatrol : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float patrolTime = 0f;
    public float patrolWaitTime = 3f;
    public float nextPatrolSpot;
    public int patrolSpotIndex = 0;

    public bool isMoving = true;
    public bool loopPatrolSpots = true;

    public GameObject[] patrolSpots;
    public Rigidbody2D enemyRigidbody2D;
    public Transform enemyTransform;
    public Animator animEnemy;
    public Collider2D playerCollider2d;

    public scrEnemyBehavior scriptEnemyBehavior;
    public scrDroneBehavior scriptDroneBehavior;

    void Awake()
    {
        enemyTransform = gameObject.GetComponent<Transform>();
        enemyRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animEnemy = gameObject.GetComponent<Animator>();
        playerCollider2d = GameObject.Find("Player").gameObject.GetComponent<Collider2D>();

        //esse codigo checa a tag do inimigo antes de puxar o componente para somente puxar o componente certo, as variaveis com
        //os outros continuarao vazias.
        if(this.gameObject.tag == "EnemyDog")
            scriptEnemyBehavior = gameObject.GetComponent<scrEnemyBehavior>();
        if(this.gameObject.tag == "EnemyDrone")
            scriptDroneBehavior = gameObject.GetComponent<scrDroneBehavior>();

        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= patrolTime)
        {
            Move();
        }
    }

    void Move()
    {
        if (patrolSpots.Length != 0 && isMoving)
        {
            if (this.gameObject.tag == "EnemyDog")
                if (scriptEnemyBehavior.enemyState >= 3)
                    return;

            nextPatrolSpot = patrolSpots[patrolSpotIndex].transform.position.x - enemyTransform.position.x;

            //esse código checa a tag do inimigo antes de checar se o estado do inimigo é menor ou igual a "alerta" para que esse
            //código seja útil para qualquer tipo de inimigo que patrulha.
            if (this.gameObject.tag == "EnemyDog")
            {
                if (scriptEnemyBehavior.enemyState <= 2)
                {
                    Flip(nextPatrolSpot);
                }
            }
            if (this.gameObject.tag == "EnemyDrone")
            {
                if (scriptDroneBehavior.droneState <= 2)
                {
                    Flip(nextPatrolSpot);
                }
            }
            //-------------------------------------------------------------------------------------------------------------------

            if (Mathf.Abs(nextPatrolSpot) <= 0.1f)
            {
                enemyRigidbody2D.velocity = Vector2.zero;
                patrolSpotIndex++;
                
                if (patrolSpotIndex >= patrolSpots.Length)
                {
                    if (loopPatrolSpots)
                    {
                        patrolSpotIndex = 0;
                    }
                    else
                    {
                        isMoving = false;
                    }
                }
                patrolTime = Time.time + patrolWaitTime;
                if (this.gameObject.tag == "EnemyDog" && scriptEnemyBehavior.enemyState <= 2)
                    scriptEnemyBehavior.enemyState = 0;
                if (this.gameObject.tag == "EnemyDrone" && scriptDroneBehavior.droneState <= 2)
                    scriptDroneBehavior.droneState = 0;

            } else {
                enemyRigidbody2D.velocity = new Vector2(enemyTransform.localScale.x * moveSpeed, enemyRigidbody2D.velocity.y);
                animEnemy.SetInteger("enemyState", 1);
                if (this.gameObject.tag == "EnemyDog" && scriptEnemyBehavior.enemyState <= 2)
                    scriptEnemyBehavior.enemyState = 1;
                if (this.gameObject.tag == "EnemyDrone" && scriptDroneBehavior.droneState <= 2)
                    scriptDroneBehavior.droneState = 1;
            }
        }
    }

    void Flip(float nextPatrolSpot)
    {
        var localScale = enemyTransform.localScale;

        if (nextPatrolSpot > 0 && localScale.x < 0)
            localScale.x *= -1;
        else if (nextPatrolSpot < 0 && localScale.x > 0)
            localScale.x *= -1;

        enemyTransform.localScale = localScale;
    }
}