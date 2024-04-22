using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 200;
    [SerializeField] private Transform player;
    private float xRotate = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") *  mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotate, 0, 0);
        player.Rotate(Vector3.up * mouseX);
        
    }
}
