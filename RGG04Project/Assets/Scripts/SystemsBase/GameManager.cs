using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

    public enum GameState { StartingGame, InMainMenu, InPauseMenu, LoadingGame, InGame };

    public GameState gameState;

    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private GameObject HUD;

    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    private Text donutsText;

    [SerializeField]
    private Text scoreText;
    
    [SerializeField]
    private int currentScore = 0;

    [SerializeField]
    private int currentAmountOfDonutsPickedUp;

    [SerializeField]
    private int minAmountOfCollectiblesRequired = 1;

    [SerializeField]
    private int maxAmountOfCollectibles = 0;

    [SerializeField]
    private float fadeDurationWhenStartingLevel = 1f;

    [SerializeField]
    private string[] level1;

    [SerializeField]
    private string[] levelsToLoadWhenDebug;

    private string[] scenesToLoad = new string[0];

    private string[] scenesToUnload = new string[0];

    private delegate void functionToCallDelegate();

    [SerializeField]
    private bool IsPaused;
    
    [SerializeField]
    private bool isDebugging;


    //-----------------------------------------------
    // If Debugging then starts the game right away, otherwise loads the Boot Menu

    void Start() {

        for (int i = 1; i < SceneManager.sceneCount; i++) { // Unloads all scenes that are in the Persistent Level on start

            SceneManager.UnloadSceneAsync(i);
        }

        fadeImage.color = Color.black; // Sets fadeImage to be black on start. It cant be it otherwise because then you cant edit stuff

        if (isDebugging) { // If debugs then instnatly loads level 1 and starts game

            fadeImage.color = Color.black; // Sets fade to be black at start so it can fade in if wanted

            SetScenesToLoad(levelsToLoadWhenDebug); 
            LoadGame();
        }

        else {

            SetScenesToLoad(new string[] { "Boot" }); // Sets the added field to be Boot so it loads Boot Scene

            StartCoroutine(LoadScenes(null, null));
        }
    }

    //-----------------------------------------------

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            
            if (gameState == GameState.InGame) {

                PauseGame();
            }
        }
    }

    //-----------------------------------------------

    void PauseGame() {

        var ss = FindObjectsOfType<MonoBehaviour>().OfType<PauseInterface>();

        foreach (PauseInterface s in ss) { // Loops through all objects using PauseInterface

            if (IsPaused)
                s.UnPaused();

            else
                s.Paused();
        }

        // When loop is done

        if (IsPaused)
            IsPaused = false;

        else
            IsPaused = true;
    }

    //-----------------------------------------------
    // Unloads Boot Menu, Fades screen In, sets state to be Main Menu, Loads Main Menu

    public void BootMenuFinished() {

        SetScenesToUnload(new string[] { "Boot" });

        StartCoroutine(UnloadScenes(null, null));
        
        StartCoroutine(FadeScreen(true, 1, null));

        gameState = GameState.InMainMenu;

        SetScenesToLoad(new string[] { "MainMenu" });

        StartCoroutine(LoadScenes(null, null));
    }
    

    //-----------------------------------------------
    // Fade screen In/Out with a certain duration and can call a function after finished

    IEnumerator FadeScreen(bool fadeIn, float duration, functionToCallDelegate functionToCallAfterFade) {

        if (fadeIn)
            fadeImage.CrossFadeAlpha(0, duration, true); 
        else
            fadeImage.CrossFadeAlpha(1, duration, true);
            
        
        yield return new WaitForSeconds(duration);

        if (functionToCallAfterFade != null)
            functionToCallAfterFade();
    }

    //-----------------------------------------------
    // Starts Game, get called from MainMenu when pressing Start Game.

    public void StartGame() {

        StartCoroutine(UnloadScenes(null, null)); // Unloads scenes

        SetScenesToLoad(level1); // Set scenes to load

        StartCoroutine(FadeScreen(false, fadeDurationWhenStartingLevel, LoadGame)); // Fades screen in, when finished it calls function LoadGame
    }

    //-----------------------------------------------
    // Sets game state to LoadingGame, LoadsScenes In

    void LoadGame() {

        gameState = GameState.LoadingGame;

        StartCoroutine(LoadScenes(LoadScreen, LoadScreen));
    }

    //-----------------------------------------------
    // Shows/Hides Loading Screen. When Hide it Fades screen in again and calls InGame when finished

    void LoadScreen() {

        if (loadingScreen.activeSelf) {

            loadingScreen.SetActive(false);
            if (isDebugging)
                StartCoroutine(FadeScreen(true, 0, InGame));
            else
                StartCoroutine(FadeScreen(true, fadeDurationWhenStartingLevel, InGame));
        }

        else
            loadingScreen.SetActive(true);
    }

    //-----------------------------------------------
    // When in game, called when loading is finished and faded out

    void InGame() {

        gameState = GameState.InGame;

        HUD.SetActive(true);

        maxAmountOfCollectibles += GameObject.FindObjectsOfType<MajorDonut>().Length;

        scoreText.text = "Score: " + currentScore;
        donutsText.text = "Donuts: " + currentAmountOfDonutsPickedUp + " / " + maxAmountOfCollectibles;
    }
    
    //-----------------------------------------------
    // Sets scenes to load

    public void SetScenesToLoad (string[] setScenesToLoad) {

        scenesToLoad = setScenesToLoad;
    }

    //-----------------------------------------------
    // Takes in Array of Strings with the scenes that is going to be loaded

    IEnumerator LoadScenes(functionToCallDelegate methodToCallBeforeLoading, functionToCallDelegate methodToCallAfterLoadComplete) {

        AsyncOperation ao;

        // Before Any level has started loading

        if (methodToCallBeforeLoading != null)
            methodToCallBeforeLoading();
        
        for (int i = 0; i < scenesToLoad.Length; i++) {
            
            // Before Level in array is Loaded

            ao = SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);

            yield return ao;

            // After Level in array is Loaded
        }

        // After All Level Finished Loading

        scenesToLoad = null; // Clears the array when finished loading the levels

        //yield return new WaitForSecondsRealtime(2); // After all levels finished loading, waits 2 additional seconds

        if (methodToCallAfterLoadComplete != null)
            methodToCallAfterLoadComplete();
    }

    //-----------------------------------------------
    // Set scenes to Unload

    public void SetScenesToUnload(string[] setScenesToUnload) {

        scenesToUnload = setScenesToUnload;
    }

    //-----------------------------------------------
    // Unloading Array of levels, after for loop all the levels have been unloaded

    IEnumerator UnloadScenes(functionToCallDelegate methodToCallBeforeUnLoading, functionToCallDelegate methodToCallAfterUnLoadComplete) {

        AsyncOperation ao;

        if (methodToCallBeforeUnLoading != null)
            methodToCallBeforeUnLoading();

        for (int i = 0; i < scenesToUnload.Length; i++) {


            // Before Unloading Level in Array

            ao = SceneManager.UnloadSceneAsync(scenesToUnload[i]);

            yield return ao;

            // After Unloaded Level in Array
        }

        scenesToUnload = null; // Clears the array when finished unloading

        if (methodToCallAfterUnLoadComplete != null)
            methodToCallAfterUnLoadComplete();

        // All Levels Finished Unloading

    }

    //-----------------------------------------------
    // Increasing of Score, checks if enough amount is collected and will if so call Win();

    public void IncreaseScore(int scoreAmount, int donutAmount) {

        currentScore += scoreAmount;

        scoreText.text = "Score: " + currentScore;

        currentAmountOfDonutsPickedUp += donutAmount;

        donutsText.text = "Donuts: " + currentAmountOfDonutsPickedUp + " / " + maxAmountOfCollectibles;


        if (donutAmount > 0) { // If its thats been picked up

            if (currentAmountOfDonutsPickedUp >= minAmountOfCollectiblesRequired) {

                Win();
            }
        }
    }

    //-----------------------------------------------
    // Win Condition

    public void Win() {

        Debug.Log("Win");
    }
}
