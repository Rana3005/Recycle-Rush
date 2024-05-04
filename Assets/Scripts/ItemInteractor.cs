using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemInteractor : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemPosition;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            inventory.ChangeItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            inventory.ChangeItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            inventory.ChangeItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)){
            inventory.ChangeItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)){
            inventory.ChangeItem(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6)){
            inventory.ChangeItem(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7)){
            inventory.ChangeItem(7);
        }
    }

    //script handles action when player collects item
    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(inventory.getItemListCount() < 7){
            InventoryItem item = hit.gameObject.GetComponent<InventoryItem>();
            ItemAnimation itemAnimation  = hit.gameObject.GetComponent<ItemAnimation>();
            
            if(item != null){
                //turn off animation
                itemAnimation.enabled = false;

                //Make object a child of itemPosition so it's in HUDs
                //hit.transform.SetParent(itemPosition.transform);
                hit.transform.parent = itemPosition.transform;
                ChangeItemPositionAndRotation(hit.transform, item.scriptableObjectItem.item_type);

                inventory.AddItem(item);
            }
        }
    }

    //change position and rotaion of Item
    private void ChangeItemPositionAndRotation(Transform inventoryItem, ItemType item_type){
        switch (item_type){
            case ItemType.LiftTech:
                inventoryItem.localPosition = new Vector3(0,0,0);
                inventoryItem.localRotation = Quaternion.Euler(11, -68, 36);
                break;
            case ItemType.Key:
                inventoryItem.localPosition = new Vector3(-0.05f, 0.4f, 0.05f);
                inventoryItem.localRotation = Quaternion.Euler(0.1f, -75, 150);
                break;
            case ItemType.GunEMP:
                inventoryItem.localPosition = new Vector3(-0.06f,-0.06f,-0.09f);
                inventoryItem.localRotation = Quaternion.Euler(-4.25f,14.25f,0);
                break;
            default:
                Debug.Log("Unspported Item");
                break;
        }
    }
}
