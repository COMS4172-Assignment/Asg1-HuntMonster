using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public UIDragField fixed_touch_field;
    public PlayerLook cam_look;

    
    void Update()
    {
        cam_look.lock_axis = fixed_touch_field.TouchDist;
    }
}
