using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    enum MainMenuButtonPressed { StartGame, Quit };

    MainMenuButtonPressed mainMenuButtonPressed;

    GameManager gameManager;

    private Animation mainMenuFade;

    private Animation mainMenuFadeOut;

    public Button startGameButton;

    public Button quitGameButton;

    //----------------------------------------------
    
    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        mainMenuFade = GetComponent<Animation>();
        
        FadeInMainMenuButtons();
    }

    //----------------------------------------------

    void FadeInMainMenuButtons() {

        mainMenuFade.Play("MainMenuButtonsFadeIn", PlayMode.StopAll);
    }

    //----------------------------------------------

    void FadeInMenuButtonsFinished() {

        startGameButton.interactable = true;
        quitGameButton.interactable = true;
    }

    //----------------------------------------------

    void FadeOutMainMenuButtons() {

        startGameButton.interactable = false;
        quitGameButton.interactable = false;

        mainMenuFade.Play("MainMenuButtonsFadeOut", PlayMode.StopAll);
    }

    //----------------------------------------------

    void FadeOutMainMenuButtonsFinished() {

        switch (mainMenuButtonPressed) {

            case MainMenuButtonPressed.StartGame:

                gameManager.SetScenesToUnload(new string[] { "MainMenu" });
                
                gameManager.StartGame();

                break;

            case MainMenuButtonPressed.Quit:

                Application.Quit();

                break;
        }
    }

    //----------------------------------------------

    public void StartGameButtonClicked() {

        mainMenuButtonPressed = MainMenuButtonPressed.StartGame;

        FadeOutMainMenuButtons();
    }

    //----------------------------------------------

    public void QuitGameButtonClicked() {

        mainMenuButtonPressed = MainMenuButtonPressed.Quit;

        FadeOutMainMenuButtons();
    }
}
