using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public int amountOfCollectibles;

    public bool isPowerUp;
    public float powerUpLengthOfTime;


    public void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.tag == "Player") {

            if (!isPowerUp)
                other.GetComponent<PlayerPickup>().PickupCollectible(amountOfCollectibles);
            else
                other.GetComponent<PlayerPickup>().PowerUp(powerUpLengthOfTime);

            Destroy(this.gameObject);
        }
    }
}
