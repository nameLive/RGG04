using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetection : MonoBehaviour
{
    public float jumpVelocity;
    public string jumpKey = "Jump";
    public float groundedCast = 0.05f;
    public LayerMask mask; //mask is not refering to/getting the correct layer number

    bool jumpRequest;
    bool grounded;

    Vector2 playerSize;
    Vector2 boxSize;


    void Awake()
    {
        playerSize = GetComponent<BoxCollider2D>().bounds.size;
        boxSize = new Vector2(playerSize.x, groundedCast);
    }


    void Update()
    {
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
            grounded = false;
        }

        else
        {
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0, mask) != null);  //mask is not returning the correct layer number
        }
    }
}
