using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleItemContainer : MonoBehaviour
{
    public int maxItems;
    public RecycleManager recycleManager;
    private int currentItems;
    private bool isMaxItems {get; set;}

    [SerializeField] private bool isPaper;
    [SerializeField] private bool isGlass;
    [SerializeField] private bool isMetal;
    [SerializeField] private bool isPlastic;
    private String materialType;

    void Start(){
        if(isPaper){
            materialType = "Paper";
            recycleManager.UpdateScore(materialType, currentItems, maxItems);
        }
        else if(isGlass){
            materialType = "Glass";
            recycleManager.UpdateScore(materialType, currentItems, maxItems);
        }
        else if(isMetal){
            materialType = "Metal";
            recycleManager.UpdateScore(materialType, currentItems, maxItems);
        }
        else if(isPlastic){
            materialType = "Plastic";
            recycleManager.UpdateScore(materialType, currentItems, maxItems);
        }
    }


    private void OnTriggerEnter(Collider item){
        if(item.gameObject.tag == materialType){
            currentItems++;
            recycleManager.UpdateScore(materialType, currentItems, maxItems);
            checkItemMax();
            recycleManager.AllItemSorted();
        }
    }

    private void OnTriggerExit(Collider item){
        if(item.gameObject.tag == materialType){
            currentItems--;
            recycleManager.UpdateScore(materialType, currentItems, maxItems);
            checkItemMax();
            recycleManager.AllItemSorted();
        }
    }

    private void checkItemMax(){
        if(currentItems >= maxItems){
            isMaxItems = true;
            recycleManager.maxItemCheck[materialType] = isMaxItems;
        }
        else {
            isMaxItems = false;
            recycleManager.maxItemCheck[materialType] = isMaxItems;
        }
    }

}
