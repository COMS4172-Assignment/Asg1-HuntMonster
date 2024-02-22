using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WandMiove : MonoBehaviour
{
    public Scrollbar scrollbar; // connect the direction of the wand
    float dest_rotation;

    void Update()
    {
        dest_rotation = (scrollbar.value - 0.5f) * 90; // scrollbar.value is 0-1, so -0.5f to make it -0.5f to 0.5f, then *90 to make it -45 to 45
        // note localRotation.x is not in Euler angle
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,dest_rotation, transform.localRotation.eulerAngles.z);
    }
}
