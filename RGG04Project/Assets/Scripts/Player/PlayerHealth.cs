using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBase
{

	public bool invincible = false;

	void Start()
	{
		base.Start();
		PlayerStateHandler myHandler = gameObject.GetComponentInParent<PlayerStateHandler>();
		myHandler.OnHammerState += SetInvincible;
		myHandler.OnNormalState += SetInvincibleFalse;
	}

	void SetInvincible()
	{
		invincible = true;
	}

	void SetInvincibleFalse()
	{
		invincible = false;
	}

	public override void DecreaseHealth(int Amount)
	{
		if (!invincible)
		{
			base.DecreaseHealth(Amount);
			//Debug.Log("Player Current Health: " + currentHealth);
		}
	}

	public override void IncreaseHealth(int Amount)
	{
		if (!invincible)
		{
			base.DecreaseHealth(Amount);
			//Debug.Log("Player Current Health: " + currentHealth);
		}
	}


}
