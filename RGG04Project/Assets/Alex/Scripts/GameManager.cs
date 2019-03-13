using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum GameState { StartingGame, InMainMenu, InPauseMenu, LoadingGame, InGame };
    public GameState gameState;

    public GameObject playerToSpawn;

    public GameObject loadingScreen;

    public bool isDebugging;

    private string[] scenesToLoad = new string[0];

    private string[] scenesToUnload = new string[0];

    public Image fadeImage;

    private Animator animator;


    //-----------------------------------------------

    void Start() {

        animator = fadeImage.GetComponent<Animator>();

        if (isDebugging) {

            StartGame();
        }
        else {

            SetScenesToLoad(new string[] { "Boot" }); // Sets the added field to be Boot so it loads Boot Scene

            StartCoroutine(LoadScenes(false));
        }
    }

    //-----------------------------------------------

    public void BootMenuFinished() {

        SetScenesToUnload(new string[] { "Boot" });

        StartCoroutine(UnloadScenes());

        StartCoroutine(ScreenFade(true));


        gameState = GameState.InMainMenu;

        SetScenesToLoad(new string[] { "MainMenu" });

        StartCoroutine(LoadScenes(false));
    }

    IEnumerator ScreenFade (bool fadeIn) {

        animator.SetBool("FadeOut", false);
        animator.SetBool("FadeIn", false);

        if (fadeIn) {
            animator.SetBool("FadeIn", true);

            yield return new WaitUntil(() => fadeImage.color.a == 0);

            Debug.Log("is clear");
        }

        else {

            animator.SetBool("FadeOut", true);

            yield return new WaitUntil(() => fadeImage.color.a == 1);

            Debug.Log("is black");
        }




    }

    //-----------------------------------------------

    public void StartGame() {

        StartCoroutine(UnloadScenes());

        SetScenesToLoad(new string[] { "Alex" });

        gameState = GameState.LoadingGame;

        StartCoroutine(LoadScenes(true));

        // Visa loading screen o loada level, ha att den syns i minst 1 sek

        // Efter loading är klar eller 1 sek gått så ta bort loading screen. 

        // Sätt att va i In Game State
    }

    //-----------------------------------------------

    public void SetScenesToLoad (string[] setScenesToLoad) {

        scenesToLoad = setScenesToLoad;
    }

    //-----------------------------------------------
    // Takes in Array of Strings with the scenes that is going to be loaded

    IEnumerator LoadScenes(bool loadingLevel) {

        AsyncOperation ao;

        // Before Any level has started loading

        if (loadingLevel)
            loadingScreen.SetActive(true);

        for (int i = 0; i < scenesToLoad.Length; i++) {
            
            // Before Level in array is Loaded

            ao = SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);

            yield return ao;

            // After Level in array is Loaded
        }

        // After All Level Finished Loading

        scenesToLoad = null; // Clears the array when finished loading the levels

        yield return new WaitForSecondsRealtime(5); // After all levels finished loading, waits an additional second

        if (loadingLevel)
            loadingScreen.SetActive(false);
        
    }

    IEnumerator LoadingScreen() {



        return null;
    }

    //-----------------------------------------------

    public void SetScenesToUnload(string[] setScenesToUnload) {

        scenesToUnload = setScenesToUnload;
    }

    //-----------------------------------------------
    // Unloading Array of levels, after for loop all the levels have been unloaded

    IEnumerator UnloadScenes() {

        AsyncOperation ao;

        for (int i = 0; i < scenesToUnload.Length; i++) {


            // Before Unloading Level in Array

            ao = SceneManager.UnloadSceneAsync(scenesToUnload[i]);

            yield return ao;

            // After Unloaded Level in Array
        }

        scenesToUnload = null; // Clears the array when finished unloading
        
        // All Levels Finished Unloading
        
    }

    //-----------------------------------------------
    // Win Condition

    public void Win() {

    }
}
