using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {

    GameManager gameManager;

    public Text scoreText;


    //---------------------------------------

    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        scoreText.text = "" + gameManager.currentScore;
    }

    //---------------------------------------

    public void ContinueButtonClicked() {

        gameManager.SetScenesToUnload(new string[] { "WinScreen" });

        gameManager.StartGame();
    }

    //---------------------------------------

    public void MainMenuButtonCliked() {

        gameManager.SetScenesToUnload(new string[] { "WinScreen" });

        gameManager.BackToMenu();
    }
}
