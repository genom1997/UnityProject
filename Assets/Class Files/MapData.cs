using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

[Serializable]
public class MapData{
    Map map;

    public MapData(Map m) {
        map = m;
    }

    public Map mapdata
    {
        get { return map; }
        set { map = value; }
    }
}
