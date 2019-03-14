using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("This is the damage that is dealt with a 1 second interval")]
	int damageAmount = 1;

	float canDealDamageTimer = 1f;

	HealthBase playerHealthReference;

	private void OnTriggerEnter2D(Collider2D collision)
	{

		playerHealthReference = collision.gameObject.GetComponent<HealthBase>();
		canDealDamageTimer = 1f;


	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<HealthBase>() == playerHealthReference)
		{
			playerHealthReference = null;
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

			if (playerHealthReference)
			{
				playerHealthReference.DecreaseHealth(damageAmount);

			}
		}

	}

}
