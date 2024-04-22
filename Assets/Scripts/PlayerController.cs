using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float speed = 12;
    public float gravity = -10;
    public float jumpForce = 3f;
    Vector3 velocity;
    

    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            //reset velocity
            velocity.y = -1f;
        }

        //moving the player object forwards and backwards
        float forwardMoveInput = Input.GetAxis("Vertical");
        //moving from left to right
        float lateralMoveInput = Input.GetAxis("Horizontal");

        //player moves in the direction relative to the camera direction, not global forward or right directions
        Vector3 movement = transform.right * lateralMoveInput + transform.forward * forwardMoveInput;
        characterController.Move(movement * speed * Time.deltaTime);

        //apply jump
        if (Input.GetButton("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }

        //apply gravity to player
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    /*
    void LateUpdate()
    {
        [Header("Camera")]
        [SerializeField] private float mouseSensitivity = 200;
        [SerializeField] private Transform mainCamera;
        private float xRotate = 0f;

        float mouseX = Input.GetAxis("Mouse X") *  mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, -90, 90);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotate, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
        
    }*/
}
