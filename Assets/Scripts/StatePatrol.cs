using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : IState
{
    EnemyController owner;
    NavMeshAgent agent;
    EnemyWaypoint waypoint;
    
    public StatePatrol(EnemyController owner) { this.owner = owner; }

    public void Enter(){
        Debug.Log("Entering patrol state");

        waypoint = owner.waypoint;
        agent = owner.GetComponent<NavMeshAgent>();
        agent.destination = waypoint.transform.position;

        //start moving, incase agent was stopped
        agent.isStopped = false;

    }

    public void Execute(){
        //Debug.Log("updating patrol state");

        if(!agent.pathPending && agent.remainingDistance < 0.5f){
            EnemyWaypoint nextWaypoint = waypoint.nextWaypoint;
            waypoint = nextWaypoint;
            agent.destination = waypoint.transform.position;
        }

        if(owner.seenTarget){
            owner.stateMachine.ChangeState(new StateAttack(owner));
        }
    }

    public void Exit(){
        Debug.Log("exiting patrol state");
        //stop moving
        agent.isStopped = true;
    }
}
