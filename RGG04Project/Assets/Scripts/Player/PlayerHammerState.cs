using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammerState : MonoBehaviour
{
	[SerializeField]
	BoxCollider2D hammerCollision;

	bool hammerState = false;

	private void Start()
	{
		PlayerStateHandler myHandler = gameObject.GetComponentInParent<PlayerStateHandler>();
		myHandler.OnHammerState += StartHammerState;
		myHandler.OnNormalState += EndHammerState;
		//hammerCollision.enabled = false;
	
	}

	void StartHammerState()
	{
		Debug.Log("Start hammer state in Hammer State");
		hammerState = true;
		hammerCollision.enabled = true;
	}

	void EndHammerState()
	{
		if (hammerState)
		{
			Debug.Log("End hammer state in Hammer State");
			hammerState = false;
			hammerCollision.enabled = false;
		}

	}


}
