using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapRenderer : MonoBehaviour {
    public GameObject manager, spawnManager, spawnTile;
    Map map;
    Player player;
    public GameObject forestTile, planeTile, mountainTile;
    int width, length;
    Transform canvas;
    public GameObject[] renderedTiles;

    //****************WORLD PROCESSES START HERE*********************

	//set references and render the map based on the map stored in the gamemanager
	void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        manager = GameObject.FindGameObjectWithTag("Manager");
        map = manager.GetComponent<GameManager>().map;
        player = manager.GetComponent<GameManager>().player;
        width = map.mapWidth;
        length = map.mapLength;
        renderedTiles = new GameObject[length*width];
        //render the global map
        RenderGlobalMap();

        //after the map has been rendered call the spawn player function from the player spawner
        spawnManager = GameObject.Find("PlayerSpawner");
        spawnManager.SendMessage("Spawn");
    }

    //called after the player is spawned and the result is stored in this object
    void SetSpawnTile(GameObject x) {
        //do this by calling the set is spawn method on the maptiles holder
        spawnTile = x;
        spawnTile.SendMessage("SetIsSpawn");
    }

    //render the global map tiles based on their trait
    void RenderGlobalMap () {
        int i = 0;
        foreach (MapTile mt in map.m) {
            if (mt.environment.ty.Equals("forest"))
            {
                renderedTiles[i] = Instantiate(forestTile, canvas) as GameObject;
                renderedTiles[i].AddComponent<Holder>();
                Holder thisHolder = renderedTiles[i].GetComponent<Holder>();
                thisHolder.tile = mt;
                thisHolder.map = map;
                thisHolder.x = mt.X;
                thisHolder.y = mt.Y;
            }
            else if (mt.environment.ty.Equals("plane"))
            {
                renderedTiles[i] = Instantiate(planeTile, canvas) as GameObject;
                renderedTiles[i].AddComponent<Holder>();
                Holder thisHolder = renderedTiles[i].GetComponent<Holder>();
                thisHolder.tile = mt;
                thisHolder.map = map;
                thisHolder.x = mt.X;
                thisHolder.y = mt.Y;
            }
            // *************Adding mountainTile removes the initialization of  spawn manager and spawn tile and does not initalize mountain tile
            else
            {
                renderedTiles[i] = Instantiate(mountainTile, canvas) as GameObject;
                renderedTiles[i].AddComponent<Holder>();
                Holder thisHolder = renderedTiles[i].GetComponent<Holder>();
                thisHolder.tile = mt;
                thisHolder.map = map;
                thisHolder.x = mt.X;
                thisHolder.y = mt.Y;
            }
            manager.GetComponent<GameManager>().tiles[i] = renderedTiles[i];
            i++;
        }
        if (renderedTiles.Length > 1)
        { 
             RenderRow();
        }
    }

    //helper for render global map
    void RenderRow() {
        int t = 0;
        for(int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                renderedTiles[t].transform.Translate((j * 50), -(i * 50), 0);
                t++;
            }
        }
    }

}
