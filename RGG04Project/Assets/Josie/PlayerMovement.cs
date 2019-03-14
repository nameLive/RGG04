﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;

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
    


    public void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
        // playerSprite = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        anim.SetBool("IsGrounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis(horizontalKey)));
        anim.SetBool("CanDoubleJump", canDoubleJump);
        anim.SetBool("IsFalling", isFalling);
        
        


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


    private void Move(float horizontal)
    {
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
