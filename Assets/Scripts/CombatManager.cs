using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public Person target;
    GameObject player, targetObj, canvas, localCanvas, manager;
    MapTile tile;
	// Use this for initialization
	void Start () {
     //   canvas = GameObject.FindGameObjectWithTag("Canvas");
        localCanvas = GameObject.FindGameObjectWithTag("LocalCanvas");
        manager = GameObject.FindGameObjectWithTag("Manager");
    }
	
	// Update is called once per frame
	void Update () {
        /*
        for (int i = 0; i < canvas.transform.childCount; i++) {
            if (canvas.transform.GetChild(i).GetComponent<Holder>().isActive) {
                tile = canvas.transform.GetChild(i).GetComponent<Holder>().tile;
            }
        }*/
        for (int i = 0; i < localCanvas.transform.childCount; i++) {
            try{
                if (localCanvas.transform.GetChild(i).GetComponent<UnitTileHolder>().isActive) {
                    player = localCanvas.transform.GetChild(i).gameObject;
                    break;
                } 
            }catch{
                Debug.LogWarning("Combat manager couldnt find player right now because of the button. Ignore this warning");
            }
        }
        int x = player.GetComponent<UnitTileHolder>().x;
        int y = player.GetComponent<UnitTileHolder>().y;
        int tilex = manager.GetComponent<GameManager>().currentTile.GetComponent<Holder>().x;
        int tiley = manager.GetComponent<GameManager>().currentTile.GetComponent<Holder>().x;
        int width = manager.GetComponent<GameManager>().map.m[tilex, tiley].Width;
        if (Input.GetButtonDown("AttackUp")) {
            Debug.Log("following tiles would be hit : " + (x - 1) + "," + y);//find a value of this to check if this thing is person, also add bounds checks
            int a = ((y * width) + (x-1)) + 1;
            if (localCanvas.transform.GetChild(a).gameObject.GetComponent<UnitTileHolder>().isPerson) {
                Debug.Log("Attack!!");
            }
        } else if (Input.GetButtonDown("AttackDown")) {
            //Debug.Log("Attack!!");
            Debug.Log("following tiles would be hit : " + (x + 1) + "," + y);
        }
        else if (Input.GetButtonDown("AttackLeft"))
        {
            //Debug.Log("Attack!!");
            Debug.Log("following tiles would be hit : " + (y - 1) + "," + x);
        }
        else if (Input.GetButtonDown("AttackRight"))
        {
            //Debug.Log("Attack!!");
            Debug.Log("following tiles would be hit : " + (y + 1) + "," + x);
        }
    }
}
