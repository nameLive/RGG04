using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorDonut : CollectibleBase {

    GameManager gameManager;

    bool hasBeenPickedUp;

    Animator animator;

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        animator = GetComponent<Animator>();
    }
    
    //Because this object only interacts with the player layer it will always and only ever be the player entering and triggering this event
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player") {

            if (!hasBeenPickedUp) {

                hasBeenPickedUp = true;

                gameManager.IncreaseScore(scoreValue, 0);

                //animator.SetBool("PickedUp", true);

                StartCoroutine(DestroyAnim(.25f));
            }
        }
    }

    //------------------------------------------------
    // Lerps the scale down to 0 over a period of time

    IEnumerator DestroyAnim(float scaleTime) {

        Vector3 originalScale = gameObject.transform.localScale;

        Vector3 targetScale = new Vector3(0, 0, 0);

        float currentTime = 0;

        do {
            gameObject.transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / scaleTime);

            currentTime += Time.deltaTime;

            yield return null;
        }

        while (currentTime <= scaleTime);

        Destroy(gameObject);
    }
}
