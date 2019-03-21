using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammerState : MonoBehaviour
{

	Damager damager;

    public bool hasHammer;

	PlayerMovement playerMovement;

	float facingLeftXOffset = -1f;
	float facingRightXOffset = 1f;

	private void Start()
	{
		facingLeftXOffset = gameObject.transform.localPosition.x;
		facingRightXOffset = facingLeftXOffset * -1;

		PlayerStateHandler myHandler = gameObject.GetComponentInParent<PlayerStateHandler>();
		myHandler.OnHammerState += StartHammerState;
		myHandler.OnNormalState += EndHammerState;

		damager = GetComponent<Damager>();
		damager.canDealDamage = false;

		playerMovement = GetComponentInParent<PlayerMovement>();

        if (hasHammer)
        {
            StartHammerState();
        }

	}

	private void Update()
	{
		if (hasHammer)
		{
			float targetXOffset = playerMovement.facingRight ? facingRightXOffset : facingLeftXOffset;

			if (!Mathf.Approximately(targetXOffset, gameObject.transform.position.x))
			{
				Vector3 targetPosition = new Vector3(targetXOffset, 0f, 0f);
				gameObject.transform.localPosition = targetPosition;
			}
		}
	}

	void StartHammerState()
	{
		if (!hasHammer)
		{
            Debug.Log("Start Hammer state");
			damager.canDealDamage = true;
            hasHammer = true; 
		}
	}

	void EndHammerState()
	{
		if (hasHammer)
		{
            Debug.Log("End hammer state");
			damager.canDealDamage = false;
            hasHammer = false;
		}

	}


}
