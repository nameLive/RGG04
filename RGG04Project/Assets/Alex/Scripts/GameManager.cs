using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum GameState { StartingGame, InMainMenu, InPauseMenu, LoadingGame, InGame };
    public GameState gameState;

    public GameObject PlayerToSpawn;

    public string[] scenesToLoad = new string[0];

    public string[] scenesToUnload = new string[0];


    //-----------------------------------------------

    void Start() {

        //StartCoroutine (UnloadScenes(scenesToUnload)); // To Unload Levels
        

        //StartCoroutine(LoadScenesTest(scenesToLoad)); // To Load Levels
    }
    
    //-----------------------------------------------
    // Takes in Array of Strings with the scenes that is going to be loaded

    IEnumerator LoadScenesTest(string[] scenesToLoad) {

        AsyncOperation ao;

        for (int i = 0; i < scenesToLoad.Length; i++) {


            // Before Level Loaded

            ao = SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);

            SceneManager.sceneLoaded += OnSceneLoaded;

            yield return ao;

            // After Level Loaded
        }

        // After All Level Finished Loading

    }

    //-----------------------------------------------
    // Unloading Array of levels, after for loop all the levels have been unloaded

    IEnumerator UnloadScenes(string[] scenesToUnload) {

        AsyncOperation ao;

        for (int i = 0; i < scenesToUnload.Length; i++) {


            // Before Unloading Level in Array

            ao = SceneManager.UnloadSceneAsync(scenesToUnload[i]);

            SceneManager.sceneLoaded -= OnSceneLoaded;

            yield return ao;

            // After Unloaded Level in Array
        }
        
        // All Levels Finished Unloading
        
    }

    //-----------------------------------------------
    // Win Condition

    public void Win() {

    }
}
