using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("This is the damage that is dealt with a 1 second interval")]
	int damageAmount = 1;

	float canDealDamageTimer = 1f;

	EnemyHealth enemyHealthReference;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		enemyHealthReference = collision.gameObject.GetComponent<EnemyHealth>();
		canDealDamageTimer = 1f;
	}


	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<EnemyHealth>() == enemyHealthReference)
		{
			enemyHealthReference = null;
		}
	}

	private void Update()
	{
		Timer();
	}

	void Timer()
	{
		canDealDamageTimer += Time.deltaTime;

		if (canDealDamageTimer >= 1f)
		{
			canDealDamageTimer = 0f;

			if (enemyHealthReference)
			{
				enemyHealthReference.DecreaseHealth(damageAmount);

			}
		}
	}
}
