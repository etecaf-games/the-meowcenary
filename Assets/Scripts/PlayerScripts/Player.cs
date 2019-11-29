using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f, jump = 30f;
    Rigidbody2D rb;
    float direction = 0f;

    public float playerVelXatual;
    public float playerVelYatual;
    public float playerHealth = 7f;
    public float playerbalas = 6f;

    public Animator animPlayer;

    //essas variaveis precisam ser setadas no editor para funcionarem no codigo:
    public Transform localPes;
    public LayerMask layerChao;
    //-------------------------------------------------------------------------

    public bool isGrounded;
    public bool isShooting;
    public bool isHooked = false;
    public bool lookingRight = true;

    public Gun playerGun;
    public scrpontuacao scriptPontuacao;
    public GameObject playerDeathAnim;

    void Start()
    {
        this.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
        playerGun = GetComponentInChildren<Gun>();
        scriptPontuacao = GameObject.Find("Gerenciador").gameObject.GetComponent<scrpontuacao>();
    }

    void Update()
    {
        playerVelXatual = rb.velocity.x;
        playerVelYatual = rb.velocity.y;

        direction = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(localPes.position, 0.5f, layerChao);

        if (playerHealth <= 0f)
        {
            var morteAtual = Instantiate(playerDeathAnim);
            morteAtual.transform.position = this.transform.position;
            this.gameObject.SetActive(false);
        }

        if (playerbalas > 6f)
        {
            playerbalas = 6f;
            scriptPontuacao.UpdateUI();
        }
        if (playerHealth > 7f)
        {
            playerHealth = 7f;
            scriptPontuacao.UpdateUI();
        }
    }

    void FixedUpdate()
    {
        animPlayer.SetFloat("velY", playerVelYatual);
        if (playerVelYatual != 0)
        {
            isGrounded = false;
            animPlayer.SetBool("isGrounded", false);
        }
        if (isGrounded)
        {
            animPlayer.SetBool("isGrounded", true);
        }

        if(direction != 0f && !isShooting)
        {
            rb.velocity = new Vector2(speed * direction,rb.velocity.y);
            animPlayer.SetInteger("animPlayer", 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animPlayer.SetInteger("animPlayer", 0);
        }

        if(Input.GetButtonDown ("Jump") && isGrounded && !isShooting)
        {
            rb.velocity = new Vector2(rb.velocity.x,jump);
        }

        if (direction < 0 && lookingRight)
        {
            Virar();
        }

        if (direction > 0 && !lookingRight)
        {
            Virar();
        }
    }

    public void OnTriggerEnter2D(Collider2D quem)
    {
        if (quem.gameObject.tag == "vida")
        {
            Destroy(GameObject.FindWithTag("vida"));
            playerHealth += 1;
        }

        if (quem.gameObject.tag == "municao")
        {
            Destroy(GameObject.FindWithTag("municao"));
            playerbalas += 1;
        }

    }

    public void Virar()
    {
        lookingRight = !lookingRight;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, 1, 1);
    }

    public void AcabouDeAtirar()
    {
        isShooting = false;
        animPlayer.SetBool("isShooting", false);
        animPlayer.SetInteger("animPlayer", 0);
    }

    public void AtirandoBala()
    {
        if (playerbalas >= 1)
        {
            playerGun.Shoot1();
            playerbalas -= 1f;
            scriptPontuacao.UpdateUI();
        }
    }

    public void AtirandoHook()
    {
        playerGun.Shoot2();
    }
}
