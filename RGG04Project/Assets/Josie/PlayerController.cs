using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public string horizontalKey = "Horizontal";
    

    private void Update()
    {
        float horizontal = Input.GetAxis(horizontalKey);
        if (!Mathf.Approximately(horizontal, 0f))
        {
            Move(horizontal);
        }

    }
    

    private void Move(float horizontal)
    {
        transform.Translate(horizontal * speed * Time.deltaTime, 0f, 0f);
        Vector2 position = transform.position;
        transform.position = position;
    }
    
    
}
