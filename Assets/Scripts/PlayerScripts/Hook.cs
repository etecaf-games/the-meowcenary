using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    Rigidbody2D rb;
    public bool hooked = false;
    Vector2 direction;
    GameObject player;
    public Transform hookspotTransform;
    public scrPatrol dronePatrol;
    public Gun scriptGun;
    public float compensationDirection = 0.5f;

    void Awake()
    {
        scriptGun = GameObject.Find("Gun").gameObject.GetComponent<Gun>();
        if (GameObject.Find("Hook(Clone)").activeInHierarchy == true && scriptGun.hasShotAHook)
        {
            Destroy(this.gameObject);
        }
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire2") && hooked)
        {
            Destroy(gameObject);
            hooked = false;
            player.GetComponent<Player>().isHooked = false;
            player.GetComponent<Player>().isShooting = false;
            scriptGun.hasShotAHook = false;
        }
        direction = transform.position - player.transform.position;

        /*if (hookspotTransform != null)
        {
            if (dronePatrol.patrolSpotIndex == 0)
            {
                compensationDirection = 0.5f;
            }
            else
            {
                compensationDirection = -0.5f;
            }
        direction = new Vector3(transform.position.x, transform.position.y) - player.transform.position;
        }*/
    }

    void FixedUpdate()
    {
        if (hooked)
        {
            player.GetComponent<Rigidbody2D>().velocity = direction * player.GetComponent<Player>().speed;
            if (hookspotTransform == null) return;
            else
            {
                this.transform.position = new Vector3(hookspotTransform.position.x, hookspotTransform.position.y - 1f, 0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            hooked = true;
            player.GetComponent<Player>().isHooked = true;
            if (collision.gameObject.name == "DroneHookspot")
            {
                dronePatrol = collision.gameObject.GetComponent<scrPatrol>();
                hookspotTransform = collision.gameObject.GetComponent<Transform>();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject);
            scriptGun.hasShotAHook = false;
        }
    }
}
