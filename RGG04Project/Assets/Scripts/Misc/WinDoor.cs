using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDoor : MonoBehaviour {

    GameManager gameManager;

    public GameObject winDoorSprite;


    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    //-------------------------------

    public void OpenDoor() {

        StartCoroutine(OpenDoorAnim(3f));
    }

    //-------------------------------

    IEnumerator OpenDoorAnim(float openingLength) {

       

        Vector3 currentPosition = winDoorSprite.transform.position;

        Vector3 targetPosition = winDoorSprite.transform.position;

        targetPosition.y += 5f;

        float currentTime = 0;

        do {
            winDoorSprite.transform.position = Vector3.Lerp(currentPosition, targetPosition, currentTime / openingLength);

            currentTime += Time.deltaTime;

            yield return null;
        }
        while (currentTime <= openingLength);


        GetComponent<BoxCollider2D>().enabled = true;
    }

    //-------------------------------

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.gameObject.tag == "Player") {

            GetComponent<AudioSource>().Play();

            gameManager.EnteredWinTrigger();
        }
    }
}
