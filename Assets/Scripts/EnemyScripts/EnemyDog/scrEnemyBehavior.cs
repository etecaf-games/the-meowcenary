using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyBehavior : MonoBehaviour
{
    public int enemyState = 0;
    /*
     Enemy States:
     enemyState = 0 - idle
     enemyState = 1 - patrolling
     enemyState = 2 - alert
     enemyState = 3 - high alert
     enemyState = 4 - engaged
    */
    public bool isGrounded;
    public bool isShooting = false;
    public bool timerStarted = false;
    public bool shootingTimerStarted = false;
    public bool isLimitedY = true;

    public float enemySpeed = 10f;
    public float enemyHealth = 2f;
    public float bulletSpeed = 50f;
    public float shootingCooldown = 2f;
    public float highAlertCooldown = 5f;
    public float shooting;
    public float highAlert;
    public float testTimer;
    public float originalX;
    public float originalY;

    public Transform[] localPesInimigo;
    public LayerMask layerChaoInimigo;
    
    public Animator animEnemy;
    public Rigidbody2D enemyRigidbody2D;
    public Collider2D enemyCollider;

    public Transform[] shootingPoint;
    public BoxCollider2D[] enemyVisionCollider;
    public float visionSizeX;
    public float visionSizeY;

    public scrPlayerInteraction scriptPlayerInteraction;
    public Transform playerTransform;
    public scrpontuacao scriptPontuacao;
    public scrGerenciadorSons scriptGerenciaSons;

    public GameObject bulletPrefab;
    public GameObject enemyDogDeathAnim;

    // Start is called before the first frame update
    void Start()
    {
        originalY = this.transform.position.y;
        this.gameObject.SetActive(true);

        if (this.gameObject.tag == "EnemyDog")
            enemyHealth = 2f;

        if (this.gameObject.tag == "EnemyDrone")
            enemyHealth = 2f;

        animEnemy = GetComponent<Animator>();
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();

        shootingPoint = GetComponentsInChildren <Transform>();
        localPesInimigo = GetComponentsInChildren<Transform>();
        enemyVisionCollider = GetComponentsInChildren<BoxCollider2D>();

        scriptPlayerInteraction = GameObject.FindWithTag("Player").gameObject.GetComponent<scrPlayerInteraction>();
        playerTransform = GameObject.FindWithTag("Player").gameObject.GetComponent<Transform>();
        scriptPontuacao = GameObject.FindWithTag("Gerenciador").gameObject.GetComponent<scrpontuacao>();
        scriptGerenciaSons = GameObject.Find("GerenciadorGlobal(Clone)").gameObject.GetComponent<scrGerenciadorSons>();
    }

    // Update is called once per frame
    void Update()
    {
        //trava o Y positivo do inimigo para não interferir no MoveTowards
        //if (this.transform.position.y > originalY && isLimitedY)
        //{
        //    this.transform.position = new Vector2(transform.position.x, originalY);
        //}
        testTimer = Time.time;

        isGrounded = Physics2D.OverlapCircle(localPesInimigo[3].position, 0.05f, layerChaoInimigo);

        //ifs checando o estado do inimigo, que é mudado pelo script da Interação do Player assim como por esse script
        if (enemyState == 0)
        {
            animEnemy.SetInteger("enemyState", 0);
            animEnemy.SetBool("isWalking", false);
            enemyVisionCollider[1].size = new Vector2(20, 10);
        }
        if (enemyState == 1)
        {
            animEnemy.SetInteger("enemyState", 1);
            animEnemy.SetBool("isWalking", true);
            enemyVisionCollider[1].size = new Vector2(20, 10);
            //Debug.Log("patrulhando");
        }
        if (enemyState == 2)
        {
            animEnemy.SetInteger("enemyState", 2);
            animEnemy.SetBool("isWalking", true);

            //esse codigo faz o inimigo voltar para patrulha normal depois de um determinado tempo
            /*if (enemyState != 2) return;
            if (Time.time > highAlert && timerStarted)
            {
                enemyState = 1;
                timerStarted = false;
            }
            if (timerStarted) return;

            HighAlertTimer();*/
            enemyVisionCollider[1].size = new Vector2(30, 20);
            
            //Debug.Log("alert");
        }
        if (enemyState == 3)
        {
            //esse código faz o inimigo seguir o player e flipar de acordo com o posicionamento. o inimigo não atira nesse modo
            isShooting = false;
            animEnemy.SetBool("isShooting", false);
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
        if (enemyState == 4)
        {
            //esse código faz o inimigo seguir o player e flipar de acordo com o posicionamento, e também reseta o timer pois o player
            //está sendo visto aqui
            timerStarted = false;
            FlipPlayerBased();

            //esse código trava a movimentação do inimigo caso ele esteja atirando
            if (isShooting == false)
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
            else 
                transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            animEnemy.SetInteger("enemyState", 4);

            //esse código inicializa um timer para definir um delay entre os tiros do inimigo
            if (enemyState != 4) return;
            if (Time.time > shooting && shootingTimerStarted)
            {
                shootingTimerStarted = false;
                isShooting = true;
                animEnemy.SetBool("isShooting", true);
            }
            if (shootingTimerStarted) return;

            ShootingTimer();
            enemyVisionCollider[1].size = new Vector2(30, 20);
            //Debug.Log("engaged");
        }

        //ifs checando se o Y do inimigo decaiu demais para que ele consiga voltar para a patrulha, resetando-o para a posição inicial
        //caso isso seja positivo.
        //(seria interessante existir uma animação que trigga isso)
        /*if (enemyState <= 2)
        {
            if (this.transform.position.y < originalY - 3f)
            {
                this.transform.position = new Vector2(originalX, originalY);
            }
        }*/
    }

    void FixedUpdate()
    {
        if (enemyHealth <= 0)
        {
            var morteAtual = Instantiate(enemyDogDeathAnim);
            morteAtual.transform.position = this.transform.position;
            scriptPontuacao.pontos += 100;
            Debug.Log("Pontos: " + scriptPontuacao.pontos);
            this.gameObject.SetActive(false);
            scriptGerenciaSons.bgmSoundEffects[3].Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Bullet")
        {
            enemyHealth--;
            Destroy(target.gameObject);
            enemyState = 3;
        }
    }

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.gameObject.tag == "LimiterY" && isGrounded)
        {
            isLimitedY = false;
            Jump();
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.gameObject.tag == "LimiterY")
        {
            isLimitedY = true;
        }
    }

    //flip do inimigo baseado na posição do player
    //o flip normal do inimigo está no script "scrPatrol"
    void FlipPlayerBased()
    {
        var localScale = transform.localScale;

        if (transform.position.x > playerTransform.position.x && localScale.x > 0)
            localScale.x *= -1;
        else if (transform.position.x < playerTransform.position.x && localScale.x < 0)
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    //timer da mudança de estado 3 para 2
    void HighAlertTimer()
    {
        highAlert = Time.time + highAlertCooldown;
        timerStarted = true;
    }

    //timer do tiro
    void ShootingTimer()
    {
        shooting = Time.time + shootingCooldown;
        shootingTimerStarted = true;
    }

    void instanciaBullet()
    {
        GameObject shot = Instantiate(bulletPrefab, shootingPoint[2].position, transform.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * bulletSpeed, 0f);
    }

    void isShootingFalseAnim()
    {
        isShooting = false;
        animEnemy.SetBool("isShooting", false);
    }
    void Jump()
    {
        //enemyRigidbody2D.AddForce(new Vector2(0, 30f), ForceMode2D.Impulse);
        enemyRigidbody2D.velocity = new Vector2(enemyRigidbody2D.velocity.x, 30);
    }
}
