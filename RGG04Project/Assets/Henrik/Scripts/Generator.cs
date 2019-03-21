using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    GameObject playerCharacter;
    

    void Start()
    {
        playerCharacter = GameObject.Find("PlayerCharacter");
    }
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
