using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalMapRenderer : MonoBehaviour {
    GameObject canvas, localCanvas;
    public GameObject unitTile;
    GameObject[] tiles;
    Transform canvasTr;
    MapTile mapTile;
	// Use this for initialization
	void Awake () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        localCanvas = GameObject.FindGameObjectWithTag("LocalCanvas");
    }
 
    void Start() {
        localCanvas.SetActive(false);
    }
    //return back to the global map
    public void Return() {
        for (int k = 1; k < localCanvas.GetComponent<Transform>().childCount; k++)
        {
            Transform child = localCanvas.transform.GetChild(k);
            Destroy(child.gameObject);
        }
        localCanvas.SetActive(false);
        canvas.SetActive(true);
    }

    //render the specified maptile
    public void Render(MapTile mt) {
        mapTile = mt;
        canvas.SetActive(false);
        localCanvas.SetActive(true);
        canvasTr = localCanvas.GetComponent<Transform>();
        tiles = new GameObject[mt.Width * mt.Length];
        //place the player at their last known position, provided we have data about what the last player position was
        
        //create the tiles
        for (int k = 0; k < mt.Width * mt.Length; k++) {
            //instantiate all the spaces in the tile and add holders to them
            tiles[k] = Instantiate(unitTile, canvasTr) as GameObject;
            tiles[k].AddComponent<UnitTileHolder>();
            UnitTileHolder holder = tiles[k].GetComponent<UnitTileHolder>();
            holder.tile = mt;
        }
        //move the tiles into position
        int t = 0;
        for (int i = 0; i < mt.Length; i++)
        {
            for (int j = 0; j < mt.Width; j++)
            {
                //after the tiles are spawned move them right then down into a rectangle
                tiles[t].transform.Translate((j * 10), -(i * 10), 0);
                //also set the tiles order to correspond with its position
                tiles[t].GetComponent<UnitTileHolder>().x = j;
                tiles[t].GetComponent<UnitTileHolder>().y = i;
                //set the color based on their resource
                if (mt.environment.ty.Equals("forest"))
                {
                    Color forest = new Color(0f,0.6039f,0.2549f,1f);                        //***************COLOR VALUE FOR FOREST
                    tiles[t].GetComponent<Image>().color = forest;
                }
                else if (mt.environment.ty.Equals("plane"))
                {
                    Color plane = new Color(0.8627f,0.6784f,0f,1f);                         //***************COLOR VALUE FOR PLANE
                    tiles[t].GetComponent<Image>().color = plane;
                }
                else
                {
                    Color mountain = new Color(0.5f, 0.5f, 0.5f, 1);                        //***************COLOR VALUE FOR MOUNTAIN
                    tiles[t].GetComponent<Image>().color = mountain;
                }
                t++;
            }
        }
        //render the trees and fields AND SET THE HOLDERS TO KNOW THAT THERES RESOURCES HERE
        foreach (TerrainObject obj in mt.Objects) {
            int x = obj.X;
            int y = obj.Y;
            int a = ((y * mt.Width) + (x)) + 1;
            GameObject terrainObj = localCanvas.GetComponent<Transform>().GetChild(a).gameObject;
            if (mt.environment.ty.Equals("forest"))
            {
                terrainObj.GetComponent<Image>().color = new Color(0f, 0.4f, 0.2f, 1f);
                terrainObj.GetComponent<UnitTileHolder>().isResource = true;
            }
            else if (mt.environment.ty.Equals("plane"))
            {
                terrainObj.GetComponent<Image>().color = new Color(0.4f, 0.2f, 0f, 1f);
                terrainObj.GetComponent<UnitTileHolder>().isResource = true;
            }
            else
            {
                terrainObj.GetComponent<Image>().color = new Color(0.75f, 0.5f, 0.2f, 1);
                terrainObj.GetComponent<UnitTileHolder>().isResource = true;
            }
        }
        //render the people in the tile at random positions
        int count = 0;
        int h = 0;
        //for each person in the maptile
        if(mapTile.People != null)
        {
            foreach (Person p in mapTile.People)
            {
                if (mapTile.People[h] != null)
                    count++;
                h++;
            }
        }
        //place the people at random positions in the tile
        for (int i = 0; i < count; i++) {
            int x = Random.Range(0, mapTile.Width + 1);
            int y = Random.Range(0, mapTile.Length + 1);
            int a = ((y * mt.Width) + (x)) + 1;
            GameObject person = localCanvas.GetComponent<Transform>().GetChild(a).gameObject;
            person.GetComponent<Image>().color = new Color(0, 0, 0, 255);
            person.GetComponent<UnitTileHolder>().isPerson = true;
        }
    }
}
