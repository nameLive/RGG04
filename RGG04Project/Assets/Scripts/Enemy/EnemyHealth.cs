using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
	EnemyStunState stunState;

	private void Start()
	{
		base.Start();
		stunState = GetComponent<EnemyStunState>();
		stunState.EventOnBeginStun += StartStunState;
		stunState.EventOnEndStun += EndStunState;
	}

	void StartStunState()
	{
		canTakeDamage = false;
		SetMaxHealth();
	}

	void EndStunState()
	{
		canTakeDamage = true;
	}

	public void SetMaxHealth()
	{
		currentHealth = maxHealth;
	}
}
