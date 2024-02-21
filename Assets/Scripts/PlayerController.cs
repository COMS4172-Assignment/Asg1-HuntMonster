using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform orbsSpawnPoint;
    public GameObject orbsPrefab;
    public float orbsSpeed = 10;

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

    void Update()
    {
        if (GameScript.Instance.partyRoom) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            var orbs = Instantiate(orbsPrefab, orbsSpawnPoint.position, orbsSpawnPoint.rotation);
            orbs.GetComponent<Rigidbody>().velocity = orbsSpawnPoint.forward * orbsSpeed;
        }
    }

    public void on_hit()
    {
        Debug.Log("hit!");
    }
}
