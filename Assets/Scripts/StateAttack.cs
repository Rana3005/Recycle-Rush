using UnityEngine;
using UnityEngine.AI;

public class StateAttack : IState
{
    EnemyController owner;
    NavMeshAgent agent;

    public StateAttack(EnemyController owner) { this.owner = owner; }

    public void Enter(){
        Debug.Log("entering attack state");

        agent = owner.GetComponent<NavMeshAgent>();

        if(owner.seenTarget){
            agent.destination = owner.lastSeenPosition;
            agent.isStopped = false;
        }
    }

    public void Execute(){
        Debug.Log("updating attack state");

        agent.destination = owner.lastSeenPosition;
        agent.isStopped = false;

        if (!agent.pathPending && agent.remainingDistance < 5.0f){
            agent.isStopped = true;
        }

        if (owner.seenTarget != true){
            Debug.Log("lost sight");
            //search for player
            owner.stateMachine.ChangeState(new StatePatrol(owner));
        }

        //fire on player
    }

    public void Exit(){
        Debug.Log("exiting attack state");
        agent.isStopped = true;
    }
}
