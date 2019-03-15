using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//There should ever be only one instance of this script, and that is on the player.
public class PlayerStateHandler : MonoBehaviour
{
    public delegate void OnPlayerStateChanged();
	public event OnPlayerStateChanged OnHammerState;
    public event OnPlayerStateChanged OnNormalState;

    PlayerState currentPlayerState = PlayerState.Normal;


    private void Start()
    {
        OnHammerState += HammerStatePlaceHolder;
        OnNormalState += NormalStatePlaceHolder;
    }


    //public event called from other scripts(e.g collectibles) to try to change the state. Returns true if state was successfully changed.
    public bool SetNewState(PlayerState NewState)
    {
        ChangeState(NewState);

        return true;
    }

    public void SetHammerState(float Duration)
    {
        ChangeState(PlayerState.Hammer);
        Invoke("DisableHammerState", Duration);
    }

    //Internal event that actually changes the state
    void ChangeState(PlayerState NewState)
    {
        currentPlayerState = NewState;

        Debug.Log("New State: " + NewState);

        if (NewState == PlayerState.Hammer)
        {
            OnHammerState();
        }
        else if (NewState == PlayerState.Normal)
        {
            OnNormalState();
        }
    }


    void DisableHammerState()
    {
        ChangeState(PlayerState.Normal);
    }




    //This is just a placeholder function and doesn't do anything. Will be removed later.
    void NormalStatePlaceHolder()
    {
		//Debug.Log("Normal state in Handler");
    }


    //This is just a placeholder function and doesn't do anything. Will be removed later.
    void HammerStatePlaceHolder()
    {
		//Debug.Log("Hammer State in Handler");
    }
}
