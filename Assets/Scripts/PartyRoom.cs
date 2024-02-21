using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyRoom : MonoBehaviour
{
    private static PartyRoom _instance;
    public static PartyRoom Instance { get { return _instance; } }

    public Transform[] platforms;
    public GameObject air_creatures;
    public GameObject sea_creature;
    public GameObject land_creature;
    [SerializeField]
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    public void add_creature(Creature ct)
    {
        if (ct==Creature.LandCreature)
        {
            Instantiate(land_creature, platforms[(int)ct].position+Vector3.up*3, platforms[(int)ct].rotation);
        }
        else if (ct == Creature.SeaCreature)
        {
            Instantiate(sea_creature, platforms[(int)ct].position + Vector3.up * 3, platforms[(int)ct].rotation);
        }
        else if (ct == Creature.AirCreature)
        {
            Instantiate(air_creatures, platforms[(int)ct].position + Vector3.up * 3, platforms[(int)ct].rotation);
        }
    }

}
