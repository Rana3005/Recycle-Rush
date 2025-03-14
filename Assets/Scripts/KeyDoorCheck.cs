using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorCheck : MonoBehaviour
{
    private bool inRange = false;
    public Inventory inventory;
    public InventoryItem key;
    public Door linkedDoor;
    void Start(){
            inventory.ItemUsed += InventoryItemUsed;
    }

    private void OnTriggerEnter(Collider hitObject){       
        if (hitObject.CompareTag("Player"))
            Debug.Log("player enter");
            inRange = true;
    }

    private void OnTriggerExit(Collider other){
        inRange = false;
    }

    void InventoryItemUsed(InventoryItem item){
        if(linkedDoor.getIsOpen()){
            return;
        }

        if (item.itemName == key.itemName){
            if(inRange){
                linkedDoor.DoorInteract();
                inventory.RemoveItem(item);
            }
        }
    }
}
