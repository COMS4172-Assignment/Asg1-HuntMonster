using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            CreatureController cc;
            if (collision.gameObject.transform.parent.parent.GetComponent<CreatureController>() != null)
            {
                cc = collision.gameObject.transform.parent.parent.GetComponent<CreatureController>();
            }
            else
            {
                cc = collision.gameObject.transform.parent.parent.parent.GetComponent<CreatureController>();
            }
            if(cc.in_range)
            {
                cc.on_hit();
                PartyRoom.Instance.add_creature(cc.creature_type);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}