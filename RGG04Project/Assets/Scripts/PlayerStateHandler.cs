using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//There should ever be only one instance of this script, and that is on the player.
public class PlayerStateHandler : MonoBehaviour
{
    public delegate void OnPlayerStateChanged();
    OnPlayerStateChanged OnHammerState;
    OnPlayerStateChanged OnNormalState;

    PlayerState currentPlayerState = PlayerState.Normal;

    //public event called from other scripts(e.g collectibles) to try to change the state. Returns true if state was successfully changed.
    public bool SetNewState(PlayerState NewState)
    {
        ChangeState(NewState);

        return true;
    }

    //Internal event that actually changes the state
    void ChangeState(PlayerState NewState)
    {
        currentPlayerState = NewState;

        if (NewState == PlayerState.Hammering)
        {
            OnHammerState();
        }
        else if (NewState == PlayerState.Normal)
        {
            OnNormalState();
        }
    }
}
