using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//creates the map, once it is created create the people and put them into their respective home tiles
public class MapGenerator : MonoBehaviour{
    int value;      //stores the size the map will be
    string mapName; //stores the name the map will have
    GameObject manager;
	// called at start of the scene
	void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager"); //set the manager reference
    }
    /// <summary>
    /// actually create the map by setting the name and value to what they are in the users UI. called on the button click
    /// </summary>
    public void CreateMapButton() {
        value = (int)gameObject.GetComponentInChildren<Slider>().value;
        mapName = gameObject.GetComponentInChildren<InputField>().text;
        CreateMap(value);
    }
    /// <summary>
    /// create a map based on the size passed in.
    /// </summary>
    /// <param name="size"> size to set the map to be </param>
    void CreateMap(int size) {
        Map map = new Map(value, mapName);
        manager.SendMessage("SetMap", map);
        //spawn a bunch of people that will exist in the map
        map.People = new Person[size * size];
        int i = 0;
        while (i < size*size) {
            Person p = new Person(map.mapWidth, map.mapLength);
            map.People[i] = p;
            i++;
        }
        //send those people to the home map tiles
        int j = 0;
        foreach (Person p in map.People) {
            // be a shitty developer and just initialize each array to hold max max number of people        /////////////This might be causing errors
            map.m[p.HomeX, p.HomeY].People = new Person[size * size];
            map.m[p.HomeX,p.HomeY].People[j] = p;
            j++;
        }
    }
}
