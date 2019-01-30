using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gamemanager object that persists accross scenes and stores the map and player objects.
/// </summary>
public class GameManager : MonoBehaviour {
    public Map map;             //stores the map
    public Player player;       //stores the player
    public GameObject[] tiles;  //stores all the map tiles in the map
    public GameObject currentTile; //stores reference to tile player is currently in
    /// <summary>
    /// Call the DontDestroyOnLoad on the gameobject this is attached to.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// Called every frame, ensures that the current tile stored is the only one active and that the white color value for that is set accordingly
    /// </summary>
    void Update()
    {
        if (currentTile != null)
        {
            foreach (GameObject tile in tiles) {
                if (tile.GetComponent<Holder>().x == currentTile.GetComponent<Holder>().x && tile.GetComponent<Holder>().y == currentTile.GetComponent<Holder>().y)
                {
                    tile.GetComponent<Holder>().isActive = true;
                    tile.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    tile.GetComponent<Holder>().isActive = false;
                    if (tile.GetComponent<Holder>().tile.environment.ty.Equals("forest"))
                    {
                        tile.GetComponent<Image>().color = new Color(0f, 0.6039f, 0.2549f, 1f); 
                    }
                    else if (tile.GetComponent<Holder>().tile.environment.ty.Equals("plane"))
                    {
                        tile.GetComponent<Image>().color = new Color(0.8627f, 0.6784f, 0f, 1f);
                    }
                    else if (tile.GetComponent<Holder>().tile.environment.ty.Equals("mountain"))
                    {
                        tile.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Set the map variable in this script to be specified map. The tiles gameobject array
    /// is initialized to be the max size of the map so that it can hold all maptiles. (does not set the 
    /// tiles)
    /// </summary>
    /// <param name="m"> The map to set the gamemanagers map to </param>
    void SetMap(Map m) {
        map = m;
        tiles = new GameObject[map.mapLength * map.mapWidth];
    }
    /// <summary>
    /// Sets the gamemanagers player variable to the specified player.
    /// </summary>
    /// <param name="p"> player to set the gamemanagers player variable to </param>
    void SetPlayer(Player p) {
        player = p;
    }
    /// <summary>
    /// function that will display info on the map stored in the gamemanager.
    /// includes info on the maps size, maptile data, and all AI people and where theyll live
    /// </summary>
    public void MapInfo() {
        if (map != null)
        {
            Debug.Log(map.mapProperties());
            Person[] people = map.People;
            foreach (Person p in people)
            {
                Debug.Log(p.Name + " lives at tile : (" + p.HomeX + "," + p.HomeY + ")");
            }
        }
        else {
            Debug.Log("No map has been found");
        }
    }

    /// <summary>
    /// Can be called with parameter tile from any source to set the current tile
    /// </summary>
    /// <param name="tile">tile to set as the current tile</param>
    public void SetCurrentTile(GameObject tile) {
        currentTile = tile;
    }
}
