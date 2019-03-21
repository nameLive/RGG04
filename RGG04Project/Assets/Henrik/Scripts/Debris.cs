using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    GameObject playerCharacter;
    

    void Start()
    {
        playerCharacter = GameObject.Find("PlayerCharacter");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerCharacter)
        {
            Destroy(gameObject);
        }
    }
}
