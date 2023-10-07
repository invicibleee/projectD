using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyConroller : MonoBehaviour
{
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private State currentState;



    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
            default:
                break;
        }
    }


    //-----------WALKING STATE--------------------------------------------

    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState() 
    { 
    
    }

    private void ExitWalkingState()
    {

    }

    //-----------KNOCKBACK------------------------------------------------

    private void EnterKnockbackState()
    {

    }

    private void UpdateKnockbackState()
    {

    }

    private void ExitKnockbackState()
    {

    }

    //------------DEAD STATE-------------------------------------------------
    private void EnterDeadState()
    {

    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //------------OTHER FUNCTIONS---------------------------------------------

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
            default:
                break;
        }
        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
            default:
                break;
        }
        currentState = state;
    }
}
