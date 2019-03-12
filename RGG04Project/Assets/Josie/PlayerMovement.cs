using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed = 1f;
    public string horizontalKey = "Horizontal";
    public bool facingRight;

    [Header("Jumping")]
    public float jumpVelocity;
    public string jumpKey = "Jump";
    bool grounded;
    bool canDoubleJump;

    /* [Header("Wall Jumping")]
    public bool wallSliding;
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;
    public float wallPushOff;
    public float wallPushUp; */


    public void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        float horizontal = Input.GetAxis(horizontalKey); 
        if (!Mathf.Approximately(horizontal, 0f))
        {
            Move(horizontal);
        }

        if (Input.GetButtonDown(jumpKey) /* && !wallSliding */)
        {
            Jump();
        }

        /*
        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

            if (facingRight && horizontal > 0f || !facingRight && horizontal < 0f)
            {
                if (wallCheck)
                {
                    WallSliding();
                }

            }
          

        }

        if (wallCheck == false || grounded)
        {
            wallSliding = false;
        }

        if (grounded)
        {
            wallCheck = false;
        } */

    }


    private void Move(float horizontal)
    {
        transform.Translate(horizontal * speed * Time.deltaTime, 0f, 0f);
        Vector2 position = transform.position;
        transform.position = position;

        if (horizontal > 0)
        {
            transform.localScale = new Vector2(1.840318f, 1.813247f);
            facingRight = true;
        }

        else if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1.840318f, 1.813247f);
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
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && !grounded)
        {
            grounded = true;
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

/* WALL JUMPING IF WE WANT
void WallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f);
        wallSliding = true;

        if (Input.GetButtonDown(jumpKey))
        {
            if (facingRight)
            {
               rb2d.AddForce(new Vector2(-wallPushOff, wallPushUp) * jumpVelocity, ForceMode2D.Impulse);
            }

            else
            {
                rb2d.AddForce(new Vector2(wallPushOff, wallPushUp) * jumpVelocity, ForceMode2D.Impulse);
            }
        }
    } */
}
