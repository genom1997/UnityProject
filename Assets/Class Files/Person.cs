using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Person {
   // Person[] friends, enemies;
    int homeX, homeY;
    string name;

    public Person(int x, int y) {
        homeX = UnityEngine.Random.Range(0, x);
        homeY = UnityEngine.Random.Range(0, y);
        name = GenerateName(); 
    }

    private string GenerateName() {
        String[] names = {"Pete", "Steve", "Mike", "Ted", "Laverithus", "Chris", "Tim" , "Paul", "Marie", "Jessica", "Leah", "Ann", "Kate", "Alice", "Jane", "Bjkorchzwitine"};
        String[] lnames = { "Smith", "Fisher", "Steele", "Quathision IV"};
        return (names[(int)UnityEngine.Random.Range(0, 16)] + " " + lnames[(int)UnityEngine.Random.Range(0, 4)]);
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int HomeX
    {
        get { return homeX; }
        set { homeX = value; }
    }
    public int HomeY
    {
        get { return homeY; }
        set { homeY = value; }
    }
}
