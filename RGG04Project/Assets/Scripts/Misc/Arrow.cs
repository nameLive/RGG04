using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    GameManager gameManager;

    [SerializeField]
    private GameObject arrow;

    GameObject winDoor;

    GameObject player;

    [SerializeField]
    private float heightAbovePlayer;

    bool paused;



    //---------------------------------------------

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        winDoor = GameObject.FindGameObjectWithTag("WinDoor");

        player = GameObject.FindGameObjectWithTag("Player");
    }

    //---------------------------------------------

    void Update() {

        if (gameManager.isPaused) { // If Paused then Hides arrow
            
            if (!paused) {

                arrow.SetActive(false);
                paused = true;
            }
        }
        else {

            if (paused) { // If UnPauses then Shows it arrow

                arrow.SetActive(true);
                paused = false;
            }
        }

        if (gameManager.gameState == GameState.WonGame || gameManager.gameState == GameState.LostGame) { // Destroys it if won or lost
            Destroy(gameObject);
        }

        else {
            Vector3 targetPosition = player.transform.position; // Sets position to always be above player
            targetPosition.y += heightAbovePlayer;
            transform.position = targetPosition;

            transform.LookAt(winDoor.transform); // Sets rotation to look at door
        }
    }
}
