using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemImage;

    //script to hold information about items
    //disables item when picked up
    public void onPickup(){
        gameObject.SetActive(false);
    }
}
