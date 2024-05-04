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
    
    public int selectedSlotIndex = -1;
    public InventoryItem currentItem;

    public void AddItem(InventoryItem item){
        if(items.Contains(item)){
            return;
        }

        items.Add(item);
        item.onPickup();

        ItemAdded?.Invoke(item);
        if(items.Count == 1){
            ChangeItem(1);
        }
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
        currentItem = items[itemIndex];

        NewItemSelect(itemIndex);                   //Updates item gameobject
        ItemSelect?.Invoke(items[itemIndex]);       //Updates HUD inventory
    }

    public void RemoveItem(InventoryItem inventoryItem)
    {
        Debug.Log("removeItem");
        items.Remove(inventoryItem);
        ItemRemoved?.Invoke(inventoryItem);
        
        Destroy(inventoryItem.gameObject);
        
    }

    private void NewItemSelect(int selectedIndex){
        if(items.Count == 0){ return; }
        
        //disables all inventory items and enables selected inventory item
        foreach(InventoryItem item in items){
            item.gameObject.SetActive(false);
        }

        items[selectedIndex].gameObject.SetActive(true);        
    }

    public int getItemListCount(){
        return items.Count;
    }

    public List<InventoryItem> getList(){
        return items;
    }
}
