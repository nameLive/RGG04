using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBobbingMovement : BobbingMovement
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyStunState enemyStunState = GetComponent<EnemyStunState>();
        enemyStunState.EventOnBeginStun += BeginStun;
        enemyStunState.EventOnEndStun += EndStun;
    }

    void BeginStun()
    {
        shouldBob = false;
    }

    void EndStun()
    {
        shouldBob = true;
    }

}
