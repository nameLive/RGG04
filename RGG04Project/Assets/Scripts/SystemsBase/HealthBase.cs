using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
	public bool canTakeDamage = true;

	[Tooltip("After damage is taken you become \"Invincible\" and can't take damage for this duration")]
	float takeDamageInterval = 1.5f;

	[SerializeField]
	protected int maxHealth = 5;

	protected int currentHealth = 5;

	public int health
	{
		get { return currentHealth; }
	}

	public delegate void OnHealthChanged();
	//This is called every time the health is decreased
	public event OnHealthChanged EventOnHealthDecreased;
	//This is called when the health reaches 0
	public event OnHealthChanged EventOnDeath;

	protected void Start()
	{
		currentHealth = maxHealth;
		EventOnDeath += OnDeath;
		EventOnHealthDecreased += OnHealthDecreased;
	}

	public virtual bool DecreaseHealth(int Amount)
	{
		if (!canTakeDamage) return false;

		canTakeDamage = false;
		if (currentHealth == 0)
		{
			EventOnDeath();
			return false;
		}
		currentHealth -= Mathf.Abs(Amount);
		EventOnHealthDecreased();

		Invoke("CanTakeDamageReset", 1f);


		return true;
	}

	void CanTakeDamageReset()
	{
		canTakeDamage = true;
	}

	public virtual void IncreaseHealth(int Amount)
	{
		currentHealth += Mathf.Abs(Amount);
	}

	protected virtual void OnDeath()
	{
		Debug.Log("Oh noes, " + gameObject.name + " died...");
	}

	private void OnHealthDecreased()
	{
		Debug.Log("Current Health of " + gameObject.name + ": " + currentHealth);
	}
}
