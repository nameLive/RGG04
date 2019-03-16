using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenu : MonoBehaviour {

    GameManager gameManager;
    

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    //-----------------------------------------------

    public void ResumeButtonClicked() {

        gameManager.PauseGame(true);
    }

    //-----------------------------------------------

    public void MainMenuButtonClicked() {

        gameManager.PauseGame(true);

        gameManager.SetScenesToUnload(gameManager.level1);

        gameManager.ScreenFade(false, .5f, gameManager.BackToMenu);
    }
}