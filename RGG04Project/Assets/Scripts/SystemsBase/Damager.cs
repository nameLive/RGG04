using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("This is the damage it deals at the interval of \"DamageInterval\" ")]
	int damageAmount = 1;

	[SerializeField]
	[Tooltip("This is the interval(In Seconds) at which it deals damage if still overlapping")]
	float damageInterval = 1f;

	HealthBase targetHealth;

	[SerializeField]
	[Tooltip("This is the collider that checks for damageable objects")]
	Collider2D colliderReference;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("Overlapped Thing: " + collision.gameObject.name);

		targetHealth = collision.gameObject.GetComponent<HealthBase>();
		if (targetHealth)
		{
			DealDamage();
			colliderReference.enabled = false;
			Invoke("ActivateCollider", damageInterval);
			//Debug.Log("Overlapped Component with health comp: " + collision.gameObject.name);

		}
	}

	void DealDamage()
	{
		targetHealth.DecreaseHealth(damageAmount);
	}

	void ActivateCollider()
	{
		colliderReference.enabled = true;
	}
}
