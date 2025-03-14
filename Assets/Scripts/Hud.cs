using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Hud : MonoBehaviour
{
    public Inventory inventory;
    public Transform hotbar;
    public Color selectedColour, nonSelectedColour;
    private GameObject[] slotBorder;

    [SerializeField] private TMP_Text itemText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text bestTime;

    private float t;

    //subscribe to events in Inventory class
    void Start()
    {
        inventory.ItemAdded += InventoryItemAdded;
        inventory.ItemSelect += InventoryItemSelect;
        inventory.ItemRemoved += InventoryItemRemoved;
 
        //Gets all Border gameobjects by tag and then sorts in order by name
        slotBorder = GameObject.FindGameObjectsWithTag("SlotBorder");
        slotBorder = slotBorder.OrderBy(slot => slot.name).ToArray();   

        getBest();     
    }

    void Update(){
        t = Time.timeSinceLevelLoad;
		int mins = (int)( t / 60 );
		int rest = (int)(t % 60);
		timeText.text = string.Format("Playtime - {0:D2}:{1:D2}", mins, rest);
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
        itemText.text = item.itemName;
    }

    private void ResetItemSelect(){
        foreach(GameObject border in slotBorder){
            border.GetComponent<Image>().color = nonSelectedColour;
        }
        itemText.text = "";
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

    private void getBest(){
        t = PlayerPrefs.GetFloat("time");

        if(t > 0){
            int mins = (int)( t / 60 );
            int rest = (int)(t % 60);
            bestTime.text = string.Format("Best Time - {0:D2}:{1:D2}", mins, rest);
        }
        else{
            bestTime.text = "";
        }
    }

    public float GetTime(){
        return t;
    }
}
