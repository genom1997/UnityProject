using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TerrainObject {
    int x, y;
    string type;

    public TerrainObject() {
        x = (int)UnityEngine.Random.value * 10;
        y = (int)UnityEngine.Random.value * 10;
        type = "unspecified";
    }
    public TerrainObject(int posx, int posy, string name) {
        x = posx;
        y = posy;
        type = name;
    }

    public string ty
    {
        get { return type; }
        set { type = value; }
    }
    public int X
    {
        get { return x; }
        set { x = value; }
    }
    public int Y
    {
        get { return y; }
        set { y = value; }
    }
}
