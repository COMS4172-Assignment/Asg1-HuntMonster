using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Creature { LandCreature, SeaCreature, AirCreature }
public class GameScript : MonoBehaviour
{
    private static GameScript _instance;
    public bool partyRoom; // whether in party room
    public GameObject player;
    // cameras
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    // UI control
    public GameObject partyRoomUI;
    public GameObject gameUI;
    public GameObject WinUI;
    public GameObject LoseUI;

    public static GameScript Instance { get { return _instance; } }


    // singleton pattern
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
        // on game start, set up game room, and disable party room
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        switch_cam1();
        switch_game_room();
        partyRoom = false;
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

    public void toggle_perspective() // change player camera
    {
        if (!cam3.enabled)
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }
    }

    public void switch_party_room() // switch to party room
    {
        switch_cam3();
        partyRoomUI.SetActive(true);
        gameUI.SetActive(false);
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        partyRoom = true;
    }
    public void switch_game_room() // switch to game room
    {
        switch_cam1();
        partyRoomUI.SetActive(false);
        gameUI.SetActive(true);
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        partyRoom = false;
    }

    public void lose() // lose
    {
        LoseUI.SetActive(true) ;
        WinUI.SetActive(false) ;
        gameUI.SetActive(false) ;
        partyRoomUI.SetActive(false ) ;
    }

    public void win() // win
    {
        WinUI.SetActive(true) ;
        LoseUI.SetActive(false );
        gameUI.SetActive(false);
        partyRoomUI.SetActive(false);
    }

    public void exit() // exit
    {
        Application.Quit();
    }
    public void restart() // restart
    {
        SceneManager.LoadScene("Game");
    }
}
