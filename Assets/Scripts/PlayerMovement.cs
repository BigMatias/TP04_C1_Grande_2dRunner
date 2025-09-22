using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerData PlayerData;
    [SerializeField] private UIGame UIGame;
    [SerializeField] private AudioClip dead;
    [SerializeField] private AudioClip coinPickedUp;
    [SerializeField] private AudioClip goldPowerUp;

    [NonSerialized] public bool potionPickedUp = false;
    [NonSerialized] public bool goldPowerUpPickedUp = false;

    private static readonly int State = Animator.StringToHash("State");
    private bool grounded = false;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    enum PlayerState
    {
        Idle = 0,
        Run = 1,
        Die = 2,
        Fall = 3,
        Jump = 4
    }

    [SerializeField] private PlayerState playerState = PlayerState.Idle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        animator.SetInteger(State, (int)playerState);
        rb.gravityScale = 5f;
    }
    private void Update()
    {
        if (UIGame.gameStarted)
        {
            Fall();
        }
    }

    private void FixedUpdate()
    {
        if (UIGame.gameStarted)
        {
            if (grounded)
            {
                playerState = PlayerState.Run;
                animator.SetInteger(State, (int)playerState);
            }
            Jump();
        }
    }

    private void Fall()
    {
        if (Input.GetKeyUp(PlayerData.Jump))
        {
            rb.gravityScale = 8f;
        }
        if (rb.velocity.y <= 0f && !grounded)
        {
            playerState = PlayerState.Fall;
            animator.SetInteger(State, (int)playerState);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(PlayerData.Jump) && grounded && UIGame.gameStarted)
        {
            rb.gravityScale = 5f;
            playerState = PlayerState.Jump;
            animator.SetInteger(State, (int)playerState);
            rb.AddForce(PlayerData.JumpSpeed * Time.fixedDeltaTime * Vector2.up);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            grounded = true;
            animator.SetInteger(State, (int)playerState);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && !potionPickedUp)
        {
            playerState = PlayerState.Die;
            animator.SetInteger(State, (int)playerState);
            rb.gravityScale = 0f;
            UIGame.gameStarted = false;
            audioSource.PlayOneShot(dead);
            UIGame.GameOver();
        }

        if (collision.gameObject.layer == 8 && potionPickedUp)
        {
            collision.gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            audioSource.Play();
            StartCoroutine(PotionPickedUp());
        }
        if (collision.gameObject.layer == 11)
        {
            UIGame.CoinPickedUp();
            audioSource.PlayOneShot(coinPickedUp);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.layer == 12)
        {
            audioSource.PlayOneShot(goldPowerUp);
            collision.gameObject.SetActive(false);
            StartCoroutine(GoldPowerUpPickedUp());
        }
    }

    private IEnumerator PotionPickedUp()
    {
        potionPickedUp = true;
        yield return new WaitForSeconds(10);
        potionPickedUp = false;

    }
    private IEnumerator GoldPowerUpPickedUp()
    {
        goldPowerUpPickedUp = true;
        yield return new WaitForSeconds(10);
        goldPowerUpPickedUp = false;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            grounded = false;
        }
    }
}
