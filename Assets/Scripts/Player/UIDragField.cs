using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragField : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] // hide the variable in the inspector
    public Vector2 TouchDist; // feed to rotation control
    [HideInInspector]
    public Vector2 PointerOld; // old touch position
    [HideInInspector]
    public int PointerId; // assigned at the beginning of the touch
    [HideInInspector]
    public bool Pressed; // whether the screen touch field is pressed

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)// if the touch is on the screen
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData) // when finger touch the screen
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData) // when finger leave the screen
    {
        Pressed = false;
    }
}
