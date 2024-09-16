using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    float dir;

    [Header("Jump")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 size;
    [SerializeField] float jumpFocre;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float jumpDamping;
    [SerializeField] GameObject jumpEffect;
    bool canJump;

    [Header("Gravity")]
    [SerializeField] float jumpGravity;
    [SerializeField] float fallGravity;

    [Header("Status")]
    public int hp = 3;
    [SerializeField] GameObject shield;
    [SerializeField] Animator shieldAnim;
    [SerializeField] LayerMask trapLayer;
    [SerializeField] int playerID;

    [Header("Animation")]
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sr;
    public Sprite idleSprite;
    [SerializeField] Sprite walkingSprite;

    [Header("Sound Effect")]
    [SerializeField] GameObject deathSound;
    AudioSource jumpSound;

    [Header("Other")]
    [SerializeField] SpawnPoint spawnPoint;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] ParticleSystem walkingEffect;
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject playerSoul;
    MapController mapController;

    bool takeDmg = true;
    bool checkGround = true;
    bool isGround;
    bool isPushed;

    Rigidbody2D rb;

    GameManager GM;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GM = GameManager.instance;
        mapController = MapController.instance;
        jumpSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        takeDmg = false;
        checkGround = true;
        isPushed = false;
        StartCoroutine(ShieldCO());
    }

    void Update()
    {
        IsGround();
        Movement();
        GravityController();
        SpriteControll();
    }

    void Movement()
    {
        if(isPushed == false)
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
    }

    void SpriteControll()
    {
        if(dir != 0 && isGround)
        {
            sr.sprite = walkingSprite;
        }
        else
        {
            sr.sprite = idleSprite;
        }
    }

    void GravityController()
    {
        if (rb.velocity.y < 0)
            rb.gravityScale = fallGravity;
        else if (rb.velocity.y > 0)
            rb.gravityScale = jumpGravity;
    }

    void IsGround()
    {
        if (checkGround)
        {
            bool isOtherPlayer = false;
            Collider2D[] players = Physics2D.OverlapBoxAll(groundCheck.position, size, 0, playerLayer);
            foreach (Collider2D player in players)
            {
                if(player.gameObject != gameObject)
                {
                    isOtherPlayer = true;
                }
            }
            isGround = Physics2D.OverlapBox(groundCheck.position, size, 0, groundLayer) || isOtherPlayer;

        }
        else
            isGround = false;

        if (isGround)
        {
            canJump = true;
        }
        anim.SetBool("Landed", isGround);
    }

    public void TakeDamage()
    {
        if(takeDmg == true)
        {
            takeDmg = false;
            hp--;
            playerHealth.SetHeart(hp, playerID);

            if (hp <= 0)
            {
                gameObject.SetActive(false);
                mapController.DeathSignal();
            }
            else
            {
                gameObject.SetActive(false);
                spawnPoint.Respawn(gameObject, playerSoul);
                takeDmg = false;
            }

            GameObject effect = Instantiate(deathEffect, transform.position + Vector3.up / 2, Quaternion.identity);
            Destroy(effect, 1f);
            GameObject sound = Instantiate(deathSound);
            Destroy(sound, 1f);
        }
    }

    IEnumerator ShieldCO()
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(1.85f);
        shieldAnim.SetTrigger("Shield Broke");
        yield return new WaitForSeconds(0.15f);
        takeDmg = true;
        shield.SetActive(false);

        bool inTheTrap = Physics2D.OverlapBox(transform.position + Vector3.up / 2, new Vector2(0.9f, 0.9f), 0, trapLayer);
        if (inTheTrap)
        {
            TakeDamage();
        }
    }

    IEnumerator JumpCO()
    {
        checkGround = false;
        yield return new WaitForSeconds(0.1f);
        checkGround = true;
    }

    public void Push(Vector2 dir)
    {
        isPushed = true;
        rb.velocity = Vector2.zero;
        rb.velocity = dir * 15f;
        StartCoroutine(PushCO());
    }

    IEnumerator PushCO()
    {
        yield return new WaitForSeconds(0.3f);
        isPushed = false;
    }

    public void Move(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<float>();

        if (dir < 0)
            transform.rotation = Quaternion.Euler(0, -180, 0);
        else if (dir > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if(dir != 0)
        {
            anim.SetBool("Walking", true);
            walkingEffect.Play();
        }
        else
        {
            anim.SetBool("Walking", false);
            walkingEffect.Stop();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump && context.performed)
        {
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * jumpFocre;
            jumpSound.Play();

            GameObject effect = Instantiate(jumpEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.5f);

            canJump = false;
            StartCoroutine(JumpCO());

            isPushed = false;
            StopCoroutine(PushCO());
        }
        if (!context.performed && rb.velocity.y > jumpDamping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpDamping);
        }
    }
    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GM.PauseGame();
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            if (isGround == true)
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;

            Gizmos.DrawWireCube(groundCheck.position, size);
        }
    }
}
