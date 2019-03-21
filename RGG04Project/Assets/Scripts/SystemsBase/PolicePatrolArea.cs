using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicePatrolArea : MonoBehaviour
{
    [SerializeField]
    GameObject patrolIdleLocation;

    [SerializeField]
    List<PoliceEnemy> patrollingPoliceEnemies;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        SetPolicePatrolling();

        player = GameObject.Find("PlayerCharacter");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            SetPoliceChasePlayer();
            //Debug.Log("Player entered patrol area");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            SetPolicePatrolling();
        }
    }


    void SetPoliceChasePlayer()
    {
        foreach (PoliceEnemy enemy in patrollingPoliceEnemies)
        {
            enemy.SetStateChasePlayer();
        }
    }

    void SetPolicePatrolling()
    {
        foreach (PoliceEnemy enemy in patrollingPoliceEnemies)
        {
            enemy.SetStatePatroling(patrolIdleLocation);
        }
    }

}
