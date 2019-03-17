using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

    public GameState gameState;

    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private GameObject HUD;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private Text donutsText;

    [SerializeField]
    private Text scoreText;
    
    public int currentScore = 0;

    [SerializeField]
    private int currentAmountOfDonutsPickedUp;

    [SerializeField]
    private int minAmountOfCollectiblesRequired = 1;

    [SerializeField]
    private int maxAmountOfCollectibles = 0;

    [SerializeField]
    private float fadeDurationWhenStartingLevel = 1f;
    
    public string[] level1;

    [SerializeField]
    private string[] levelsToLoadWhenDebug;

    public string[] scenesToLoad = new string[0];

    public string[] scenesToUnload = new string[0];

    public delegate void functionToCallDelegate();

    private bool isLoading;
    
    private bool isPaused;
    
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

            gameState = GameState.LoadingToGame;

            Loading();
        }

        else {

            SetScenesToLoad(new string[] { "Boot" }); // Sets the added field to be Boot so it loads Boot Scene

            StartCoroutine(LoadScenes(null, null));
        }
    }

    //-----------------------------------------------

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            
            if (gameState == GameState.InGame)

                PauseGame(true);

            else if (gameState == GameState.InPauseMenu)
                PauseGame(true);
        }
    }

    //-----------------------------------------------

    public void PauseGame(bool pausedByInput) {
      
        /* // Turns out this was unncessary as it affected everything by just doing it in any script what so ever
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<PauseInterface>();

        foreach (PauseInterface s in ss) { // Loops through all objects using PauseInterface

            if (isPaused)
                s.UnPaused();

            else
                s.Paused();
        }
        */
        // When loop is done
        
        if (isPaused) {

            isPaused = false;

            Time.timeScale = 1; // Sets Time to normal

            if (pausedByInput) {

                gameState = GameState.InGame;
                pauseMenu.SetActive(false);
            }

        }

        else {
            isPaused = true;

            Time.timeScale = 0; // Sets Time to paused

            if (pausedByInput) {
                gameState = GameState.InPauseMenu;
                pauseMenu.SetActive(true);
            }
        }
            
    }

    //-----------------------------------------------
    // Unloads Boot Menu, Fades screen In, sets state to be Main Menu, Loads Main Menu

    public void BootMenuFinished() {

        SetScenesToUnload(new string[] { "Boot" });

        StartCoroutine(UnloadScenes(null, null));

        ScreenFade(true, 1, null);

        SetScenesToLoad(new string[] { "MainMenu" });

        gameState = GameState.LoadingToMenu;

        StartCoroutine(LoadScenes(null, InMainMenu));
    }

    //-----------------------------------------------

    void InMainMenu() {

        currentScore = 0;
        currentAmountOfDonutsPickedUp = 0;

        gameState = GameState.InMainMenu;
    }

    //-----------------------------------------------

    public void BackToMenu() {

        gameState = GameState.LoadingToMenu;

        StartCoroutine(UnloadScenes(null, null));

        SetScenesToLoad(new string[] { "MainMenu" });

        ScreenFade(false, .5f, Loading);
    }

    //-----------------------------------------------

    public void ScreenFade(bool fadeIn, float duration, functionToCallDelegate functionToCallAfterFade) {

        StartCoroutine(FadeScreen(fadeIn, duration, functionToCallAfterFade));
    }

    //-----------------------------------------------
    // Starts Game, get called from MainMenu when pressing Start Game.

    public void StartGame() {

        SetScenesToLoad(level1); // Set scenes to load

        gameState = GameState.LoadingToGame;

        ScreenFade(false, .5f, Loading);
    }

    //-----------------------------------------------
    // Sets game state to LoadingGame, LoadsScenes In

    void Loading() {

        if (scenesToUnload != null)
            StartCoroutine(UnloadScenes(null, null));

        if (scenesToLoad != null)
            StartCoroutine(LoadScenes(LoadScreen, LoadScreen));
    }

    //-----------------------------------------------
    // Shows/Hides Loading Screen, switches between them when called like a flip flop kind of

    void LoadScreen() {
        
        if (isLoading) { // If function is called and loading true, then fades in screen and removes loading screen (if any)

            isLoading = false;

            switch (gameState) {

                case GameState.LoadingToGame:

                    ScreenFade(true, fadeDurationWhenStartingLevel, InGame);

                    break;

                case GameState.LoadingToMenu:
                    
                    HUD.SetActive(false);

                    ScreenFade(true, fadeDurationWhenStartingLevel, InMainMenu);

                    break;

                case GameState.WonGame:

                    HUD.SetActive(false);

                    ScreenFade(true, fadeDurationWhenStartingLevel, WinScreen);

                    break;
            }
        }

        else if (!isLoading) { // If Function is called and is not in loading then sets it to be that and show loading screen (if any)

            isLoading = true;

             switch (gameState) {

                case GameState.LoadingToGame:

                    //StartCoroutine(FadeScreen(true, fadeDurationWhenStartingLevel, InGame));

                    

                    break;

                case GameState.LoadingToMenu:

                    //StartCoroutine(FadeScreen(true, fadeDurationWhenStartingLevel, InMainMenu));

                    break;

                case GameState.WonGame:

                    break;
            }
        }
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
    // Increasing of Score, checks if enough amount is collected and will if so call Win();

    public void IncreaseScore(int scoreAmount, int donutAmount) {

        currentScore += scoreAmount;

        scoreText.text = "Score: " + currentScore;

        currentAmountOfDonutsPickedUp += donutAmount;

        donutsText.text = "Donuts: " + currentAmountOfDonutsPickedUp + " / " + maxAmountOfCollectibles;


        if (donutAmount > 0) { // If its thats been picked up

            if (currentAmountOfDonutsPickedUp >= minAmountOfCollectiblesRequired) {

                GameObject.FindGameObjectWithTag("WinDoor").GetComponent<WinDoor>().OpenDoor(); // Opens door when all required donuts are picked up
            }
        }
    }

    //-----------------------------------------------

    public void EnteredWinTrigger() {

        gameState = GameState.WonGame;

        PauseGame(false); // Pauses the game when getting the last donut

        StartCoroutine(DelayBeforeGettingToWinScreen(5)); // Delay that ignores the pause
    }

    //-----------------------------------------------

    IEnumerator DelayBeforeGettingToWinScreen (float delay) {

        yield return new WaitForSecondsRealtime(delay);

        PauseGame(false); // Un Pauses it

        SetScenesToUnload(level1);

        SetScenesToLoad(new string[] { "WinScreen" });

        ScreenFade(false, .15f, Loading);
    }

    //-----------------------------------------------

    void WinScreen() {

        gameState = GameState.AtWinScreen;
    }
}
