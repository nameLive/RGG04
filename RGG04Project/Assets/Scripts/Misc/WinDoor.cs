using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDoor : MonoBehaviour {

    GameManager gameManager;

    public GameObject winDoorSprite;
    Animator anim;
    bool isOpen = false;

    void Start() {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponentInChildren<Animator>();
    }

    //-------------------------------

    public void ActivateWinZone() {
        
        isOpen = true;

        anim.SetBool("DoorOpen", isOpen);

        GetComponent<BoxCollider2D>().enabled = true;
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

            if (isOpen) {

                //GetComponent<AudioSource>().Play();

                isOpen = false;

                gameManager.EnteredWinTrigger();
            }
        }
    }
}
