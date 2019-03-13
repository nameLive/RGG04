using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootMenu : MonoBehaviour {

    GameManager gameManager;

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void BootMenuFinished() {

        gameManager.BootMenuFinished();
    }
}
