using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    GameManager gameManager;

    public GameObject StartGameButton;

    public GameObject QuitButton;

    public GameObject CopyrightImage;

    public GameObject PressEnterImage;

    public GameObject grafitti;

    //----------------------------------------------

    void Start() {

        StartGameButton.SetActive(false);

        QuitButton.SetActive(false);

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        StartCoroutine(ButtonDelay(2));

        StartCoroutine(CopyrightDelay(15));

        StartCoroutine(Graffitti(3));
    }

    IEnumerator Graffitti (float delay) {

        yield return new WaitForSeconds(delay);

        grafitti.SetActive(true);
    }

    IEnumerator ButtonDelay (float delay) {

        yield return new WaitForSeconds(delay);

        StartGameButton.SetActive(true);

        QuitButton.SetActive(true);
    }

    IEnumerator CopyrightDelay(float delay) {

        yield return new WaitForSeconds(delay);

        CopyrightImage.SetActive(true);

        StartCoroutine(PressEnterBlink(1));
    }

    IEnumerator PressEnterBlink(float delay) {

        yield return new WaitForSeconds(delay);

        if (PressEnterImage.activeSelf)
            PressEnterImage.SetActive(false);
        else
            PressEnterImage.SetActive(true);

        StartCoroutine(PressEnterBlink(1));
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
