using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Creature { LandCreature, SeaCreature, AirCreature }
public class GameScript : MonoBehaviour
{
    private static GameScript _instance;
    public GameObject player;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public bool partyRoom;

    public static GameScript Instance { get { return _instance; } }


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
        switch_cam1();
        partyRoom = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P)) 
        {
            if (!partyRoom)
            {
                switch_party_room();
            }
            else
            {
                switch_game_room();
            }
        }
    }

    public void switch_party_room()
    {
        //SceneManager.LoadScene("GameScene");
        switch_cam3();
        partyRoom=true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void switch_game_room()
    {
        //SceneManager.LoadScene("Game",LoadSceneMode.Additive);
        switch_cam1();
        partyRoom = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void switch_cam1()
    {
        cam1.enabled=true;
        cam2.enabled=false;
        cam3.enabled=false;
    }

    public void switch_cam2()
    {
        cam1.enabled = false;
        cam2.enabled = true;
        cam3.enabled = false;
    }

    public void switch_cam3()
    {
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = true;
    }

    public void toggle_perspective()
    {
        if (!cam3.enabled)
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }
    }

}
