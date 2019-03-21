using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour {


    public Image[] hearts;

    private PlayerHealth playerHealth;

    void Start() {

        
    }

    void OnEnable() {

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHealth>();

        playerHealth.EventOnHealthDecreased += HealthDecreased;

        // set ful heart  health here. Can be a prob if HUD is hidden when at pause menu though. 

        // 
    }

    void OnDisable() {
        
        // Reset Health here
    }

    void HealthDecreased() {

        // Cgabge heart sprite to empty depending on HP.

        //playerHealth.health

        for (int i = 0; i < hearts.Length; i++) {

            if (i < playerHealth.health)
                Debug.Log("Test");
        }

        //hearts[playerHealth.health - 1]
    }
}
