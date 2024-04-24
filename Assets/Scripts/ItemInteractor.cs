using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractor : MonoBehaviour
{
    public Inventory inventory;

    //script handles action when player collects item
    private void OnControllerColliderHit(ControllerColliderHit hit){
        InventoryItem item = hit.gameObject.GetComponent<InventoryItem>();

        //sebug.Log("Test hit");

        if(item != null){
            Debug.Log("Controller collider hit");
            inventory.addItem(item);
        }
    }
}
