using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WandMiove : MonoBehaviour
{
    public Scrollbar scrollbar;
    float rotation;
    float dest_rotation;
    public Quaternion show;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dest_rotation = (scrollbar.value - 0.5f) * 90;
        show = transform.localRotation;
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,dest_rotation, transform.localRotation.eulerAngles.z);
    }
}
