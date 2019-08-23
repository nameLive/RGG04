using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEnemy : MonoBehaviour
{

	PatrolingPoliceStateEnum currentState = PatrolingPoliceStateEnum.ChasePlayer;
	PatrolingPoliceStateEnum previousState = PatrolingPoliceStateEnum.ChasePlayer;
	
	public PatrolingPoliceStateEnum state
	{
		get { return currentState; }
	}

	//the location of this object is the location the enemy is moving towards
	GameObject currentTargetGO = null;
	GameObject playerTargetGO = null;
	Vector3 newTargetPosition;

	GameManager gameManager;
	Animator anim;

	[HideInInspector]
	public bool movingRight = true;

	

	[SerializeField]
	[Tooltip("The speed of which the enemy moves towards the player when in range")]
	[Range(0.0f, 0.5f)]
	public static float movementSpeed = 0.045f;

	[SerializeField]
	[Tooltip("The enemy will always keep this distance to the player no matter how fast the player moves")]
	float maxDistanceToPlayer = 25f;

	[SerializeField]
	float minDistanceToPlayer = 5f;


	private void Start()
	{
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		anim = GetComponentInChildren<Animator>();

		playerTargetGO = GameObject.Find("PlayerCharacter/EnemyTargetLocation");

		if (currentTargetGO == null)
		{
			currentTargetGO = playerTargetGO;

		}

		EnemyStunState stunState = GetComponentInChildren<EnemyStunState>();
		stunState.EventOnBeginStun += SetStateStunned;
		stunState.EventOnEndStun += ResetState;
	}

	private void Update()
	{
		//Checks if the movement should be updated
		if (gameManager.gameState == GameState.InPauseMenu || gameManager.gameState == GameState.WonGame || gameManager.gameState == GameState.LostGame) return;
		if (currentState == PatrolingPoliceStateEnum.Stunned || currentState == PatrolingPoliceStateEnum.None) return;

		//The Vector pointing to the target
		Vector3 directionToTarget = transform.position - currentTargetGO.transform.position;

		//To prevent enemies from stopping at the same spot(inside of each other) when going to a patrol point
		if (directionToTarget.magnitude <= 1f) return;

		//Update target location
		if (currentState == PatrolingPoliceStateEnum.ChasePlayer && directionToTarget.magnitude > maxDistanceToPlayer)
		{
			//calculates the point that is pointing towards this enemy exactly maxDistanceToPlayer-units away from the player
			newTargetPosition = Vector3.Normalize(directionToTarget);
			newTargetPosition *= maxDistanceToPlayer;
			newTargetPosition += currentTargetGO.transform.position;
		}
		else
		{		
			newTargetPosition = Vector3.MoveTowards(transform.position, currentTargetGO.transform.position, movementSpeed);			
		}
		
		//Update position
		transform.position = newTargetPosition;

		//Calculate the movement direction
		float dot = Vector3.Dot(directionToTarget, transform.right);
		movingRight = dot < 0 ? true : false;

		anim.SetBool("FacingRight", movingRight);

	}

	private void SetState(PatrolingPoliceStateEnum NewState)
	{
		if (currentState == NewState) return;

		previousState = currentState;
		currentState = NewState;

		//Debug.Log("New State: " + currentState + " Previous State: " + previousState);
	}
	private void ResetState()
	{
		SetState(previousState);
	}

	public void SetStateChasePlayer()
	{
		SetState(PatrolingPoliceStateEnum.ChasePlayer);
		currentTargetGO = playerTargetGO;
	}

	public void SetStatePatroling(GameObject TargetLocationObject)
	{
		SetState(PatrolingPoliceStateEnum.Patroling);
		currentTargetGO = TargetLocationObject;
	}

	public void SetStateStunned()
	{
		SetState(PatrolingPoliceStateEnum.Stunned);
	}

	public void SetStateNoneForDuration(float Duration)
	{
		SetState(PatrolingPoliceStateEnum.None);
		Invoke("ResetState", Duration);
	}
	   
}