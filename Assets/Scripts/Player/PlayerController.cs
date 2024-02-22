using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform orbsSpawnPoint; // the start position the orbs
    public GameObject orbsPrefab;
    public float orbsSpeed = 10;
    public int health = 5;
    private int cur_health; // current health
    public Slider healthbar; // health bar UI
    public Text healthText; // health value

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
        healthbar.value = (float)cur_health / health;
        healthText.text=cur_health.ToString();
    }
    public void on_hit() // when player is hit by creature's orb/projectile
    {
        Debug.Log("hit!");
        cur_health = cur_health - 1;
        // update UI
        healthbar.value = (float)cur_health / health;
        healthText.text = cur_health.ToString();
        if (cur_health==0) // if health is 0, player loses
        {
            GameScript.Instance.lose();
        }
    }

    public void hit() // player throws orbs
    {
        var orbs = Instantiate(orbsPrefab, orbsSpawnPoint.position, orbsSpawnPoint.rotation);
        orbs.GetComponent<Rigidbody>().velocity = orbsSpawnPoint.forward * orbsSpeed;
    }
}
