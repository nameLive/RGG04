using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour {


    public Image[] hearts;

    [SerializeField]
    private Sprite heartSpriteFull;

    [SerializeField]
    private Sprite heartSpriteEmpty;

    private PlayerHealth playerHealth;
    

    //------------------------------------

    void OnEnable() {

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHealth>();

        playerHealth.EventOnHealthDecreased += HealthDecreased;

        for (int i = 0; i < hearts.Length; i++) {

                hearts[i].sprite = heartSpriteFull;
        }

        // Can be a prob if HUD is hidden when at pause menu though. 
    }

    //------------------------------------

    void OnDisable() {
        
        // Reset Health here
    }

    //------------------------------------

    void HealthDecreased() {

        for (int i = 0; i < hearts.Length; i++) {

            if (i > playerHealth.health - 1)
                hearts[i].sprite = heartSpriteEmpty;
        }
    }
}
