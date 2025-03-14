using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    public EnemyWaypoint waypoint;
    public GameObject target;
    public bool seenTarget = false;
    public Vector3 lastSeenPosition;
    private float sightFov = 110;

    public StateMachine stateMachine = new StateMachine();

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoint.transform.position;

        stateMachine.ChangeState(new StatePatrol(this));
    }

    void Update(){
        if(!agent.pathPending && agent.remainingDistance < 1f){
            EnemyWaypoint nextWaypoint = waypoint.nextWaypoint;
            waypoint = nextWaypoint;
            agent.destination = waypoint.transform.position;
        }

        stateMachine.Update();
    }

    private void OnTriggerStay(Collider other){
        //check if player
        
        if(other.gameObject == target){
            //angle between enemy and the player
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            //reset if player is seen
            seenTarget = false;

            RaycastHit hit;

            if(angle < sightFov * 0.5f){
                //if raycast hits player, path is clear
                //transform.up raises up from the floor by 1 unit
                if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, GetComponent<SphereCollider>().radius)){
                    if(hit.transform.root.CompareTag(target.tag)){
                        Debug.Log("player found");
                        //flag that player has been seen - remember their position
                        seenTarget = true;
                        lastSeenPosition = target.transform.position;
                    }
                }
            }
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        
        if (GetComponent<SphereCollider>() != null){
            Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
            
            if(seenTarget){
                Gizmos.DrawLine(transform.position, lastSeenPosition);
            }

            if (lastSeenPosition != Vector3.zero){
                Gizmos.DrawSphere(lastSeenPosition, 0.2f);
            }

            Vector3 rightPeripheral;
            Vector3 leftPeripheral;
            rightPeripheral = (Quaternion.AngleAxis(sightFov * 0.5f, Vector3.up) * transform.forward * GetComponent<SphereCollider>().radius) + transform.position;
            leftPeripheral = (Quaternion.AngleAxis(-sightFov * 0.5f, Vector3.up) * transform.forward * GetComponent<SphereCollider>().radius) + transform.position;
            
            Gizmos.DrawLine(transform.position, rightPeripheral);
            Gizmos.DrawLine(transform.position, leftPeripheral);
        }
    }
}

