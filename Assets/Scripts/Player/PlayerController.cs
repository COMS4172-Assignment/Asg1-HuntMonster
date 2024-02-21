using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform orbsSpawnPoint;
    public GameObject orbsPrefab;
    public float orbsSpeed = 10;
    public int health = 5;
    private int cur_health;
    public Slider healthbar;

    private static PlayerController _instance;
    public static PlayerController Instance { get { return _instance; } }
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

    private void Start()
    {
        cur_health = health;
    }
    public void on_hit()
    {
        Debug.Log("hit!");
        cur_health = cur_health - 1;
        healthbar.value = (float)cur_health / health;
        if (cur_health==0)
        {
            GameScript.Instance.lose();
        }
    }

    public void hit()
    {
        var orbs = Instantiate(orbsPrefab, orbsSpawnPoint.position, orbsSpawnPoint.rotation);
        orbs.GetComponent<Rigidbody>().velocity = orbsSpawnPoint.forward * orbsSpeed;
    }
}
