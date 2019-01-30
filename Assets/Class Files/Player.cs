using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player {
    int athleticism, intelligence, agility;
    public string name;

    public Player(string n, int a, int i, int ag) {
        name = n;
        athleticism = a;
        intelligence = i;
        agility = ag;
    }

    public int Athleticism
    {
        get { return athleticism; }
        set { athleticism = value; }
    }
    public int Intelligence
    {
        get { return intelligence; }
        set { intelligence = value; }
    }
    public int Agility
    {
        get { return agility; }
        set { agility = value; }
    }
}
