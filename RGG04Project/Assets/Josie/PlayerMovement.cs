using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public string horizontalKey = "Horizontal";

    public float jumpVelocity;
    public string jumpKey = "Jump";
    bool jumpRequest;
    bool grounded;


    private void Update()
    {
        float horizontal = Input.GetAxis(horizontalKey);
        if (!Mathf.Approximately(horizontal, 0f))
        {
            Move(horizontal);
        }

        if (Input.GetButtonDown(jumpKey) && grounded)
        {
            jumpRequest = true;
        }

    }

    void FixedUpdate()
    {
        if (jumpRequest)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);

            jumpRequest = false;
        }
    }


    private void Move(float horizontal)
    {
        transform.Translate(horizontal * speed * Time.deltaTime, 0f, 0f);
        Vector2 position = transform.position;
        transform.position = position;

        if (horizontal > 0)
        {
            transform.localScale = new Vector2(1.840318f, 1.813247f);
        }

        else if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1.840318f, 1.813247f);
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
}
