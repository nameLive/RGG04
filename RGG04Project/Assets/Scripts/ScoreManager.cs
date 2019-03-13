using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    public int currentScore = 0;


    public void IncreaseScore(int Amount)
    {
        currentScore += Amount;
        Debug.Log("New Score: " + currentScore);
    }
}
