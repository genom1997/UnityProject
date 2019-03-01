using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitTileHolder : MonoBehaviour {
    public int x, y;
    public bool isSpawn;
    public bool isActive;
    public bool isResource;
    public bool isPerson;
    GameObject manager;
    public MapTile tile;
    public Map map;
    
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("Manager");
        map = manager.GetComponent<GameManager>().map;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
