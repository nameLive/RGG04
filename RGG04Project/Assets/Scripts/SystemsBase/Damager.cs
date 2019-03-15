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
	float damageInterval = 1f;

	HealthBase targetHealth;

	[SerializeField]
	[Tooltip("This is the collider that checks for damageable objects")]
	Collider2D colliderReference;

    GameObject damagedObject;

    public delegate void DidDamage(GameObject ToObject);
    public event DidDamage EventOnDidDamage;


    private void Start()
    {
        EventOnDidDamage += EventOnDidDamagePlaceholder;
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
        //Debug.Log("Overlapped Thing: " + collision.gameObject.name);
        damagedObject = collision.gameObject;

		targetHealth = damagedObject.GetComponent<HealthBase>();
		if (targetHealth)
		{
			DealDamage();
			DeactivateCollider();
			Invoke("ActivateCollider", damageInterval);
			//Debug.Log("Overlapped Component with health comp: " + collision.gameObject.name);

		}
	}

	void DealDamage()
	{
		if (canDealDamage)
		{
			targetHealth.DecreaseHealth(damageAmount);
            EventOnDidDamage(damagedObject);
		}
	}

	public void ActivateCollider()
	{
		if (canDealDamage)
		{
			colliderReference.enabled = true;
		}
	}

	public void DeactivateCollider()
	{
		colliderReference.enabled = false;
	}

    void EventOnDidDamagePlaceholder(GameObject DidDamageTo)
    {
        //blabla
    }
}
