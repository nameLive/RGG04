using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
	public void SetMaxHealth()
	{
		currentHealth = maxHealth;
		Debug.Log("Enemy Current Health: " + health);
	}
}
