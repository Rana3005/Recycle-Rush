using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorTarget : MonoBehaviour
{
    public Door linkedDoor;

    public void DoorSlide(){
        linkedDoor.DoorInteract();
    }
}
