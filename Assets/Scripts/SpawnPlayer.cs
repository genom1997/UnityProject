using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour {
    GameObject manager, renderer;
    GameObject canvas;
    GameObject localCanvas;
    GameObject play;
    GameObject tile, localTile, movementManager;
    int startX;
    int startY;
    // Use this for initialization
    void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        localCanvas = GameObject.FindGameObjectWithTag("LocalCanvas");
        Map mp = manager.GetComponent<GameManager>().map;
        startX = Random.Range(0, mp.mapWidth);
        startY = Random.Range(0, mp.mapLength);
        movementManager = GameObject.Find("Movement Controller");
    }

    void Spawn() {
        Debug.Log("Player starts at : " + startX + "," + startY);
        if (canvas.GetComponent<Transform>().childCount != 0) {
            int i = 0;
            int x = canvas.transform.GetChild(i).gameObject.GetComponent<Holder>().x;
            int y = canvas.transform.GetChild(i).gameObject.GetComponent<Holder>().y;
            //search through the children in the map
            while (i < canvas.GetComponent<Transform>().childCount)
            {
                if ((startX == canvas.transform.GetChild(i).gameObject.GetComponent<Holder>().x) && (startY == canvas.transform.GetChild(i).gameObject.GetComponent<Holder>().y))
                {
                    //if found get a reference to the tile the player spawns in
                    tile = canvas.transform.GetChild(i).gameObject;
                    break;
                }
                i++;
            }
        }
        //set the tiles color to white
        tile.GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        //set the spawn tile in the map renderer to be this tile
        renderer = GameObject.Find("Renderer");
        //tell the manager where the player is
        manager.SendMessage("SetCurrentTile", tile);
        renderer.SendMessage("SetSpawnTile", tile);
    }

    void LocalSpawn() {
        //get the map
        Map mP = manager.GetComponent<GameManager>().map;
        //set spawn x and y within the bounds of the global maptile that was spawned in 
        int X = Random.Range(0, mP.tile(startX,startY).Width);
        int Y = Random.Range(0, mP.tile(startX, startY).Length);
        Debug.Log("Spawning player at tile : " + startX + "," + startY + "; at pos : " + X + " , " + Y);

        
        if (localCanvas.GetComponent<Transform>().childCount != 0)
        {
            int i = 0;
            while (i < localCanvas.GetComponent<Transform>().childCount)
            {
                //code breaks because of return button so check to see if not the button
                if (localCanvas.transform.GetChild(i).childCount == 0)
                {
                    //while searching through the children of the local map, if the i'th child's holder has the x and y coordinates of the correct spawn position, continue
                    if ((X == localCanvas.transform.GetChild(i).gameObject.GetComponent<UnitTileHolder>().x) && (Y == localCanvas.transform.GetChild(i).gameObject.GetComponent<UnitTileHolder>().y))
                    {
                        //set reference to the gameobject that is the spawned player position
                        localTile = localCanvas.transform.GetChild(i).gameObject;
                        localTile.GetComponent<UnitTileHolder>().isSpawn = true;
                        localTile.GetComponent<UnitTileHolder>().isActive = true;
                        break;
                    }
                }
                i++;
            }
            localTile.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            //store spawning position in a vector 2 and send that tiles position to the movement manager
            Vector2 tilePos = new Vector2();
            tilePos.x = localTile.GetComponent<UnitTileHolder>().x;
            tilePos.y = localTile.GetComponent<UnitTileHolder>().y;
            movementManager.SendMessage("SetPos", tilePos);
        }
        

    }
}
