using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
    GameObject manager, localCanvas, canvas, up, down, left, right, localRenderer;
    public GameObject current;
    bool moveUp, moveDown, moveLeft, moveRight, travelUp, travelDown, travelLeft, travelRight, isAtPlayer;
    Color background;
    MapTile tile;
    Map map;
    public Vector2 pos;
    int x, y, maxMapWidth, maxMapLength;
    // Use this for initialization
    void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        localCanvas = GameObject.FindGameObjectWithTag("LocalCanvas");
        map = manager.GetComponent<GameManager>().map;
        maxMapLength = map.mapLength;
        maxMapWidth = map.mapWidth;
        isAtPlayer = false;
        localRenderer = GameObject.Find("Local Map Renderer");
    }
    //tile we are in recieved from holder when clicked
    public void SetTile(MapTile t) {
        tile = t;
    }
    //position of where the player spawned recieved from the spawner
    public void SetPos(Vector2 p) {
        pos = p;
        x = (int)pos.x;
        y = (int)pos.y;
    }
    public void SetSpawn(int caseName) {
        if(caseName == 0)
            isAtPlayer = false;        
        else if(caseName == 1)
            isAtPlayer = true;
    }
	// Update is called once per frame
	void Update () {
        //if we are looking at the global map, we cant move
        if (canvas.activeSelf)
        {
            Debug.Log("cannot move while looking at map");
        }
        //if we are looking at the player we can move them
        else if(manager.GetComponent<GameManager>().currentTile.GetComponent<Holder>().x == tile.X && manager.GetComponent<GameManager>().currentTile.GetComponent<Holder>().y == tile.Y)
        {
            //width and length will be 1 more than max x and y positions.
            //get the gameobject by child index based on position of spawned player. index i determined by formula below. +1 is added to accound for the return button
            int a = ((y * tile.Width) + (x)) + 1;
            //set the position of the current player, call the coloration now so that it displays where you are before any move commands run
            current = localCanvas.GetComponent<Transform>().GetChild(a).gameObject;
            current.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            Debug.Log("current POS: (" + current.GetComponent<UnitTileHolder>().x + " , " + current.GetComponent<UnitTileHolder>().y + ")");

            //index movemnet limitation formulas
            if (a >= tile.Width) {
                up = localCanvas.GetComponent<Transform>().GetChild(a - tile.Width).gameObject;
                moveUp = true;
                travelUp = false;
            }
            else {
                moveUp = false;
                if(tile.X != 0){
                    travelUp = true;
                    Debug.Log("travel up = " + travelUp);
                }
            }
            if (a < ((tile.Length - 1) * tile.Width))
            {
                down = localCanvas.GetComponent<Transform>().GetChild(a + tile.Width).gameObject;
                moveDown = true;
                travelDown = false;
            } else {
                moveDown = false;
                if (tile.X < maxMapLength - 1) {
                    travelDown = true;
                    Debug.Log("travel down = " + travelDown);
                } 
            }
            if (a % tile.Width != 1) {
                left = localCanvas.GetComponent<Transform>().GetChild(a - 1).gameObject;
                moveLeft = true;
                travelLeft = false;
            }
            else {
                moveLeft = false;
                if (tile.Y != 0)
                {
                    travelLeft = true;
                    Debug.Log("travel left = " + travelLeft);
                }
            }
            if (a % tile.Width != 0) {
                right = localCanvas.GetComponent<Transform>().GetChild(a + 1).gameObject;
                moveRight = true;
                travelRight = false;
            }
            else {
                moveRight = false;
                if (tile.Y < maxMapWidth - 1)
                {
                    travelRight = true;
                    Debug.Log("travel right = " + travelRight);
                }
            }

            //find out what the background environment color is to know what the change the tile to when the player leaves
            if (tile.environment.ty.Equals("forest"))
            {
                background = new Color(0f, 0.6039f, 0.2549f, 1f);
            }
            else if (tile.environment.ty.Equals("plane"))
            {
                background = new Color(0.8627f, 0.6784f, 0f, 1f);
            }
            else
            {
                background = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            //actually move the player and set the corresponding isActive values on the holder
            if (Input.GetButtonDown("Left") && moveLeft && !(left.GetComponent<UnitTileHolder>().isResource) && !(left.GetComponent<UnitTileHolder>().isPerson))
            {
                current.GetComponent<Image>().color = background;
                current.GetComponent<UnitTileHolder>().isActive = false;
                left.GetComponent<Image>().color = Color.white;
                left.GetComponent<UnitTileHolder>().isActive = true;
                //reset the update loop with the coordinates of your new position
                x = left.GetComponent<UnitTileHolder>().x;
                y = left.GetComponent<UnitTileHolder>().y;
            }
            else if (Input.GetButtonDown("Right") && moveRight && !(right.GetComponent<UnitTileHolder>().isResource) && !(right.GetComponent<UnitTileHolder>().isPerson))
            {
                current.GetComponent<Image>().color = background;
                current.GetComponent<UnitTileHolder>().isActive = false;
                right.GetComponent<Image>().color = Color.white;
                right.GetComponent<UnitTileHolder>().isActive = true;
                //reset the update loop with the coordinates of your new position
                x = right.GetComponent<UnitTileHolder>().x;
                y = right.GetComponent<UnitTileHolder>().y;
            }
            else if (Input.GetButtonDown("Up") && moveUp && !(up.GetComponent<UnitTileHolder>().isResource) && !(up.GetComponent<UnitTileHolder>().isPerson))
            {
                current.GetComponent<Image>().color = background;
                current.GetComponent<UnitTileHolder>().isActive = false;
                up.GetComponent<Image>().color = Color.white;
                up.GetComponent<UnitTileHolder>().isActive = true;
                //reset the update loop with the coordinates of your new position
                x = up.GetComponent<UnitTileHolder>().x;
                y = up.GetComponent<UnitTileHolder>().y;
            }
            else if (Input.GetButtonDown("Down") && moveDown && !(down.GetComponent<UnitTileHolder>().isResource) && !(down.GetComponent<UnitTileHolder>().isPerson))
            {
                current.GetComponent<Image>().color = background;
                current.GetComponent<UnitTileHolder>().isActive = false;
                down.GetComponent<Image>().color = Color.white;
                down.GetComponent<UnitTileHolder>().isActive = true;
                //reset the update loop with the coordinates of your new position
                x = down.GetComponent<UnitTileHolder>().x;
                y = down.GetComponent<UnitTileHolder>().y;
            }
            //control moving the player into a different area if they are on the boarder
            else if (Input.GetButtonDown("Down") && travelDown && !moveDown)
            {
                int x = tile.X;
                int y = tile.Y;
                MapTile moveTile = map.m[x +1, y];     //reference the tile to move to
                a = ((moveTile.X * map.mapLength) + (moveTile.Y)); //formula here is for glabal map tiles so there is no + 1 for the button
                int b = ((x * map.mapLength) + (y));
                //Vector2 newPos = new Vector2(0, current.GetComponent<UnitTileHolder>().y);    //set position to be at in tile to enter. here you will start where you went down but at the very top of the tile below
                Vector2 newPos = new Vector2(0, 0);
                SetPos(newPos);
                manager.GetComponent<GameManager>().SetCurrentTile(manager.GetComponent<GameManager>().tiles[a]); //set the tile were moving to as active, the manager will handle unsetting active and changing color
                SetTile(moveTile);  //set this tile in movement controller to be the tile were moving to so we can move while looking at that tile 
                localRenderer.GetComponent<LocalMapRenderer>().Return();    //remove tile currently being rendered
                localRenderer.GetComponent<LocalMapRenderer>().Render(moveTile);    //render tile weve moved to
            }
            else if (Input.GetButtonDown("Left") && travelLeft && !moveLeft)
            {
                int x = tile.X;
                int y = tile.Y;
                MapTile moveTile = map.m[x, y - 1];     //reference the tile to move to
                a = ((moveTile.X * map.mapLength) + (moveTile.Y)); //formula here is for glabal map tiles so there is no + 1 for the button
                int b = ((x * map.mapLength) + (y));
                //   Vector2 newPos = new Vector2(current.GetComponent<UnitTileHolder>().x, moveTile.Width);    //set position to be at in tile to enter. here you will start where you went to the left but at the very right of the tile to the left
                Vector2 newPos = new Vector2(0, 0);
                SetPos(newPos);
                manager.GetComponent<GameManager>().SetCurrentTile(manager.GetComponent<GameManager>().tiles[a]); //set the tile were moving to as active, the manager will handle unsetting active and changing color
                SetTile(moveTile);  //set this tile in movement controller to be the tile were moving to so we can move while looking at that tile 
                localRenderer.GetComponent<LocalMapRenderer>().Return();    //remove tile currently being rendered
                localRenderer.GetComponent<LocalMapRenderer>().Render(moveTile);    //render tile weve moved to
            }
            else if (Input.GetButtonDown("Right") && travelRight && !moveRight)
            {
                int x = tile.X;
                int y = tile.Y;
                MapTile moveTile = map.m[x, y + 1];     //reference the tile to move to
                a = ((moveTile.X * map.mapLength) + (moveTile.Y)); //formula here is for glabal map tiles so there is no + 1 for the button
                int b = ((x * map.mapLength) + (y));
                //  Vector2 newPos = new Vector2(current.GetComponent<UnitTileHolder>().x, 0);    //set position to be at in tile to enter. here you will start where you went to the right but at the very left of the tile to the right
                Vector2 newPos = new Vector2(0, 0);
                SetPos(newPos);
                manager.GetComponent<GameManager>().SetCurrentTile(manager.GetComponent<GameManager>().tiles[a]); //set the tile were moving to as active, the manager will handle unsetting active and changing color
                SetTile(moveTile);  //set this tile in movement controller to be the tile were moving to so we can move while looking at that tile 
                localRenderer.GetComponent<LocalMapRenderer>().Return();    //remove tile currently being rendered
                localRenderer.GetComponent<LocalMapRenderer>().Render(moveTile);    //render tile weve moved to
            }
            else if (Input.GetButtonDown("Up") && travelUp && !moveUp)
            {
                int x = tile.X;
                int y = tile.Y;
                MapTile moveTile = map.m[x - 1, y];     //reference the tile to move to
                a = ((moveTile.X * map.mapLength) + (moveTile.Y)); //formula here is for glabal map tiles so there is no + 1 for the button
                int b = ((x * map.mapLength) + (y));
                // Vector2 newPos = new Vector2(moveTile.Length, current.GetComponent<UnitTileHolder>().y);    //(logic is a little messed up but okay for now)set position to be at in tile to enter. here you will start where you went up but at the bottom of the tile above you
                Vector2 newPos = new Vector2(0, 0);
                SetPos(newPos);
                manager.GetComponent<GameManager>().SetCurrentTile(manager.GetComponent<GameManager>().tiles[a]); //set the tile were moving to as active, the manager will handle unsetting active and changing color
                SetTile(moveTile);  //set this tile in movement controller to be the tile were moving to so we can move while looking at that tile 
                localRenderer.GetComponent<LocalMapRenderer>().Return();    //remove tile currently being rendered
                localRenderer.GetComponent<LocalMapRenderer>().Render(moveTile);    //render tile weve moved to
            }



        }
    }
}
