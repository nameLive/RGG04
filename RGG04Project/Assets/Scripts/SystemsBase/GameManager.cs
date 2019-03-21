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

    [SerializeField]
    private GameObject arrowPointingToDoor;

    private GameObject winDoor;
    
    public int wonGamesInARow;

    public float enemySpeedMultiplier = .5f;

    private float originalEnemyMovementSpeed;
    
    public int currentScore = 0;

    [SerializeField]
    private int currentAmountOfDonutsPickedUp;

    [SerializeField]
    private int minAmountOfCollectiblesRequired = 1;

    private bool hasPickedUpMinimum;

    [SerializeField]
    private int maxAmountOfCollectibles = 0;

    [SerializeField]
    private float fadeDurationWhenStartingLevel = 1f;
    
    public string[] level1;

    [SerializeField]
    private string[] levelsToLoadWhenDebug;

    private string[] scenesToLoad = new string[0];

    private string[] scenesToUnload = new string[0];

    public delegate void functionToCallDelegate();

    private bool isLoading;
    
    public bool isPaused;
    
    [SerializeField]
    private bool isDebugging;


    //-----------------------------------------------
    // If Debugging then Loads Levels to Load When Debug right away. Othewise Loads Boot Scene

    void Start() {

        originalEnemyMovementSpeed = PoliceEnemy.movementSpeed;

        for (int i = 1; i < SceneManager.sceneCount; i++) { // Unloads all scenes that are in the Persistent Level on start

            SceneManager.UnloadSceneAsync(i);
        }

        fadeImage.color = Color.black; // Sets fadeImage to be black on start.

        if (isDebugging) { 

            SetScenesToLoad(levelsToLoadWhenDebug);

            gameState = GameState.LoadingToGame;

            ScreenFade(false, 0, UnloadAndLoadScenes);
        }

        else {

            SetScenesToLoad(new string[] { "Boot" }); // Sets the added field to be Boot so it loads Boot Scene

            ScreenFade(false, .5f, UnloadAndLoadScenes);
        }
    }

    //-----------------------------------------------
    // Input for Pause Menu

    void Update() {

        if (Input.GetButtonDown("Cancel")) {

            /*if (gameState == GameState.InGame)
                PauseGame(true);
                */
            if (gameState == GameState.InPauseMenu) {

                PauseGame(true);

                SetScenesToUnload(level1);

                ScreenFade(false, .5f, BackToMenu);

            }

            else if (gameState == GameState.AtLoseScreen) {

                SetScenesToUnload(new string[] { "LoseScreen" });
                BackToMenu();
            }

            else if (gameState == GameState.AtWinScreen) {

                SetScenesToUnload(new string[] { "WinScreen" });
                BackToMenu();
            }

            else if (gameState == GameState.InMainMenu) {

                Application.Quit();
            }

        }

        else if (Input.GetButtonDown("Submit")) {

            if (gameState == GameState.InGame)
                PauseGame(true);

            else if (gameState == GameState.InPauseMenu)
                PauseGame(true);

            else if (gameState == GameState.AtLoseScreen) {

                SetScenesToUnload(new string[] { "LoseScreen" });
                currentScore = 0;
                StartGame();
            }

            else if (gameState == GameState.AtWinScreen) {

                SetScenesToUnload(new string[] { "WinScreen" });
                StartGame();
            }

            else if (gameState == GameState.InMainMenu) {

                SetScenesToUnload(new string[] { "MainMenu" });

                StartGame();
            }
        }
    }

    //-----------------------------------------------
    // Pauses / UnPauses the game. If Paused by Input from player then opens Pause Menu

    public void PauseGame(bool pausedByInput) {
        
        if (isPaused) { // UnPauses

            isPaused = false;
            Time.timeScale = 1;

            if (pausedByInput) {

                gameState = GameState.InGame;
                pauseMenu.SetActive(false);
            }
        }

        else { // Pauses

            isPaused = true;
            Time.timeScale = 0;

            if (pausedByInput) {
                gameState = GameState.InPauseMenu;
                pauseMenu.SetActive(true);
            }
        }
    }

    //-----------------------------------------------
    // Boot Menu Finished. Called from BootMenu. Sets scenes to unload and load, state and fades out. 

    public void BootMenuFinished() {

        SetScenesToUnload(new string[] { "Boot" });

        SetScenesToLoad(new string[] { "MainMenu" });

        gameState = GameState.LoadingToMenu;

        ScreenFade(false, .5f, UnloadAndLoadScenes);
    }

    //-----------------------------------------------
    // Main Menu. Called when it finished loading and fading out to it. 

    void InMainMenu() {

        PoliceEnemy.movementSpeed = originalEnemyMovementSpeed;
        wonGamesInARow = 0;
        currentScore = 0;
        currentAmountOfDonutsPickedUp = 0;

        gameState = GameState.InMainMenu;
    }

    //-----------------------------------------------
    // Back to Main Menu. Called from Pause Menu, Win Screen and Lose Screen

    public void BackToMenu() {

        gameState = GameState.LoadingToMenu;

        StartCoroutine(UnloadScenes(null, null));

        SetScenesToLoad(new string[] { "MainMenu" });

        ScreenFade(false, .5f, UnloadAndLoadScenes);
    }

    //-----------------------------------------------
    // Calls the Fade Screen Coroutine that can call a function when the fade is finished. 

    public void ScreenFade(bool fadeIn, float duration, functionToCallDelegate functionToCallAfterFade) {

        StartCoroutine(FadeScreen(fadeIn, duration, functionToCallAfterFade));
    }

    //-----------------------------------------------
    // Starts Game. Gets called from Main Menu, Win Screen and Lose Screen

    public void StartGame() {

        SetScenesToLoad(level1);

        gameState = GameState.LoadingToGame;

        ScreenFade(false, .5f, UnloadAndLoadScenes);
    }

    //-----------------------------------------------
    // Unloads and Loads Scenes. Calls Load Screen Before and After Loading scenes.  

    void UnloadAndLoadScenes() {

        if (scenesToUnload != null)
            StartCoroutine(UnloadScenes(null, null));

        if (scenesToLoad != null)
            StartCoroutine(LoadScenes(LoadScreen, LoadScreen));
    }

    //-----------------------------------------------
    // Load Screen gets called before and after scenes are loaded

    void LoadScreen() {
        
        if (isLoading) { // Called after Scenes Loading is Finished, depending on state it fades in screen and calls specific function

            isLoading = false;

            switch (gameState) {

                case GameState.LoadingToGame:

                    if (isDebugging)
                        ScreenFade(true, 0, InGame);
                    else
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

                case GameState.LostGame:

                    HUD.SetActive(false);

                    ScreenFade(true, fadeDurationWhenStartingLevel, LostScreen);

                    break;
            }
        }

        else if (isLoading == false) { // Called before scenes have started loading

            isLoading = true;

             switch (gameState) {

                case GameState.LoadingToGame:
                    
                    break;

                case GameState.LoadingToMenu:
                    
                    break;

                case GameState.WonGame:

                    break;
            }
        }
    }

    //-----------------------------------------------
    // When in game, called when loading is finished when LoadingToGame was true

    void InGame() {

        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHealth>().EventOnDeath += LostGame;

        winDoor = GameObject.FindGameObjectWithTag("WinDoor");

        gameState = GameState.InGame;

        HUD.SetActive(true);

        hasPickedUpMinimum = false;

        currentAmountOfDonutsPickedUp = 0;

        maxAmountOfCollectibles = GameObject.FindObjectsOfType<MajorDonut>().Length;

        scoreText.text = "" + currentScore;
        donutsText.text = "" + currentAmountOfDonutsPickedUp + " / " + maxAmountOfCollectibles;
    }
    
    //-----------------------------------------------
    // Sets scenes to are going to be loaded

    public void SetScenesToLoad (string[] setScenesToLoad) {

        scenesToLoad = setScenesToLoad;
    }

    //-----------------------------------------------
    // Loads Array of Scenes. Can call functions before load starts and after its finished

    IEnumerator LoadScenes(functionToCallDelegate methodToCallBeforeLoading, functionToCallDelegate methodToCallAfterLoadComplete) {

        AsyncOperation ao;
        
        if (methodToCallBeforeLoading != null) // Before Any level has started loading
            methodToCallBeforeLoading();
        
        for (int i = 0; i < scenesToLoad.Length; i++) {
            
            // Before Level in array is Loaded

            ao = SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);

            yield return ao;

            // After Level in array is Loaded
        }
        
        scenesToLoad = null; // Clears the array when finished loading the levels

        //yield return new WaitForSecondsRealtime(2); // After all levels finished loading, can wait additional seconds before continuing

        if (methodToCallAfterLoadComplete != null) // After All Level Finished Loading
            methodToCallAfterLoadComplete();
    }

    //-----------------------------------------------
    // Sets scenes that are going to be Unloaded

    public void SetScenesToUnload(string[] setScenesToUnload) {

        scenesToUnload = setScenesToUnload;
    }

    //-----------------------------------------------
    // UnLoads Array of Scenes. Can call functions before load starts and after its finished

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
    // Increasing of Score. Checks amount of donuts collected, if enough then opens win door. 

    public void IncreaseScore(int scoreAmount, int donutAmount = 0) {

        currentScore += scoreAmount;

        scoreText.text = "" + currentScore;

        currentAmountOfDonutsPickedUp += donutAmount;

        donutsText.text = "" + currentAmountOfDonutsPickedUp + " / " + maxAmountOfCollectibles;


        if (donutAmount > 0) {

            if (currentAmountOfDonutsPickedUp >= minAmountOfCollectiblesRequired) {

                if (!hasPickedUpMinimum) {

                    winDoor.GetComponent<WinDoor>().ActivateWinZone();

                    Instantiate(arrowPointingToDoor, transform.position, Quaternion.identity);

                    hasPickedUpMinimum = true;
                }
            }
        }
    }

    //-----------------------------------------------
    // Called from Win Door when entered its trigger. Sets State, Pauses Game and sets which scene to load after fade out

    public void EnteredWinTrigger() {

        gameState = GameState.WonGame;

        PauseGame(false); // Pauses the game when getting the last donut

        StartCoroutine(PauseDelayBeforeGettingToScreen(1.5f, "WinScreen")); // Delay that ignores the pause
    }

    //-----------------------------------------------
    // Win Screen. Called when after Loading Win Screen when Entered Win Trigger

    void WinScreen() {
        
        wonGamesInARow++;

        if (PoliceEnemy.movementSpeed < .11f)
            PoliceEnemy.movementSpeed += originalEnemyMovementSpeed * enemySpeedMultiplier;

        gameState = GameState.AtWinScreen;
    }

    //-----------------------------------------------
    // Lost Game. Sets state, pauses game and which screen to load after fade out.  CURRENTLY CALLED WHEN PRESSING K. 

    public void LostGame() { // This code could be in the player char
        
        if (gameState != GameState.LostGame) {

            gameState = GameState.LostGame;

            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;

            PauseGame(false); // Pauses Game while playing death anim

            // Play Death Animation

            StartCoroutine(PauseDelayBeforeGettingToScreen(1.75f, "LoseScreen"));
        }
    }

    //-----------------------------------------------
    // Lost Screen. Called when finished loading LoseScreen. 

    void LostScreen() {

        wonGamesInARow = 0;

        PoliceEnemy.movementSpeed = originalEnemyMovementSpeed;

        gameState = GameState.AtLoseScreen;
    }

    //-----------------------------------------------
    // Delay before Unpausing and Fading Screen Out and unloading. 

    IEnumerator PauseDelayBeforeGettingToScreen(float delay, string screenToGetLoad) {

        yield return new WaitForSecondsRealtime(delay);

        PauseGame(false); // Un Pauses it

        SetScenesToUnload(level1);

        SetScenesToLoad(new string[] { screenToGetLoad });

        ScreenFade(false, .15f, UnloadAndLoadScenes);
    }
}
