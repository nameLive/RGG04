using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEnemy : MonoBehaviour
{

	private PoliceEnemyStateEnum currentState = PoliceEnemyStateEnum.ChasePlayer;
	private PoliceEnemyStateEnum previousState = PoliceEnemyStateEnum.ChasePlayer;
	public PoliceEnemyStateEnum State { get { return currentState; } }

	//Defaults to the player
	GameObject chaseTarget = null;
	Vector3 targetLocation;

	private GameObject playerChaseTarget = null;

	private GameManager gameManager = null;
	private Animator anim = null;

	[HideInInspector]
	public bool movingRight = true;

	[SerializeField]
	[Tooltip("The speed of which the enemy moves towards the player when in range")]
	[Range(0.0f, 0.5f)]
	public static float movementSpeed = 0.045f;

	//[SerializeField] Can be made visible again if it needs to be changed
	[Tooltip("The enemy will always keep this distance to the player no matter how fast the player moves")]
	float maxDistanceToPlayer = 25f;

	//[SerializeField]
	float minDistanceToPlayer = 5f;

	//FUNCTIONS

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		anim = GetComponentInChildren<Animator>();

		playerChaseTarget = GameObject.Find("PlayerCharacter/EnemyTargetLocation");

		if (chaseTarget == null)
		{
			chaseTarget = playerChaseTarget;
		}

		EnemyStunState stunState = GetComponentInChildren<EnemyStunState>();
		stunState.EventOnBeginStun += SetStateStunned;
		stunState.EventOnEndStun += EndStateStunned;
	}

	private void Update()
	{

		//Game Manager stuff
		if (gameManager.gameState == GameState.InPauseMenu || gameManager.gameState == GameState.WonGame || gameManager.gameState == GameState.LostGame)
			return;

		else if (currentState == PoliceEnemyStateEnum.Stunned || currentState == PoliceEnemyStateEnum.None)
			return;


		movingRight = isMovingRight();

		anim.SetBool("FacingRight", movingRight);

		Vector3 newPosition = Vector3.MoveTowards(transform.position, chaseTarget.transform.position, movementSpeed);

		if (currentState == PoliceEnemyStateEnum.ChasePlayer)
		{



			//funkar ej än

			float distanceToPlayer = Vector3.Distance(transform.position, chaseTarget.transform.position);
			bool isWithinPlayerRange = distanceToPlayer < maxDistanceToPlayer;


			Debug.Log($"Is within Player Range: {isWithinPlayerRange}");
			Debug.Log($"Enemy State: {currentState}");

			if (!isWithinPlayerRange)
			{

				Debug.Log($"Distance to player:{distanceToPlayer}");

				Vector3 directionToTarget = newPosition - chaseTarget.transform.position;
				directionToTarget = Vector3.Normalize(directionToTarget);
				directionToTarget *= maxDistanceToPlayer;
				newPosition = directionToTarget + chaseTarget.transform.position;

			}


			/*
						Vector3 directionToTarget = transform.position - chaseTarget.transform.position;
						Vector3 newTargetLocation = Vector3.Normalize(directionToTarget);

						newTargetLocation *= maxDistanceToPlayer;

						newTargetLocation += chaseTarget.transform.position;*/






			//newTargetLocation *= maxDistanceToPlayer;

			//newTargetLocation += chaseTarget.transform.position;



			/*	if (currentState == PoliceEnemyStateEnum.ChasePlayer)
				{
					ChasePlayerUpdate();
				}*/

		}



		transform.position = newPosition;
	}

	void ChasePlayerUpdate()
	{
		if (!IsWithinPlayerMaxRange())
		{
			transform.position = MaxRangeTargetLocation();
		}
	}

	public void SetTargetLocationObject(GameObject NewTargetLocationObject)
	{
		chaseTarget = NewTargetLocationObject;
	}

	void SetTargetLocationPlayer()
	{
		SetTargetLocationObject(GameObject.Find("EnemyTargetLocation"));
	}

	//Intended for internal use only
	private void SetState(PoliceEnemyStateEnum NewState)
	{
		if (currentState == NewState) return;

		previousState = currentState;
		currentState = NewState;

		//Debug.Log("New State: " + currentState + " Previous State: " + previousState);
	}

	public void SetStateChasePlayer()
	{
		SetState(PoliceEnemyStateEnum.ChasePlayer);
		SetTargetLocationPlayer();
	}

	public void SetStatePatroling(GameObject TargetLocationObject)
	{
		SetState(PoliceEnemyStateEnum.Patroling);
		SetTargetLocationObject(TargetLocationObject);
	}

	public void SetStateStunned()
	{
		SetState(PoliceEnemyStateEnum.Stunned);
	}

	void EndStateStunned()
	{
		SetState(previousState);
	}

	public void SetStateNoneForDuration(float Duration)
	{
		SetState(PoliceEnemyStateEnum.None);
		Invoke("ResetState", Duration);
	}

	private void ResetState()
	{
		SetState(previousState);
	}


	bool ArrivedAtTargetLocation()
	{
		float distanceToTargetLocation = Vector3.Distance(chaseTarget.transform.position, transform.position);

		if (distanceToTargetLocation <= 1f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool IsWithinPlayerMaxRange()
	{
		float distanceToPlayer = Vector3.Distance(gameObject.transform.position, chaseTarget.transform.position);

		if (distanceToPlayer <= maxDistanceToPlayer) return true;
		else return false;
	}

	bool IsWithinPlayerMinRange()
	{
		float distanceToPlayer = Vector3.Distance(gameObject.transform.position, chaseTarget.transform.position);

		if (distanceToPlayer <= minDistanceToPlayer) return true;
		else return false;
	}

	//calculates the point that is positioned between target location and this enemy, exactly maxDistanceToPlayer-units away from the player
	//Keeps enemy within visual range
	Vector3 MaxRangeTargetLocation()
	{
		Vector3 directionToTarget = transform.position - chaseTarget.transform.position;
		Vector3 newTargetLocation = Vector3.Normalize(directionToTarget);

		newTargetLocation *= maxDistanceToPlayer;

		newTargetLocation += chaseTarget.transform.position;

		return newTargetLocation;
	}

	bool isMovingRight()
	{
		Vector3 movementDirection = transform.position - chaseTarget.transform.position;

		float dot = Vector3.Dot(movementDirection, transform.right);

		return dot < 0 ? true : false;
	}


}
