using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : Damager
{
	PoliceEnemy policeEnemy;

	[SerializeField]
	float onHitStopDuration = 2f;

	protected override void Start()
	{
		base.Start();
		policeEnemy = GetComponentInParent<PoliceEnemy>();
	}

	protected override void EventOnDidDamagePlaceholder(GameObject DidDamageTo)
	{
		//base.EventOnDidDamagePlaceholder(DidDamageTo);

		policeEnemy.SetStateNoneForDuration(onHitStopDuration);

		//PlayerHealth playerHealth = DidDamageTo.GetComponent<PlayerHealth>();
		//if (playerHealth)
		//{
		//	Debug.Log("Did damage to player: " + DidDamageTo.name);

		//}
	}


}
