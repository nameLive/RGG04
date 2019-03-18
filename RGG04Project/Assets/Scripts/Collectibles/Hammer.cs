using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : CollectibleBase
{
    [SerializeField]
    float hammerDuration = 5f;

    GameManager gameManager;

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    //Because this object only interacts with the player layer it will always and only ever be the player entering and triggering this event
    private void OnTriggerEnter2D(Collider2D collision) {

        PlayerStateHandler stateHandler = collision.GetComponent<PlayerStateHandler>();

        if (collision.tag == "Player" && stateHandler) {

            gameManager.IncreaseScore(scoreValue, 0);
            
            stateHandler.SetHammerState(hammerDuration);
            Destroy(gameObject);
        }

    }
}
