using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour {


    public enum PlayerState {Normal, HaveHammer, Invincible, Killed};
    public PlayerState playerState = PlayerState.Normal;
    
    [SerializeField]
    private int amountOfCollectiblesPickedUp = 0;

    [SerializeField]
    private int minAmountOfCollectiblesRequired = 0;
    private int maxAmountOfCollectibles = 0;
    
    public Text amountOfDonuts;

    Coroutine powerUpCoroutine;

    //---------------------------------------
    // Constructor

    PlayerPickup() {


    }

    //---------------------------------------

    void Start() {

        maxAmountOfCollectibles = GameObject.FindObjectsOfType<Collectible>().Length;

        amountOfDonuts.text = "Donuts: " + amountOfCollectiblesPickedUp + " / " + maxAmountOfCollectibles;
    }

    //---------------------------------------

    void Update() {
        
    }

    //---------------------------------------
    // Gets called by Collectible when colliding. Adds to Amount of Collectibles, sets text, checks amount

    public void PickupCollectible(int amount) {
        
        amountOfCollectiblesPickedUp += amount;

        amountOfDonuts.text = "Donuts: " + amountOfCollectiblesPickedUp + " / " + maxAmountOfCollectibles;

        if (amountOfCollectiblesPickedUp >= minAmountOfCollectiblesRequired) {

            Debug.Log("gg no re");
        }
    }

    //---------------------------------------
    // Sets Player Into Power Up State and calls coroutines

    public void PowerUp(PlayerState powerUpToSetPlayerIn, float amountOfTimePoweredUp) {

        if (playerState == PlayerState.Normal) { // If Normal, start coroutine

            playerState = powerUpToSetPlayerIn;
            
            powerUpCoroutine = StartCoroutine(PowerUpTime(amountOfTimePoweredUp));
        }

        else if (playerState == PlayerState.HaveHammer || playerState == PlayerState.Invincible) { // If already powered up, Stops Coroutine then starts it again

            playerState = powerUpToSetPlayerIn;

            StopCoroutine(powerUpCoroutine); 
            powerUpCoroutine = StartCoroutine(PowerUpTime(amountOfTimePoweredUp));
        }
    }

    //---------------------------------------
    // Count Down for Power Up

    IEnumerator PowerUpTime(float powerUpLength) {

        Debug.Log("Before Power Up");

        yield return new WaitForSeconds(powerUpLength);

        Debug.Log("After powerup is done");

        playerState = PlayerState.Normal;

        //return null;
    }
}
