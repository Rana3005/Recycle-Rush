using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    public bool isAnimated = false;

    [Header("Rotating")]
    [Space(10)]
    public bool isRotating = false;
    public Vector3 rotationAngle;
    public float rotationSpeed;
    
    [Header("Floating")]
    [Space(10)]
    public bool isFloating = false;
    public float floatSpeed;
    public float height = 0.5f;

	// Update is called once per frame
	void Update () {
        if(isAnimated)
        {
            if(isRotating)
            {
                transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
            }

            if(isFloating)
            {
                Vector3 pos = transform.position;
                //new y position
                float newPosY = Mathf.Sin(Time.time * floatSpeed * pos.y);
                //new position set to object
                transform.position = new Vector3(pos.x, newPosY * height, pos.z);
            }
        }
	}
}
