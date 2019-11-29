using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 0f;
    public GameObject bullet;
    public GameObject hook;
    Vector2 direction;

    public bool hasShotAHook = false;

    public Player scriptPlayer;
    public Hook scriptHook;

    void Start()
    {
        scriptPlayer = gameObject.GetComponentInParent<Player>();
    }

    void Update()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Input.GetButton("Fire1") && scriptPlayer.isGrounded)
        {
            scriptPlayer.isShooting = true;
            scriptPlayer.animPlayer.SetBool("isShooting", true);
            scriptPlayer.animPlayer.SetTrigger("shootingTrigger");
            scriptPlayer.animPlayer.SetInteger("animPlayer", 2);
        }
        if (Input.GetButton("Fire2") && scriptPlayer.isGrounded && !scriptPlayer.isHooked)
        {
            scriptPlayer.isShooting = true;
            scriptPlayer.animPlayer.SetBool("isShooting", true);
            scriptPlayer.animPlayer.SetTrigger("shootingTrigger");
            scriptPlayer.animPlayer.SetInteger("animPlayer", 3);
        }

        //if (Input.GetButtonUp("Fire1"))
        //{
        //    scriptPlayer.isShooting = false;
        //    scriptPlayer.animPlayer.SetBool("isShooting", false);
        //}
        //if (Input.GetButtonUp("Fire2"))
        //{
        //    scriptPlayer.isShooting = false;
        //    scriptPlayer.animPlayer.SetBool("isShooting", false);
        //}
    }

    public void Shoot1()
    {
        GameObject shot = Instantiate(bullet,transform.position,transform.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    public void Shoot2()
    {
        GameObject shot = Instantiate(hook,transform.position,transform.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = direction * speed;
        hasShotAHook = true;
    }
}
