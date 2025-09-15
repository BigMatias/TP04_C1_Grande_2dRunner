using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerData PlayerData;
    [SerializeField] private UIGame UIGame;
    [SerializeField] private AudioClip dead;

    [NonSerialized] public bool potionPickedUp = false;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private bool grounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        rb.gravityScale = 5f;
    }
    private void Update()
    {
        Fall();
    }

    private void FixedUpdate()
    {
        if (UIGame.gameStarted == true)
        {
            animator.SetBool("GameStarted", true);
            Jump();
        }
    }

    private void Fall()
    {
        if (Input.GetKeyUp(PlayerData.Jump))
        {
            rb.gravityScale = 8f;
        }
        if (rb.velocity.y <= 0f)
        {
            animator.SetBool("Jump", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(PlayerData.Jump) && grounded == true && UIGame.gameStarted == true)
        {
            rb.gravityScale = 5f;
            animator.SetBool("Jump", true);
            rb.AddForce(PlayerData.JumpSpeed * Time.fixedDeltaTime * Vector2.up);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            grounded = true;
            animator.SetBool("Grounded", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && !potionPickedUp)
        {
            animator.SetBool("TouchedEnemy", true);
            rb.gravityScale = 0f;
            UIGame.gameStarted = false;
            audioSource.PlayOneShot(dead);
            UIGame.GameOver();
        }

        if (collision.gameObject.layer == 8 && potionPickedUp)
        {
            audioSource.Play();
            collision.gameObject.SetActive(false);
            potionPickedUp = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            StartCoroutine(PotionPickedUp());
        }
    }

    private IEnumerator PotionPickedUp()
    {
        potionPickedUp = true;
        yield return new WaitForSeconds(10);
        potionPickedUp = false;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            grounded = false;
            animator.SetBool("Grounded", false);
        }
    }


}
