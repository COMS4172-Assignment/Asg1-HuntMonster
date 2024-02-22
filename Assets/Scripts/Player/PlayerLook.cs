using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour // the script is held on camera
{
    public Transform player_body; // the whole player object, rotate left/right
    public Vector2 lock_axis; // adjust the rotate. Sync by TouchController.cs
    public float sesitivity = 20f; // the sensitivity of left/right + up/down rotate

    // temp variables
    private float x_move;
    private float y_move;
    private float x_rotation;
    
    void Update()
    {
        // camera up/down rotate, player left/right rotate
        x_move = lock_axis.x * sesitivity * Time.deltaTime;
        y_move = lock_axis.y * sesitivity * Time.deltaTime;

        // rotate the camera
        x_rotation -= y_move;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);// limit the up/down rotate range
        transform.localRotation = Quaternion.Euler(x_rotation,0,0);
        // rotate the player
        player_body.Rotate(Vector3.up * x_move);
    }
}
