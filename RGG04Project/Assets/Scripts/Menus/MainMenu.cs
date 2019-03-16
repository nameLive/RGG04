using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    GameManager gameManager;
    
    //----------------------------------------------

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    //----------------------------------------------

    public void StartGameButtonClicked() {

        gameManager.SetScenesToUnload(new string[] { "MainMenu" });

        gameManager.StartGame();
    }

    //----------------------------------------------

    public void QuitGameButtonClicked() {

        Application.Quit();
    }
}
