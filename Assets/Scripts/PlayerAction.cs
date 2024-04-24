using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public LayerMask layerMask;
    private GameObject heldObject;
    [SerializeField] private Transform holdPosition;
    bool fire = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            if(heldObject == null){
                RaycastHit hit;
                //selects object and turns off gravity of object
                if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f, layerMask)){
                    heldObject = hit.collider.gameObject;
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                }
            } 
            Debug.Log("fire1");           
        }

        if(Input.GetButtonUp("Fire1")){
            //Drop object
            if(heldObject != null){
                heldObject.GetComponent<Rigidbody>().useGravity = true;

                heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                heldObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                heldObject.GetComponent<Rigidbody>().ResetInertiaTensor();

                heldObject = null;
            }
        }

        if(Input.GetButtonUp("Fire2")){
            if (heldObject != null){
                fire = true;
            }
            Debug.Log("fire2"); 
        }
    }

    void FixedUpdate(){
        if (heldObject != null){
            //moving object being held based on hold position
            heldObject.GetComponent<Rigidbody>().MoveRotation(holdPosition.rotation);
            Vector3 difference = holdPosition.position - heldObject.transform.position;
            heldObject.GetComponent<Rigidbody>().AddForce(difference * 500.0f);
            heldObject.GetComponent<Rigidbody>().velocity *= 0.1f;

            if(fire){
                fire = false;
                heldObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10.0f, ForceMode.Impulse);
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject = null;
            }
        }
    }
}
