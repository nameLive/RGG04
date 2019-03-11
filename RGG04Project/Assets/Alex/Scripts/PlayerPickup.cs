using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour {

    public Text amountOfDonuts;

    enum PlayerState {Normal, Powered, Killed};

    PlayerState playerState = PlayerState.Normal;

    public int amountOfCollectiblesPickedUp;

    public int minAmountOfCollectiblesRequired;
    

    void Start() {
        

    }


    void Update()
    {
        
    }

    public void PickupCollectible(int amount) {
        
        amountOfCollectiblesPickedUp += amount;

        amountOfDonuts.text = "Donuts: " + amountOfCollectiblesPickedUp;

        if (amountOfCollectiblesPickedUp >= minAmountOfCollectiblesRequired) {

        }
    }

    public void PowerUp(float amountOfTimePoweredUp) {

        if (playerState == PlayerState.Normal) {

            playerState = PlayerState.Powered;
        }
    }
}
