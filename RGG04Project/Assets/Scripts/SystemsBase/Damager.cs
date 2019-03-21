using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
	public bool canDealDamage = true;

	[SerializeField]
	[Tooltip("This is the damage it deals at the interval of \"DamageInterval\" ")]
	int damageAmount = 1;

	[SerializeField]
	[Tooltip("This is the interval(In Seconds) at which it deals damage if still overlapping")]
	float damageInterval = 0.1f;

	HealthBase targetHealth;

	[SerializeField]
	[Tooltip("This is the collider that checks for damageable objects")]
	Collider2D colliderReference;

	GameObject damagedObject;

	public delegate void DidDamage(GameObject ToObject);
	public event DidDamage EventOnDidDamage;


	protected virtual void Start()
	{
		EventOnDidDamage += EventOnDidDamagePlaceholder;
	}

	/*
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Overlapped Thing: " + collision.gameObject.name);
            damagedObject = collision.gameObject;

            targetHealth = damagedObject.GetComponent<HealthBase>();
            if (targetHealth)
            {
                DealDamage();
                DeactivateCollider();
                Invoke("ActivateCollider", damageInterval);
                Debug.Log("Overlapped Component with health comp: " + collision.gameObject.name);

            }
        }*/

	/*

		private void OnTriggerEnter2D(Collider2D collision)
		{

			StartCoroutine("DealDamageThing");
		}

		int myInt = 0;

		IEnumerator DealDamageThing()
		{
			while (true)
			{
				Debug.Log("Damage Coroutine");
				myInt++;
				if (myInt >= 10)
				{
					StopCoroutine("DealDamageThing");
				}
				else
				{
					yield return new WaitForSeconds(damageInterval);

				}
			}
		}*/

	private void OnTriggerStay2D(Collider2D collision)
	{

		if (collision.gameObject != damagedObject)
		{

			damagedObject = collision.gameObject;
			targetHealth = damagedObject.GetComponent<HealthBase>();
		}

		if (targetHealth)
		{
			DealDamage();
		}
	}

	void DealDamage()
	{
		if (canDealDamage)
		{
			bool didDamage = false;
			didDamage = targetHealth.DecreaseHealth(damageAmount);
			if (didDamage)
			{
				//CallEventOnDidDamage(damagedObject);
				//EventOnDidDamagePlaceholder(damagedObject);
				EventOnDidDamage(damagedObject);

			}


		}
	}

	protected virtual void EventOnDidDamagePlaceholder(GameObject DidDamageTo)
	{
		//blabla
	}
}
