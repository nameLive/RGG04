using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEnemy : MonoBehaviour
{

    //PRIVATE VARIABLES

    PatrolingPoliceStateEnum currentState = PatrolingPoliceStateEnum.ChasePlayer;
    PatrolingPoliceStateEnum previousState = PatrolingPoliceStateEnum.ChasePlayer;


	public PatrolingPoliceStateEnum state
	{
		get { return currentState; }
	}

    //the location of this object is the location the enemy is headed towards
    GameObject targetLocationObject = null;
    Vector3 targetLocation;

    GameManager gameManager;
    Animator anim;

	[HideInInspector]
	public bool movingRight = true;

    //EDITABLE VARIABLES

    [SerializeField]
    [Tooltip("The speed of which the enemy moves towards the player when in range")]
    [Range(0.0f, 0.5f)]
    public static float movementSpeed = 0.045f;

    [SerializeField]
    [Tooltip("The enemy will always keep this distance to the player no matter how fast the player moves")]
    float maxDistanceToPlayer = 20f;

    [SerializeField]
    float minDistanceToPlayer = 5f;

    //FUNCTIONS

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponentInChildren<Animator>();

        if (targetLocationObject == null)
        {
            SetTargetLocationPlayer();
        }

        EnemyStunState stunState = GetComponentInChildren<EnemyStunState>();
        stunState.EventOnBeginStun += SetStateStunned;
        stunState.EventOnEndStun += EndStateStunned;
    }

    private void Update()
    {
        anim.SetBool("FacingRight", movingRight);
        if (gameManager.gameState == GameState.InPauseMenu || gameManager.gameState == GameState.WonGame) return;

        if (currentState != PatrolingPoliceStateEnum.Stunned && currentState != PatrolingPoliceStateEnum.None)
        {
            UpdateTargetLocation();
            MoveToTargetLocation();

			movingRight = isMovingRight();

            if (currentState == PatrolingPoliceStateEnum.ChasePlayer)
            {
                ChasePlayerUpdate();
            }
        }
    }

    void UpdateTargetLocation()
    {
        targetLocation = targetLocationObject.transform.position;
    }

    void ChasePlayerUpdate()
    {
        if (!IsWithinPlayerMaxRange())
        {
            transform.position = MaxRangeTargetLocation();
        }
    }

    void MoveToTargetLocation()
    {
        if (!ArrivedAtTargetLocation())
        {
            Vector3 NewPosition = Vector3.MoveTowards(transform.position, targetLocation, movementSpeed);
            transform.position = NewPosition;
        }
    }

    public void SetTargetLocationObject(GameObject NewTargetLocationObject)
    {
        targetLocationObject = NewTargetLocationObject;
    }

    void SetTargetLocationPlayer()
    {
        SetTargetLocationObject(GameObject.Find("EnemyTargetLocation"));
    }

    //Intended for internal use only
    private void SetState(PatrolingPoliceStateEnum NewState)
    {
		if (currentState == NewState) return;

        previousState = currentState;
        currentState = NewState;

		//Debug.Log("New State: " + currentState + " Previous State: " + previousState);
    }

    public void SetStateChasePlayer()
    {
        SetState(PatrolingPoliceStateEnum.ChasePlayer);
        SetTargetLocationPlayer();
    }

    public void SetStatePatroling(GameObject TargetLocationObject)
    {
        SetState(PatrolingPoliceStateEnum.Patroling);
        SetTargetLocationObject(TargetLocationObject);
    }

    public void SetStateStunned()
    {
        SetState(PatrolingPoliceStateEnum.Stunned);
    }

    void EndStateStunned()
    {
        SetState(previousState);
    }

	public void SetStateNoneForDuration(float Duration)
	{
		SetState(PatrolingPoliceStateEnum.None);
		Invoke("ResetState", Duration);
	}

	private void ResetState()
	{
		SetState(previousState);
	}


    bool ArrivedAtTargetLocation()
    {
        float distanceToTargetLocation = Vector3.Distance(targetLocationObject.transform.position, transform.position);

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
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, targetLocationObject.transform.position);

        if (distanceToPlayer <= maxDistanceToPlayer) return true;
        else return false;
    }

    bool IsWithinPlayerMinRange()
    {
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, targetLocationObject.transform.position);

        if (distanceToPlayer <= minDistanceToPlayer) return true;
        else return false;
    }

    //calculates the point that is positioned between target location and this enemy, exactly maxDistanceToPlayer-units away from the player
    Vector3 MaxRangeTargetLocation()
    {
        Vector3 directionToTarget = transform.position - targetLocationObject.transform.position;
        Vector3 newTargetLocation = Vector3.Normalize(directionToTarget);

        newTargetLocation *= maxDistanceToPlayer;

        newTargetLocation += targetLocationObject.transform.position;

        return newTargetLocation;
    }

	bool isMovingRight()
	{
		Vector3 movementDirection = transform.position - targetLocationObject.transform.position;

		float dot = Vector3.Dot(movementDirection, transform.right);

		return dot < 0 ? true : false;
	}


}
