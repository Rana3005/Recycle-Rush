using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void InventorEventDelegate(InventoryItem item);

    public InventorEventDelegate ItemAdded;
    public InventorEventDelegate ItemUsed;
    public InventorEventDelegate ItemRemoved;

    //list to hold all items
    List<InventoryItem> items = new List<InventoryItem>();

    public void addItem(InventoryItem item){
        if(items.Contains(item)){
            return;
        }

        items.Add(item);
        Debug.Log(items.Count);
        item.onPickup();

        ItemAdded?.Invoke(item);
    }
    
    public void useItem(InventoryItem item){
        Debug.Log("item being used");
        ItemUsed?.Invoke(item);
    }

    internal void removeItem(InventoryItem inventoryItem)
    {
        Debug.Log("removeItem");
        items.Remove(inventoryItem);
        ItemRemoved?.Invoke(inventoryItem);
    }
}
