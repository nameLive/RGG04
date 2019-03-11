using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState { StartingGame, InMainMenu, InPauseMenu, LoadingGame, InGame };
    public GameState gameState;

    public GameObject PlayerToSpawn;

    
    void Start() {
        
    }
    
    void Update()
    {
        
    }

    public void Win() {

    }
}
