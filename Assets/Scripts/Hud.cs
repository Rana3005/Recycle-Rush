using System;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Inventory inventory;
    public Transform hotbar;
    public Color selectedColour, nonSelectedColour;

    //subscribe to events in Inventory class
    void Start()
    {
        inventory.ItemAdded += InventoryItemAdded;
        inventory.ItemSelect += InventoryItemSelect;
        //inventory.ItemRemoved += InventoryItemRemoved;    
    }

    //shows item into HUD after added into inventory list
    private void InventoryItemAdded(InventoryItem item){
        foreach (Transform slot in hotbar.transform){   
            //GameObject itemImage = slot.Find("Border/ItemImage").gameObject;
            GameObject itemImage = slot.GetComponentInChildren<EmptyPlaceholder>().gameObject;

            if(itemImage == null){
                Debug.Log("no item found");
            }     
        
            Image image = itemImage.GetComponent<Image>();

            //InventoryItemClickable button = slot.GetComponent<InventoryItemClickable>();

            if(!image.enabled){
                image.enabled = true;
                image.sprite = item.itemImage;
                //button.item = item;

                break;
            }
        }
    }

    private void InventoryItemSelect(InventoryItem item){
        Debug.Log("selet");
    }

    /*
    private void InventoryItemRemoved(InventoryItem item){
        Debug.Log("Remove Item");
        Transform panel = transform.Find("InventoryHud");

        foreach (Transform slot in panel){
            Image image = slot.GetComponent<Image>();
            //InventoryItemClickable button = slot.GetComponent<InventoryItemClickable>();

            if (button.item.itemName == item.itemName){
                image.enabled = false;
                image.sprite = null;
                //button.item = null;

                break;
            }
            /*
            if(!image.enabled){
                image.enabled = true;
                image.sprite = item.itemImage;
                button.item = item;

                break;
            }
        }
    }*/
}
