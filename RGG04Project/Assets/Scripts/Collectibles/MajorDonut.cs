using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorDonut : CollectibleBase {

    GameManager gameManager;

    Animator animator;

    bool hasBeenPickedUp;

    //------------------------------------------------

    void Start() {

        animator = GetComponent<Animator>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    //------------------------------------------------

    //Because this object only interacts with the player layer it will always and only ever be the player entering and triggering this event
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player") {

            if (!hasBeenPickedUp) {

                hasBeenPickedUp = true;

                gameManager.IncreaseScore(scoreValue, 1);

                animator.SetBool("PickedUp", true);
            }
        }
    }

    //------------------------------------------------
    // When Animation Finished it gets destroyed

    public void PickedUpAnimationFinished() {

        Destroy(gameObject);
    }
}
