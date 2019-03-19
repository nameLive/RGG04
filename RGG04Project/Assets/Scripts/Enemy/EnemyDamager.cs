using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : Damager
{
	PoliceEnemy policeEnemy;

	[SerializeField]
	float onHitStopDuration = 2f;

	protected void Start()
	{
		base.Start();
		policeEnemy = GetComponentInParent<PoliceEnemy>();
	}

	protected override void EventOnDidDamagePlaceholder(GameObject DidDamageTo)
	{
		policeEnemy.SetStateNoneForDuration(onHitStopDuration);
	}


}
