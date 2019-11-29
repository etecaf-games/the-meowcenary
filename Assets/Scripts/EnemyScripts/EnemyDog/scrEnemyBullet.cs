using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyBullet : MonoBehaviour
{
    public float timer;

    void Start()
    {
        timer = Time.time + 2f;
    }

    void FixedUpdate()
    {
        if (Time.time >= timer) Destroy(gameObject);
    }
}
