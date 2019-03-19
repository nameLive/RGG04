using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorDonut : CollectibleBase {

    GameManager gameManager;

    bool hasBeenPickedUp;

    Animator animator;

    [SerializeField]
    private GameObject spawnPoints;


    //------------------------------------------------

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        animator = GetComponent<Animator>();
    }

    //------------------------------------------------

    //Because this object only interacts with the player layer it will always and only ever be the player entering and triggering this event
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player") {

            if (!hasBeenPickedUp) {

                hasBeenPickedUp = true;

                gameManager.IncreaseScore(scoreValue, 1);

                GameObject spawnedPointTemp = Instantiate(spawnPoints, transform.position, Quaternion.identity);
                spawnedPointTemp.GetComponent<SpawnedPoints>().scoreAmount = scoreValue;

                animator.SetBool("PickedUp", true);
            }
        }
    }

    //------------------------------------------------

    public void PickedUpFinished() {

        Destroy(gameObject);
    }
}
