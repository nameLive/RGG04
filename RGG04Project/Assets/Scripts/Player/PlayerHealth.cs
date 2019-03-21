using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBase
{

	public bool invincible = false;
    public bool isDead = false;

	void Start() {

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

	public override bool DecreaseHealth(int Amount)
	{
		if (!invincible)
		{
			bool tookDamage = base.DecreaseHealth(Amount);
            return tookDamage;
        }
        else
        {
            return false;
        }
	}

	public override void IncreaseHealth(int Amount)
	{
		if (!invincible)
		{
			base.DecreaseHealth(Amount);
		}
	}

	//This function is bound to the delegate "EventOnDeath" so it is automatically called when the player health reaches 0
	protected override void OnDeath()
	{
		base.OnDeath();
		isDead = true;
	}


}
