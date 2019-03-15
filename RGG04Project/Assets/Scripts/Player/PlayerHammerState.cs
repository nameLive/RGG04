﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammerState : MonoBehaviour
{

	Damager damager;

	bool hammerState = false;

	[SerializeField]
	SpriteRenderer hammerSprite;

	PlayerMovement playerMovement;

	float facingLeftXOffset = -1f;
	float facingRightXOffset = 1f;

	Quaternion facingLeftSpriteRotation;
	Quaternion facingRightSpriteRotation;

	private void Start()
	{
		facingLeftXOffset = gameObject.transform.localPosition.x;
		facingRightXOffset = facingLeftXOffset * -1;

		facingLeftSpriteRotation = hammerSprite.gameObject.transform.localRotation;
		facingRightSpriteRotation = Quaternion.Inverse(facingLeftSpriteRotation);

		PlayerStateHandler myHandler = gameObject.GetComponentInParent<PlayerStateHandler>();
		myHandler.OnHammerState += StartHammerState;
		myHandler.OnNormalState += EndHammerState;

		damager = GetComponent<Damager>();
		damager.canDealDamage = false;

		playerMovement = GetComponentInParent<PlayerMovement>();

	}

	private void Update()
	{
		if (hammerState)
		{
			float targetXOffset = playerMovement.facingRight ? facingRightXOffset : facingLeftXOffset;

			if (!Mathf.Approximately(targetXOffset, gameObject.transform.position.x))
			{
				Vector3 targetPosition = new Vector3(targetXOffset, 0f, 0f);
				gameObject.transform.localPosition = targetPosition;

				Quaternion targetRotation = playerMovement.facingRight ? facingRightSpriteRotation : facingLeftSpriteRotation;


				hammerSprite.gameObject.transform.localRotation = targetRotation;
			}
		}
	}

	void StartHammerState()
	{
		if (!hammerState)
		{
			Debug.Log("Start hammer state");
			hammerState = true;
			damager.canDealDamage = true;
			damager.ActivateCollider();
			hammerSprite.enabled = true;
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
			hammerSprite.enabled = false;
		}

	}


}
