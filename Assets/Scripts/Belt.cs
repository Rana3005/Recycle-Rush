using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Belt : MonoBehaviour
{
    private static int beltID = 0;
    //next connecting belt
    public Belt nextConveyorBelt;
    //item attached to belt
    public BeltItem beltItem;
    //used to check if belt is taken, for queuing
    public bool isSpaceUsed;
    private BeltManager beltManager;

    void Start(){
        beltManager = FindObjectOfType<BeltManager>();

        nextConveyorBelt = null;
        nextConveyorBelt = CheckNextBelt();
        gameObject.name = $"Belt: {beltID++}";
    }

    void Update(){
        if(beltManager.isBeltOn){
           //finds next belt if next belt is empty
            if(nextConveyorBelt == null){
                nextConveyorBelt = CheckNextBelt();
            }

            /*
            if(beltItem != null && beltItem.item != null && !beltItem.isPickedUp){
                //item is in belt and starts coroutine
                StartCoroutine(StartItemBeltMove());
            } */

            if(beltItem != null && beltItem.item != null){
                //item is in belt and starts coroutine
                StartCoroutine(StartItemBeltMove());
            }
        }
        
    }

    public Vector3 GetItemPos(){
        //method called when moving item to new position
        //gets current postion and adds padding to y axis, so moving object doesn't intersect
        var padding = 0.3f;
        var position = transform.position;      //positon of current gameobject

        return new Vector3(position.x, position.y + padding, position.z);

    }

    public IEnumerator StartItemBeltMove(){

        //Debug.Log("Moving item");
        //visually moves item to another belt
        isSpaceUsed = true;         //set to true as belt currenlty has item
        Rigidbody liftabeItem = beltItem.gameObject.GetComponent<Rigidbody>();

        //check if item and next belt in sequence is not null and space is not taken
        if(beltItem.item != null && nextConveyorBelt != null && nextConveyorBelt.isSpaceUsed == false){
            //get position where to move item
            Vector3 newPosition = nextConveyorBelt.GetItemPos();

            //set the next belt position item as taken
            nextConveyorBelt.isSpaceUsed = true;

            if (liftabeItem != null) liftabeItem.useGravity = false;

            //variable for smooth movement
            var step = beltManager.beltSpeed * Time.deltaTime;

            //loop runs until moving item reaches destination
            while(beltItem.item.transform.position != newPosition){
                beltItem.item.transform.position = Vector3.MoveTowards(beltItem.transform.position, newPosition, step);

                yield return null;
            }

            //sets current belt to none as item being moved
            isSpaceUsed = false;
            //get next belt and assigns current held item
            nextConveyorBelt.beltItem = beltItem;
            beltItem = null;
        }
    }

    private Belt CheckNextBelt(){
        //checks for neighbour belt using raycast
        Transform currentBeltTransform = transform;
        RaycastHit hit;

        var forward = transform.forward;

        Ray ray = new Ray(currentBeltTransform.position, forward);

        if(Physics.Raycast(ray, out hit, 2f)){
            Belt belt = hit.collider.GetComponent<Belt>();

            //returns belt component if there is a belt object;
            if(belt != null){
                return belt;
            }
        }

        return null;        //if no belt found return null
    }

}
