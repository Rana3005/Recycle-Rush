using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void InventorEventDelegate(InventoryItem item);

    public InventorEventDelegate ItemAdded;
    public InventorEventDelegate ItemUsed;
    public InventorEventDelegate ItemRemoved;
    public InventorEventDelegate ItemSelect;

    //list to hold all items
    List<InventoryItem> items = new List<InventoryItem>();
    private int selectedSlotIndex = 0;

    public void AddItem(InventoryItem item){
        if(items.Contains(item)){
            return;
        }

        items.Add(item);
        item.onPickup();

        ItemAdded?.Invoke(item);
        if(items.Count == 1){ChangeItem(1);}
    }
    
    public void UseItem(InventoryItem item){
        Debug.Log("item being used");
        ItemUsed?.Invoke(item);
    }

    public void ChangeItem(int selectedNum){
        int itemIndex = selectedNum - 1;
        
        if(selectedNum > items.Count){
            Debug.Log("no item"); 
            return;
        }

        selectedSlotIndex = itemIndex;
        NewItemSelect();
    }

    public void RemoveItem(InventoryItem inventoryItem)
    {
        Debug.Log("removeItem");
        items.Remove(inventoryItem);
        ItemRemoved?.Invoke(inventoryItem);
    }

    private void NewItemSelect(){
        if(items.Count == 0){ return; }

        foreach(InventoryItem item in items){
            item.gameObject.SetActive(false);
        }

        items[selectedSlotIndex].gameObject.SetActive(true);        
    }
}
