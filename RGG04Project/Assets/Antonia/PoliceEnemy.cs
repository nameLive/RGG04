using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEnemy : MonoBehaviour
{

	[SerializeField]
	GameObject player;

	[SerializeField]
	[Tooltip("The enemy will always keep this distance to the player no matter how fast the player moves")]
	float maxDistanceToPlayer = 20f;

	[SerializeField]
	[Tooltip("The speed of which the enemy moves towards the player when in range")]
	[Range(0.0f, 0.5f)]
	float movementSpeed = 0.03f;

	float distanceToPlayer = 0f;

	Vector3 directionToPlayer = new Vector3();
	Vector3 targetPosition = new Vector3();
	Vector3 startPosition = new Vector3();

	// Start is called before the first frame update
	void Start()
	{
		CalculateTargetPosition();
	}

	// Update is called once per frame
	void Update()
	{
		CalculateTargetPosition();
		MoveTowardsPlayer();
	}

	void CalculateTargetPosition()
	{

		startPosition = transform.position;
		targetPosition = player.transform.position;

	}


	void MoveTowardsPlayer()
	{
		if (HitThresholdDistanceToPlayer())
		{
			return;
		}

		if (IsWithinPlayerRange())
		{
			Vector3 NewPosition = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed);
			transform.position = NewPosition;
		}
		else
		{
			//Calculates the exact point of maxDistanceToPlayer(20) units from the player pointing towards the enemy
			Vector3 PlayerToEnemyPos = transform.position - player.transform.position;
			PlayerToEnemyPos = PlayerToEnemyPos / PlayerToEnemyPos.magnitude;
			PlayerToEnemyPos *= maxDistanceToPlayer;
			Vector3 EndPos = player.transform.position + PlayerToEnemyPos;

			transform.position = EndPos;
		}
	

	}

	bool IsWithinPlayerRange()
	{
		directionToPlayer = player.transform.position - transform.position;
		distanceToPlayer = directionToPlayer.magnitude;

		if (distanceToPlayer <= maxDistanceToPlayer)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool HitThresholdDistanceToPlayer()
	{
		directionToPlayer = player.transform.position - transform.position;
		distanceToPlayer = directionToPlayer.magnitude;

		if (distanceToPlayer >= 1f)
		{
			return false;
		}
		else
		{
			return true;
		}

	}
}
