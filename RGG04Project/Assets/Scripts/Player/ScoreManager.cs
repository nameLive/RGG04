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

    public Text donutsText;
    public Text scoreText;

    //----------------------------

    private void Start() {

        //donutsText = GameObject.FindGameObjectWithTag("DonutText").GetComponent<Text>();
       // scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        maxAmountOfCollectibles += GameObject.FindObjectsOfType<MinorDonut>().Length; 
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
