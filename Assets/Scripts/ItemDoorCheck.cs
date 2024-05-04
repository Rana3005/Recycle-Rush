using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ItemDoorCheck : MonoBehaviour
{
    public GameObject checkObject;
    public Door linkedDoor; 

    private void OnTriggerEnter(Collider hitObject){
        if(hitObject.CompareTag(checkObject.tag)){
            linkedDoor.DoorInteract();
        }
    }

    private void OnTriggerExit(Collider hitObject){
        if(hitObject.CompareTag(checkObject.tag)){
            linkedDoor.DoorInteract();
        }
    }
}
