using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammerState : MonoBehaviour
{

	Damager damager;

	bool hammerState = false;

	private void Start()
	{
		PlayerStateHandler myHandler = gameObject.GetComponentInParent<PlayerStateHandler>();
		myHandler.OnHammerState += StartHammerState;
		myHandler.OnNormalState += EndHammerState;
		damager = GetComponent<Damager>();
		damager.canDealDamage = false;

	}

	void StartHammerState()
	{
		if (!hammerState)
		{
			Debug.Log("Start hammer state");
			hammerState = true;
			damager.canDealDamage = true;
			damager.ActivateCollider();
		}
	}

	void EndHammerState()
	{
		if (hammerState)
		{
			Debug.Log("End Hammer state");
			hammerState = false;
			damager.canDealDamage = false;
			damager.DeactivateCollider();
		}

	}


}
