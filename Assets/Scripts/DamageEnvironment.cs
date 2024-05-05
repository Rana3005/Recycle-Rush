using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnvironment : MonoBehaviour
{
    public bool isDamageOn;
    public int damage;
    public float damageCooldown;
    public Material damageOnColour, damageOffColour;
    private float lastTimeCollided;

    void Start(){
        if(isDamageOn){
            DamageOn();
        } else {
            DamageOff();
        }
    }

    void OnTriggerStay(Collider hitobject){
        if(isDamageOn){
            if(Time.time - lastTimeCollided < damageCooldown) return;

            if(hitobject.gameObject.CompareTag("Player")){
                PlayerHealth health  = hitobject.gameObject.GetComponent<PlayerHealth>();
                health.takeDamage(damage);

                lastTimeCollided = Time.time;
            }
        }
    }

    public void DamageOn(){
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", damageOnColour.color);
        isDamageOn = true;
    }

    public void DamageOff(){
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", damageOffColour.color);
        isDamageOn = false;
    }
}
