using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState{
    void Enter();
    void Execute();
    void Exit();
}

public class StateMachine
{
    IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null){
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    // Update is called once per frame
    public void Update()
    {
        if(currentState != null) currentState.Execute();
    }
}

