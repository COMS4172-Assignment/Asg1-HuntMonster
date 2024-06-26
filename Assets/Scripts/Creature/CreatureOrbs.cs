using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureOrbs : MonoBehaviour
{
    public float life = 3;
    bool collide = false;

    public void on_throw()
    {
        Destroy(gameObject, life);
        GetComponent<Rigidbody>().useGravity = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7 && !collide) // prevent multiple collides (can't sync)
        {
            collide = true;
            PlayerController.Instance.on_hit();
            Destroy(this.gameObject);
        }
    }
}