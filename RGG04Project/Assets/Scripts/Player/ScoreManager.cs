using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    GameManager gameManager;

    //[HideInInspector]
    public int currentScore = 0;

    public int currentAmountOfDonutsPickedUp;
    
    public int minAmountOfCollectiblesRequired = 0;
    public int maxAmountOfCollectibles = 0;

    private Text donutsText;
    private Text scoreText;

    //public CollectibleBase[] collectibles;

    //----------------------------

    private void Start() {

        donutsText = GameObject.FindGameObjectWithTag("DonutText").GetComponent<Text>();
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //collectibles = GameObject.FindObjectsOfType<CollectibleBase>();

        maxAmountOfCollectibles += GameObject.FindObjectsOfType<MinorDonut>().Length; // funkar ej :( hitta nada så blir alltid / 0
        maxAmountOfCollectibles += GameObject.FindObjectsOfType<MajorDonut>().Length;

        scoreText.text = "Score: " + currentScore;
        donutsText.text = "Donuts: " + currentAmountOfDonutsPickedUp + " / " + maxAmountOfCollectibles;
    }

    //----------------------------

    public void IncreaseScore(int scoreAmount, int donutAmount) {

        currentScore += scoreAmount;

        scoreText.text = "Score: " + currentScore;

        currentAmountOfDonutsPickedUp += donutAmount;

        donutsText.text = "Donuts: " + currentAmountOfDonutsPickedUp + " / " + maxAmountOfCollectibles;

        if (currentAmountOfDonutsPickedUp >= minAmountOfCollectiblesRequired) {

            if (gameManager)
                gameManager.Win();
        }
    }
}
