using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Environment {
    string type;
    int trait;
    /// <summary>
    /// Constructor for environment object. 33.3% chance created with forest, 33.3% chance created with plane, 33.4% chance to create a mountain.
    /// </summary>
    public Environment() {
        if (UnityEngine.Random.value <= 0.333)
        {
            type = "forest";
        }
        else if (UnityEngine.Random.value >= .666)
        {
            type = "plane";
        }
        else
        {
            type = "mountain";
        }
        trait = (int)(UnityEngine.Random.value * 10);
    }
    /// <summary>
    /// type of environment (string)
    /// </summary>
    public string ty
    {
        get { return type; }
        set { type = value; }
    }
    /// <summary>
    /// how strong density the environment has
    /// </summary>
    public int value {
        get { return trait; }
        set { trait = value; }
    }
    /// <summary>
    /// properties of the environment
    /// </summary>
    /// <returns>a string of the environments properties</returns>
    public string environmentProperties() {
        return (type + " with " + trait + " much " + type + "ness \n");
    }
}
