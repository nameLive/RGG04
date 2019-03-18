using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;

    bool isTakingDamage = false;
    [Tooltip("When the player takes damage this is the duration of not being able to move")]
    public float damageStunDuration = 0.5f;

    public float speed = 1f;
    public string horizontalKey = "Horizontal";
    public bool facingRight;

    [Header("Jumping")]
    public float jumpVelocity;
    public string jumpKey = "Jump";
    bool grounded;
    bool canDoubleJump;
    bool isFalling;

    [SerializeField]
    GameObject playerSprite;
    PlayerHammerState hammerState;
    PlayerHealth playerHealth;


    public void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
        // playerSprite = GetComponent<SpriteRenderer>();
        hammerState = GetComponentInChildren<PlayerHammerState>();
        playerHealth = GetComponentInChildren<PlayerHealth>();

        playerHealth.EventOnHealthDecreased += TookDamage;
    }


    private void Update()
    {
        anim.SetBool("IsGrounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis(horizontalKey)));
        anim.SetBool("CanDoubleJump", canDoubleJump);
        anim.SetBool("IsFalling", isFalling);
        anim.SetBool("HasHammer", hammerState.hasHammer);
        

        float horizontal = Input.GetAxis(horizontalKey);
        if (!Mathf.Approximately(horizontal, 0f))
        {
            Move(horizontal);
        }

        if (Input.GetButtonDown(jumpKey))
        {
            Jump();
        }

        if (rb2d.velocity.y > 0)
        {
            isFalling = false;
        }
        else if (rb2d.velocity.y < 0)
        {
            isFalling = true;
        }
        else if (rb2d.velocity.y == 0)
        {
            isFalling = false;
        }

      
    }

  

    void TookDamage()
    {
        isTakingDamage = true;
        anim.SetTrigger("GotDamaged");
       // delay here (no idea how)
        anim.ResetTrigger("GotDamaged");

        Invoke("StoppedTakingDamage", 0.5f);
    }

    void StoppedTakingDamage()
    {
        isTakingDamage = false;
    }

    private void Move(float horizontal)
    {
        if (isTakingDamage) return;

        transform.Translate(horizontal * speed * Time.deltaTime, 0f, 0f);
        // Vector2 position = transform.position;
        // transform.position = position;

        if (horizontal > 0 && !facingRight)
        {
            playerSprite.transform.localScale = new Vector2(playerSprite.transform.localScale.x * -1, transform.localScale.y);
            facingRight = true;
        }

        else if (horizontal < 0 && facingRight)
        {
            playerSprite.transform.localScale = new Vector2(playerSprite.transform.localScale.x * -1, transform.localScale.y);
            facingRight = false;
        }
    }


    #region JUMPING
    private void Jump()
    {
        if (isTakingDamage) return;

        if (grounded)
        {
            rb2d.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            canDoubleJump = true;
        }

        else
        {
            if (canDoubleJump)
            {
                
                canDoubleJump = false;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                anim.SetTrigger("HasDoubleJumped");

            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && !grounded)
        {
            grounded = true;
            anim.ResetTrigger("HasDoubleJumped");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && grounded)
        {
            grounded = false;
        }
    }

    #endregion JUMPING

}
