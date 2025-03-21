using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class ItemObstacleCheck : MonoBehaviour
{
    public bool isDoor;
    public bool isFence;
    public bool isMovingPlatform;
    public bool isConveyorBelt;
    public GameObject checkObject;
    public GameObject linkedObstacle; 
    private LightningBoltScript[] lightnings;

    private void OnTriggerEnter(Collider hitObject){
        if(isDoor){
            if(hitObject.CompareTag(checkObject.tag)){
                linkedObstacle.GetComponent<Door>().DoorInteract();
            }
        }
        else if(isFence){
            if(hitObject.CompareTag(checkObject.tag)){
                linkedObstacle.GetComponent<DamageEnvironment>().DamageOff();
                
                lightnings = linkedObstacle.GetComponentsInChildren<LightningBoltScript>();
                if(lightnings != null){
                    foreach(LightningBoltScript lightning in lightnings){
                        lightning.gameObject.SetActive(false);
                    }
                }
            }
        }
        else if(isMovingPlatform){
            if(hitObject.CompareTag(checkObject.tag)){
                linkedObstacle.GetComponent<MovingPlatform>().isMoving = true;
            }
        }
        else if (isConveyorBelt){
            if(hitObject.CompareTag(checkObject.tag)){
                linkedObstacle.GetComponent<BeltManager>().isBeltOn = true;
            }
        }
    }

    private void OnTriggerExit(Collider hitObject){
        if(isDoor){
            if(hitObject.CompareTag(checkObject.tag)){
                linkedObstacle.GetComponent<Door>().DoorInteract();
            }
        }
        else if(isFence){
            if(hitObject.CompareTag(checkObject.tag)){
                linkedObstacle.GetComponent<DamageEnvironment>().DamageOn();

                if(lightnings != null){
                    foreach(LightningBoltScript lightning in lightnings){
                        lightning.gameObject.SetActive(true);
                    }
                }
            }
        }
        else if(isMovingPlatform){
            if(hitObject.CompareTag(checkObject.tag)){
                linkedObstacle.GetComponent<MovingPlatform>().isMoving = false;
            }
        }
        else if (isConveyorBelt){
            if(hitObject.CompareTag(checkObject.tag)){
                linkedObstacle.GetComponent<BeltManager>().isBeltOn = false;
            }
        }

    }
}
