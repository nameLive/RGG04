using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : CollectibleBase
{
    [SerializeField]
    float hammerDuration = 5f;

    [SerializeField]
    private GameObject spawnPoints;

    GameManager gameManager;
    Animator anim;

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponentInChildren<Animator>();
    }


    //Because this object only interacts with the player layer it will always and only ever be the player entering and triggering this event
    private void OnTriggerEnter2D(Collider2D collision) {

        PlayerStateHandler stateHandler = collision.GetComponent<PlayerStateHandler>();

        if (collision.tag == "Player" && stateHandler) {

            gameManager.IncreaseScore(scoreValue, 0);

            GameObject spawnedPointTemp = Instantiate(spawnPoints, transform.position, Quaternion.identity);
            spawnedPointTemp.GetComponent<SpawnedPoints>().scoreAmount = scoreValue;

            stateHandler.SetHammerState(hammerDuration);
            Destroy(gameObject);
        }

    }
}
