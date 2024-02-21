using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    
    public Joystick joystick;
    public float move_speed = 20f;
    public float gravity = 50f;
    public float jump_force = 60f;
    private CharacterController character_controller;
    Vector3 move_direction=Vector3.zero;
    bool ready_jump;

    void Start()
    {
        character_controller= GetComponent<CharacterController>();
    }

    
    void Update()
    {
        float MoveDirectionY = move_direction.y;
        move_direction = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical) * move_speed;
        if (jumping() && character_controller.isGrounded)
        {
            move_direction.y = jump_force;
        }
        else
        {
            move_direction.y = MoveDirectionY;
        }

        if (!character_controller.isGrounded)
        {
            move_direction.y -= gravity * Time.deltaTime;
        }
        character_controller.Move(move_direction  * Time.deltaTime);
    }

    public void jump()
    {
        ready_jump = true;
    }

    private bool jumping()
    {
        if (ready_jump==true) 
        {
            ready_jump = false;
            return true;
        }
        return false;
    }
}
