using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Hud : MonoBehaviour
{
    public Inventory inventory;
    public Transform hotbar;
    public Color selectedColour, nonSelectedColour;
    private GameObject[] slotBorder;


    //subscribe to events in Inventory class
    void Start()
    {
        inventory.ItemAdded += InventoryItemAdded;
        inventory.ItemSelect += InventoryItemSelect;
        inventory.ItemRemoved += InventoryItemRemoved;
 
        //Gets all Border gameobjects by tag and then sorts in order by name
        slotBorder = GameObject.FindGameObjectsWithTag("SlotBorder");
        slotBorder = slotBorder.OrderBy(slot => slot.name).ToArray();        
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

            if(!image.enabled){
                image.enabled = true;
                image.sprite = item.itemImage;

                break;
            }
        }
    }

    private void InventoryItemSelect(InventoryItem item){
        //changes all border colour to not selected and then changes selected colour border
        foreach(GameObject border in slotBorder){
            border.GetComponent<Image>().color = nonSelectedColour;
        }

        slotBorder[inventory.selectedSlotIndex].GetComponent<Image>().color = selectedColour;
    }

    private void ResetItemSelect(){
        foreach(GameObject border in slotBorder){
            border.GetComponent<Image>().color = nonSelectedColour;
        }
    }

    
    private void InventoryItemRemoved(InventoryItem item){
        Debug.Log("Items: " + inventory.getItemListCount());
        if(inventory.getItemListCount() == 0){
            Debug.Log("No Items");
            ResetItemSelect();
            foreach (Transform slot in hotbar.transform){   
                //GameObject itemImage = slot.Find("Border/ItemImage").gameObject;
                GameObject itemImage = slot.GetComponentInChildren<EmptyPlaceholder>().gameObject;
                Image image = itemImage.GetComponent<Image>();

                if(image.enabled){
                    image.enabled = false;          
                } else{
                    break;
                }
            }
            return;
        }

        int itemIndex = 0;
        foreach (Transform slot in hotbar.transform){   
            //GameObject itemImage = slot.Find("Border/ItemImage").gameObject;
            GameObject itemImage = slot.GetComponentInChildren<EmptyPlaceholder>().gameObject;
            Image image = itemImage.GetComponent<Image>();

            if(itemIndex < inventory.getItemListCount()){
                image.sprite = inventory.getList()[itemIndex].itemImage;
                itemIndex++;
            }
            else {
                image.enabled = false;
            }
        }

        inventory.ChangeItem(inventory.getItemListCount());
    }
}
