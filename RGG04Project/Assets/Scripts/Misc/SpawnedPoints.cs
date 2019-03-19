using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedPoints : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    public int scoreAmount;

    public Sprite[] pointSprites;

    //---------------------------------------

    void Start() {
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        switch (scoreAmount) {

            case 5:
                spriteRenderer.sprite = pointSprites[0];
                break;

            case 20:
                spriteRenderer.sprite = pointSprites[1];
                break;

            case 50:
                spriteRenderer.sprite = pointSprites[2];
                break;
        }

        StartCoroutine(MoveUpwardsAndFadeOutAfterPickup());
    }

    //---------------------------------------

    IEnumerator MoveUpwardsAndFadeOutAfterPickup() {

        Vector3 currentPosition = transform.position;

        Vector3 targetPos = transform.position;
        targetPos.y += 2.5f;

        float moveLength = 3;

        float currentTime = 0;

        float spriteRenderAlphaOriginal = spriteRenderer.color.a;

        do {

            float lerp = currentTime / moveLength;

            transform.position = Vector3.Lerp(currentPosition, targetPos, lerp);

            spriteRenderer.color = new Color (spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderAlphaOriginal - lerp);

            currentTime += Time.deltaTime;

            yield return null;
        }
        while (currentTime <= moveLength);

        Destroy(gameObject);
    }
}
