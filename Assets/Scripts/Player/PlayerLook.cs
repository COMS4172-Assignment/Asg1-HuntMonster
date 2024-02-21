using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float x_move;
    private float y_move;
    private float x_rotation;
    [SerializeField] private Transform player_body;
    public Vector2 lock_axis;
    public float cam_sesitivity = 40f;

    
    void Update()
    {
        x_move = lock_axis.x * cam_sesitivity * Time.deltaTime;
        y_move = lock_axis.y * cam_sesitivity * Time.deltaTime;
        x_rotation -= y_move;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(x_rotation,0,0);
        player_body.Rotate(Vector3.up * x_move);
    }
}
