using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bounce = 3;
    public float timer;

    void Start()
    {
        timer = Time.time + 2f;
    }

    void FixedUpdate()
    {
        if (bounce <= 0 || Time.time >= timer)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            bounce--;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject);
        }
    }
}
