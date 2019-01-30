using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//brings the data stored in the map on the game manager into accessable variables stored on the maptile gameobjects
public class Holder : MonoBehaviour {
    public MapTile tile;
    public Map map;
    public int x, y;
    int spawnX, spawnY;
    GameObject localMapRenderer, spawner, movementController, tileObject;
    public bool isSpawn = false;
    public bool isSpawned = false;
    public bool isActive = false;
    public Person[] people;

        // Use this for initialization
        void Start()
        {
            EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
            trigger.triggers.Add(entry);
            localMapRenderer = GameObject.FindGameObjectWithTag("LocalRenderer");
            spawner = GameObject.Find("PlayerSpawner");
            movementController = GameObject.Find("Movement Controller");
            map = GameObject.Find("GameManager").GetComponent<GameManager>().map;
            //set the people in the holder to the people in the maptile
            people = map.m[x, y].People;
        }

    public void SetIsSpawn() {
        isSpawn = true;
    }

    public void PlayerSpawned() {
        isSpawned = true;
    }
    //find the players in the map that live in this tile and put them in people
    public void FindPlayers() {

    }
    //controller that handles when you click on the tile
    public void OnPointerDownDelegate(PointerEventData data)
        {
            //if clicked, render the maptile that was clicked 
            Debug.Log("Selected tile at pos " + x + "," + y + " environment type " + tile.environment.ty);
            localMapRenderer.SendMessage("Render", tile);
            //sends the movement controller which tile we are in
            movementController.SendMessage("SetTile", tile);
            //if the maptile that we spawned in is clicked, spawn the player locally. Only call once. After this is called the spawned changes is spawned to true and we dont call this again
            //(issue is this means it wont save your location after you spawn tho. To be fixed at a later date)
            if (isSpawn && !isSpawned) {
                spawner.SendMessage("LocalSpawn");
                isSpawned = true;
            }   
        }
}
