using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapTile
{
    int width;
    int length;
    int x;
    int y;
    Environment env;
    TerrainObject[] objects;
    Person[] people;
    
    
    //be careful, this implimentation allows for objects to appear in the same spot, however unlikely it is. wont cause errors tho because renderer just sets colors.
    /// <summary>
    /// Create a MapTile with people, and x,y coordinate, length, width, objects, and environment type
    /// </summary>
    public MapTile()
    {
        width = (int)((UnityEngine.Random.value * 10) + 10);
        length = (int)((UnityEngine.Random.value * 10) + 10);
        env = new Environment();
        int total = env.value;
        objects = new TerrainObject[total];
        for (int i = 0; i < total; i++) {
            if(env.ty.Equals("forest"))
                objects[i] = new TerrainObject(UnityEngine.Random.Range(0,width), UnityEngine.Random.Range(0, length), "tree");
            if(env.ty.Equals("plane"))
                objects[i] = new TerrainObject(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, length), "wheatfield");
            if (env.ty.Equals("mountain"))
                objects[i] = new TerrainObject(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, length), "ore");
        }
    }
    /// <summary>
    /// Array containing the people in this tile
    /// </summary>
    public Person[] People
    {
        get { return people; }
        set { people = value; }
    }
    /// <summary>
    /// Array containing the environment objects
    /// </summary>
    public TerrainObject[] Objects {
        get { return objects; }
        set { objects = value; }
    }
    /// <summary>
    /// X position of the mapTile
    /// </summary>
    public int X
    {
        get { return x; }
        set { x = value; }
    }
    /// <summary>
    /// Y position of the mapTile
    /// </summary>
    public int Y
    {
        get { return y; }
        set { y = value; }
    }
    /// <summary>
    /// Width of the maptile
    /// </summary>
    public int Width
    {
        get { return width; }
        set { width = value; }
    }
    /// <summary>
    /// Length of the maptile
    /// </summary>
    public int Length
    {
        get { return length; }
        set { length = value; }
    }
    /// <summary>
    /// Environment object that defines the environment type of the maptile
    /// </summary>
    public Environment environment
    {
        get { return env; }
        set { env = value; }
    }
    /// <summary>
    /// Info function that displays the properties of the maptile
    /// </summary>
    /// <returns>Returns a string containing info on the maptile</returns>
    public string mapTileProperties()
    {
        return ("width = " + width + " length = " + length + " position = " + x + "," + y + " Environment = " + env.environmentProperties());
    }
}

