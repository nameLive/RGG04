using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LooseScreen : MonoBehaviour {

    GameManager gameManager;

    public Text scoreText;


    //--------------------------------------

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        scoreText.text = "" + gameManager.currentScore;
    }

    //--------------------------------------

    public void RetryButtonClicked() {

        gameManager.SetScenesToUnload(new string[] { "LoseScreen" });

        gameManager.currentScore = 0;

        gameManager.StartGame();
    }

    //--------------------------------------

    public void MainMenuButtonClicked() {

        gameManager.SetScenesToUnload(new string[] { "LoseScreen" });

        gameManager.BackToMenu();
    }

}
