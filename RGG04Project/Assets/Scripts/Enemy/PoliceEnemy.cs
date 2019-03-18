﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEnemy : MonoBehaviour
{

    //PRIVATE VARIABLES

    PatrolingPoliceStateEnum currentState = PatrolingPoliceStateEnum.ChasePlayer;
    PatrolingPoliceStateEnum previousState = PatrolingPoliceStateEnum.ChasePlayer;

    //the location of this object is the location the enemy is headed towards
    GameObject targetLocationObject = null;
    Vector3 targetLocation;

    GameManager gameManager;

    //EDITABLE VARIABLES

    [SerializeField]
    [Tooltip("The speed of which the enemy moves towards the player when in range")]
    [Range(0.0f, 0.5f)]
    float movementSpeed = 0.03f;

    [SerializeField]
    [Tooltip("The enemy will always keep this distance to the player no matter how fast the player moves")]
    float maxDistanceToPlayer = 20f;

    [SerializeField]
    float minDistanceToPlayer = 5f;

    //FUNCTIONS

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

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
        //if (gameManager.gameState != GameState.InPauseMenu && gameManager.gameState != GameState.WonGame) return;

        if (currentState != PatrolingPoliceStateEnum.Stunned)
        {
            UpdateTargetLocation();
            MoveToTargetLocation();

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
        previousState = currentState;
        currentState = NewState;
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


}
