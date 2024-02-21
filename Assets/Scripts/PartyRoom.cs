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
    private bool[] collected;
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
        collected = new bool[6];
    }

    // Update is called once per frame
    public void add_creature(Creature ct)
    {
        if (ct==Creature.LandCreature)
        {
            if (!collected[(int)Creature.LandCreature])
            {
                Instantiate(land_creature, platforms[(int)ct].position+Vector3.up*8, platforms[(int)ct].rotation, platforms[(int)ct]);
                collected[(int)Creature.LandCreature] = true;
            }
        }
        else if (ct == Creature.SeaCreature)
        {
            if (!collected[(int)Creature.SeaCreature])
            {
                Instantiate(sea_creature, platforms[(int)ct].position + Vector3.up * 8, platforms[(int)ct].rotation, platforms[(int)ct]);
                collected[(int)Creature.SeaCreature] = true;
            }
        }
        else if (ct == Creature.AirCreature)
        {
            if (!collected[(int)Creature.AirCreature])
            {
                Instantiate(air_creatures, platforms[(int)ct].position + Vector3.up * 8, platforms[(int)ct].rotation, platforms[(int)ct]);
                collected[(int)Creature.AirCreature] = true;
            }
        }
        if(collected[(int)Creature.LandCreature] && collected[(int)Creature.SeaCreature] && collected[(int)Creature.AirCreature])
        {
            GameScript.Instance.win();
        }
    }

}
