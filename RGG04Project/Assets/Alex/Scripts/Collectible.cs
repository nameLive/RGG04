using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {


    private enum TypeOfCollectible {Normal, Invincible, Hammer};

    [SerializeField]
    private TypeOfCollectible typeOfCollectible = TypeOfCollectible.Normal;

    [SerializeField]
    private int amountOfCollectibles = 0;

    [SerializeField]
    private float powerUpLengthOfTime = 0;


    //---------------------------------------
    // Constructor

    Collectible() {

    }

    //---------------------------------------
    // When Colliding with player, calls function depending on what type of collectible it is. 

    public void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.tag == "Player") {

            switch (typeOfCollectible) {

                case TypeOfCollectible.Normal:
                    other.GetComponent<PlayerPickup>().PickupCollectible(amountOfCollectibles);
                    break;

                case TypeOfCollectible.Invincible:
                    other.GetComponent<PlayerPickup>().PowerUp(PlayerPickup.PlayerState.Invincible, powerUpLengthOfTime);
                    break;

                case TypeOfCollectible.Hammer:
                    other.GetComponent<PlayerPickup>().PowerUp(PlayerPickup.PlayerState.HaveHammer, powerUpLengthOfTime);
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
