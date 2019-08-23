using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BUG: If the player exits the area, sometimes is stops moving towards the patrolIdleLocation
public class PolicePatrolArea : MonoBehaviour
{
    [SerializeField]
    GameObject patrolIdleLocation;

    [SerializeField]
    List<PoliceEnemy> patrollingPoliceEnemies;

    GameObject player;

    void Start()
    {
		foreach (PoliceEnemy enemy in patrollingPoliceEnemies)
		{
			enemy.SetStatePatroling(patrolIdleLocation);
		}

		player = GameObject.Find("PlayerCharacter");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
			foreach (PoliceEnemy enemy in patrollingPoliceEnemies)
			{
				enemy.SetStateChasePlayer();
			}
			//Debug.Log("Player entered patrol area");
		}
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
			foreach (PoliceEnemy enemy in patrollingPoliceEnemies)
			{
				enemy.SetStatePatroling(patrolIdleLocation);
			}
		}
    }

}
