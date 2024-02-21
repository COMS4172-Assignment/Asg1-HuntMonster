using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMotion : MonoBehaviour
{
    public Camera playerCamera;
    public Transform playerbody;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;

    public bool canMove = true;
    public float mouseSensitivity = 100f;
    float xRotation;

    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }



    void Update()
    {
        if (GameScript.Instance.partyRoom) return;

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float vertical_input = Joystick.Instance.Vertical+Input.GetAxis("Vertical");
        float horizontal_input = Joystick.Instance.Horizontal+ Input.GetAxis("Horizontal");
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) *  vertical_input: 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * horizontal_input : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        characterController.Move(moveDirection * Time.deltaTime); // move

        #region Handles Rotation

        if (canMove)
        {
            float mouseX = 0;
            float mouseY = 0;
            if (Touchscreen.current.touches.Count == 0)
                return;

            if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[0].touchId.ReadValue()))
            {
                if (Touchscreen.current.touches.Count > 1 && Touchscreen.current.touches[1].isInProgress)
                {
                    if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[1].touchId.ReadValue()))
                        return;

                    Vector2 touchDeltaPosition = Touchscreen.current.touches[1].delta.ReadValue();
                    mouseX = touchDeltaPosition.x;
                    mouseY = touchDeltaPosition.y;
                }
            }
            else
            {
                if (Touchscreen.current.touches.Count > 0 && Touchscreen.current.touches[0].isInProgress)
                {
                    if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[0].touchId.ReadValue()))
                        return;

                    Vector2 touchDeltaPosition = Touchscreen.current.touches[0].delta.ReadValue();
                    mouseX = touchDeltaPosition.x;
                    mouseY = touchDeltaPosition.y;
                }

            }

            mouseX *= mouseSensitivity;
            mouseY *= mouseSensitivity;

            xRotation -= mouseY * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -80, 80);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            playerbody.Rotate(Vector3.up * mouseX * Time.deltaTime);
        }


        #endregion
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameScript.Instance.toggle_perspective();
        }
    }

}