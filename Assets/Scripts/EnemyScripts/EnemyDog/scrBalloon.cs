using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBalloon : MonoBehaviour
{
    public float timer;
    public float currentState;
    public Animator animBalloon;
    public scrEnemyBehavior scriptEnemyBehavior;

    // Start is called before the first frame update
    void Start()
    {
        animBalloon = gameObject.GetComponent<Animator>();
        scriptEnemyBehavior = gameObject.GetComponentInParent<scrEnemyBehavior>();
        currentState = scriptEnemyBehavior.enemyState;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState != scriptEnemyBehavior.enemyState || scriptEnemyBehavior.isShooting)
        {
            currentState = scriptEnemyBehavior.enemyState;
            animBalloon.SetInteger("balloonState", scriptEnemyBehavior.enemyState);

            if (scriptEnemyBehavior.isShooting)
            {
                animBalloon.SetBool("isShooting", true);
            }
            else
            {
                animBalloon.SetBool("isShooting", false);
            }

            timer = Time.time + 2f;

            if (currentState >= 3 && Time.time < timer)
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (Time.time >= timer && currentState == scriptEnemyBehavior.enemyState)
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -420);
        }
    }
}
