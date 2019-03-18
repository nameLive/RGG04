using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEnemy : MonoBehaviour
{

    GameManager gameManager;


	public bool isStunned = false;

	//[SerializeField]
	GameObject targetPlayerLocation;

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

	EnemyStunState stunState;

	// Start is called before the first frame update
	void Start()
	{

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        targetPlayerLocation = GameObject.Find("PlayerFeet");

		CalculateTargetPosition();
		stunState = GetComponentInChildren<EnemyStunState>();
		stunState.EventOnBeginStun += StartStunState;
		stunState.EventOnEndStun += EndStunState;
	}

	// Update is called once per frame
	void Update()
	{
        if (gameManager.gameState != GameState.InPauseMenu && gameManager.gameState != GameState.WonGame) {

            if (isStunned) return;

            CalculateTargetPosition();
            MoveTowardsPlayer();
        }
	}

	void StartStunState()
	{
		isStunned = true;
	}

	void EndStunState()
	{
		isStunned = false;
	}

	void CalculateTargetPosition()
	{

		startPosition = transform.position;
		targetPosition = targetPlayerLocation.transform.position;

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
			Vector3 PlayerToEnemyPos = transform.position - targetPlayerLocation.transform.position;
			PlayerToEnemyPos = PlayerToEnemyPos / PlayerToEnemyPos.magnitude;
			PlayerToEnemyPos *= maxDistanceToPlayer;
			Vector3 EndPos = targetPlayerLocation.transform.position + PlayerToEnemyPos;

			transform.position = EndPos;
		}
	

	}

	bool IsWithinPlayerRange()
	{
		directionToPlayer = targetPlayerLocation.transform.position - transform.position;
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
		directionToPlayer = targetPlayerLocation.transform.position - transform.position;
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
