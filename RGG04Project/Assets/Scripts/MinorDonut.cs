using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorDonut : CollectibleBase
{
    //Because this object only interacts with the player layer it will always and only ever be the player entering and triggering this event
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<ScoreManager>()?.IncreaseScore(scoreValue);
        Destroy(gameObject);
    }
}
