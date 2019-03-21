using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : Damager
{
	
	int OnEnemyHitScoreValue = 50;

	int OnEnemyStunScoreValue = 500;

	GameManager gameManager;

	protected override void Start()
	{
		base.Start();

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Debug.Log("game manager is: " + gameManager.name);

	}

	protected override void EventOnDidDamagePlaceholder(GameObject DidDamageTo)
	{

		PoliceEnemy policeEnemyHit = DidDamageTo.GetComponentInParent<PoliceEnemy>();

		if (policeEnemyHit)
		{
			
			if (policeEnemyHit.state == PatrolingPoliceStateEnum.Stunned)
			{
				gameManager.IncreaseScore(OnEnemyStunScoreValue);
				Debug.Log("Increased score by: " + OnEnemyStunScoreValue);
			}
			else
			{
				gameManager.IncreaseScore(OnEnemyHitScoreValue);
				Debug.Log("Increased score by: " + OnEnemyHitScoreValue);
			}
		}

		//gameManager.IncreaseScore()

	}


}
