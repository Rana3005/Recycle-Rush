using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{

    [Header("Sliding")]
    [SerializeField] private bool slideUp;
    [SerializeField] private bool slideRight;
    [SerializeField] private bool slideLeft;
    [SerializeField] private bool turnDoor;

    [SerializeField] float slideAmount = 0f;
    [SerializeField] int TurnAmount = 0;
    private bool isOpen = false;
    private float speed = 1f;
    private Vector3 slideDirection;
    private Vector3 slideLeftDirection = Vector3.left;

    private Vector3 startPosition;

    void Start(){
        startPosition = transform.position;

    }

    public void DoorInteract(){
        if(slideUp){
            slideDirection = Vector3.up;
            if(!isOpen){
                StartCoroutine(SlideOpen());
            }
            if (isOpen) {
                StartCoroutine(SlideClose());
            }
        }
        else if(slideRight){
            slideDirection = Vector3.right;
            if(!isOpen){
                StartCoroutine(SlideOpen());
            }
            if (isOpen) {
                StartCoroutine(SlideClose());
            }
        }
        else if (turnDoor){
            if(!isOpen){
                StartCoroutine(DoorTurn(TurnAmount));
            }
            if (isOpen) {
                StartCoroutine(DoorTurn(0));
            }
        }
    }

    private IEnumerator SlideOpen(){
        Vector3 endPos = startPosition + slideAmount * slideDirection;
        Vector3 startPos = transform.position;

        float time = 0;
        
        while(time < 1){
            transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
            time += Time.deltaTime * speed;
        }/*
        for (float r = 0.0f; r < 1.0f; r+= 0.01f){
            linkedDoor.transform.position = Vector3.Lerp(startPos, endPos, r);
            yield return null;
        }*/
        isOpen = true;
    }

    private IEnumerator SlideClose(){
        Vector3 endPos = startPosition;
        Vector3 startPos = transform.position;

        float time = 0;
        
        while(time < 1){
            transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        /*
        for (float r = 0.0f; r < 1.0f; r+= 0.01f){
            transform.position = Vector3.Lerp(startPos, endPos, r);
            yield return null;
        }*/
        isOpen = false;
    }

     private IEnumerator DoorTurn(int targetangle){
        float originalAngle = transform.localEulerAngles.y;

        for (float r = 0.0f; r < 1.0f; r+= 0.01f){
            transform.localEulerAngles = new Vector3(0, Mathf.LerpAngle(originalAngle, targetangle, r), 0);
            yield return null;
        }

        if(isOpen){
            isOpen = false;
        } 
        else{
            isOpen = true;
        }
    }

    public bool getIsOpen(){
        return isOpen;
    }

}
