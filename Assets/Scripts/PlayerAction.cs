using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public LayerMask layerMask;
    private GameObject heldObject;
    [SerializeField] private Transform holdPosition;
    bool fire = false;

    public Inventory inventory;
    private ItemType currentItemType;
    private InventoryItem currentItem;

    private float fireRate = -1;
    private float nextFire = 0.0f;

    void Start(){
        inventory.ItemSelect += ItemSelected;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentItemType == ItemType.None){
            return;
        }

        if(currentItemType == ItemType.LiftTech){
            if(Input.GetButtonDown("Fire1")){
                if(heldObject == null){
                    RaycastHit hit;
                    //selects object and turns off gravity of object
                    if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f, layerMask)){
                        heldObject = hit.collider.gameObject;
                        heldObject.GetComponent<Rigidbody>().useGravity = false;
                    }
                } 
                Debug.Log("fire1");  

                currentItem.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            }

            if(Input.GetButtonUp("Fire1")){
                //Drop object
                if(heldObject != null){
                    heldObject.GetComponent<Rigidbody>().useGravity = true;

                    heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    heldObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    heldObject.GetComponent<Rigidbody>().ResetInertiaTensor();

                    heldObject = null;
                }

                currentItem.gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            }

            if(Input.GetButtonUp("Fire2")){
                if (heldObject != null){
                    fire = true;
                }
                Debug.Log("fire2"); 
            }
        }

        if(currentItemType == ItemType.Key){
            if(Input.GetKeyDown(KeyCode.F)){
                //Debug.Log("Using Key");
                inventory.UseItem(currentItem);
            }
        }

        if(currentItemType == ItemType.GunEMP){
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire){
                nextFire = Time.time + fireRate;

                currentItem.gameObject.GetComponentInChildren<Renderer>().material.EnableKeyword("_EMISSION");
                Fire();
                //Debug.Log("Enabled: " + x);
            }

            if(Input.GetButtonUp("Fire1")){
                currentItem.gameObject.GetComponentInChildren<Renderer>().material.DisableKeyword("_EMISSION");
            }
        }

    }

    
    void FixedUpdate(){
        if (heldObject != null){
            //moving object being held based on hold position
            heldObject.GetComponent<Rigidbody>().MoveRotation(holdPosition.rotation);
            Vector3 difference = holdPosition.position - heldObject.transform.position;
            heldObject.GetComponent<Rigidbody>().AddForce(difference * 500.0f);
            heldObject.GetComponent<Rigidbody>().velocity *= 0.1f;

            if(fire){
                fire = false;
                heldObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10.0f, ForceMode.Impulse);
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject = null;
            }
        }
    }

    private void ItemSelected(InventoryItem item){
        currentItemType = item.scriptableObjectItem.item_type;
        currentItem = item;

        if(fireRate == -1 && currentItemType == ItemType.GunEMP){
            fireRate = item.scriptableObjectItem.cooldown;
        }
    }

    void Fire(){
        RaycastHit hit;

        //draws a line from the camera in forward direction, what it hits is returned 
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity)){
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            
            if (hit.transform.tag == "DoorTarget"){
                //Open Door
                hit.collider.gameObject.GetComponent<DoorTarget>().DoorSlide();
            }
        }

        /*
    if (Input.GetButton("Fire1") && Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Fire();
        }

        void Fire(){
            RaycastHit hit;

            //draws a line from the camera in forward direction, what it hits is returned 
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity)){
                //instantitate a decal gameobject at locaiton of hit, rotates it to align with surface and moves it bit closer
                var spawned = Instantiate(decal, hit.point + hit.normal * 0.001f, Quaternion.FromToRotation(-Vector3.forward, hit.normal));
                //destroys decals after few seconds
                Destroy(spawned, 5.0f);
            }

            if (hit.transform.tag == "target"){
                Destroy(hit.transform.gameObject);
            }
        }*/

        
    }
}
