using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    GameManager gameManager;

    public GameObject StartGameButton;

    public GameObject QuitButton;

    //----------------------------------------------

    void Start() {

        StartGameButton.SetActive(false);

        QuitButton.SetActive(false);

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        StartCoroutine(ButtonDelay(2));
    }

    IEnumerator ButtonDelay (float delay) {

        yield return new WaitForSeconds(delay);

        StartGameButton.SetActive(true);

        QuitButton.SetActive(true);
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
