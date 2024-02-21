using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCollider : MonoBehaviour
{
    public CreatureController controller;
    void Start()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==7) // player
        {
            controller.on_player_enter();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer==7)
        {
            controller.on_player_exit();
        }
    }
}
