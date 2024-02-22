using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{
    public float life = 3; // life of the orb

    void Awake()
    {
        Destroy(gameObject, life);// destroy the orb after life time on initialization
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)// if hit creature
        {
            CreatureController cc;
            // get the creature controller, depending on the tye of creature
            if (collision.gameObject.transform.parent.parent.GetComponent<CreatureController>() != null)
            {
                cc = collision.gameObject.transform.parent.parent.GetComponent<CreatureController>();
            }
            else
            {
                cc = collision.gameObject.transform.parent.parent.parent.GetComponent<CreatureController>();
            }
            if(cc.in_range) // if the creature is in range, they will be hit
            {
                cc.on_hit();
                PartyRoom.Instance.add_creature(cc.creature_type);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 10) // if hit the ceature's orb (projectile)
        {
            // cancel each other
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}