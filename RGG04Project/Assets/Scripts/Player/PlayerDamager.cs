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

	}

	protected override void EventOnDidDamagePlaceholder(GameObject DidDamageTo)
	{

		PoliceEnemy policeEnemyHit = DidDamageTo.GetComponentInParent<PoliceEnemy>();

		if (policeEnemyHit)
		{
			
			if (policeEnemyHit.State == PoliceEnemyStateEnum.Stunned)
			{
				gameManager.IncreaseScore(OnEnemyStunScoreValue);
			}
			else
			{
				gameManager.IncreaseScore(OnEnemyHitScoreValue);
			}
		}

	}


}
