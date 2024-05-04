using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnvironment : MonoBehaviour
{
    public int damage;
    public float damageCooldown;
    private float lastTimeCollided;

    void OnTriggerStay(Collider hitobject){
        if(Time.time - lastTimeCollided < damageCooldown) return;

        if(hitobject.gameObject.CompareTag("Player")){
            PlayerHealth health  = hitobject.gameObject.GetComponent<PlayerHealth>();
            health.takeDamage(damage);

            lastTimeCollided = Time.time;
        }
    }
}
