using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//abstract map class that can be saved
[Serializable]
public class Map
{
    string Name;
    MapTile[,] map;
    int x, y;
    Person[] people;

    public Map(int size, string name)
    {
        map = new MapTile[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                map[i, j] = new MapTile();
                map[i, j].X = i;
                map[i, j].Y = j;
            }
        }
        x = size;
        y = size;
        Name = name;
        int total = (int)UnityEngine.Random.value * (size * size) + 1;
    }
    public Person[] People
    {
        get { return people; }
        set { people = value; }
    }
    public string mapName
    {
        get { return Name; }
        set { Name = value; }
    }
    public int mapWidth
    {
        get { return x; }
        set { x = value; }
    }
    public int mapLength
    {
        get { return y; }
        set { y = value; }
    }
    public MapTile[,] m
    {
        get { return map; }
        set { map = value; }
    }
    public MapTile tile(int i, int j) {
        return map[i,j];
    }
    public string mapProperties()
    {
        string description = "";
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                description += map[i, j].mapTileProperties();
            }
        }
                return ("Description = " + description + "\n Name = " + Name );
    }
}