using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Handles saving data locally and also loading previously saved data into the game
/// </summary>
public class LoadSave : MonoBehaviour {
    GameObject manager;     //stores reference to the gamemanager object

    //called when the scene starts
    public void Start() {
        manager = GameObject.FindGameObjectWithTag("Manager");  //set the variable of the manager object to hold the game manager. found with its tag.
    }

    /// <summary>
    /// save the map stored in the manager. not called until the button calls this function
    /// </summary>
    public void SaveMapCall() {
        Map m = manager.GetComponent<GameManager>().map;
        SaveMap(m);
    }
    /// <summary>
    /// save the player stored in the gamemanager. not called until the button calls this funtion
    /// </summary>
    public void SaveCharCall() {
        Player p = manager.GetComponent<GameManager>().player;
        SaveChar(p);
    }
    /// <summary>
    /// Saves the specified player to local machine
    /// </summary>
    /// <param name="p">player to save to local machine</param>
    void SaveChar(Player p) {
        BinaryFormatter bf = new BinaryFormatter(); //create the object that will write the data
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");  //create the data file in the default save path
   
        bf.Serialize(file, p);      //write the players data in a binary file i think stored at the location above
        file.Close();   //close the file

        Debug.Log(p.name + " Has been saved");
    }
    /// <summary>
    /// Load the player data stored locally
    /// </summary>
    public void LoadChar() {
        //if the default save path has player data in it
        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
        {
            //read the file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);

            Player play = (Player)bf.Deserialize(file);
            file.Close();
            //call the setplayer message on the gamemanger with the paramater of the one stored locally
            manager.SendMessage("SetPlayer", play);
            Debug.Log("Loaded " + play.name);
        }
    }
    /// <summary>
    /// save the map data locally
    /// </summary>
    /// <param name="m"> map to save onto this machine </param>
    void SaveMap(Map m)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MapData.dat");

        MapData data = new MapData(m);
        bf.Serialize(file, data);
        file.Close();

        Debug.Log(m.mapName + " Has been saved");
    }
    /// <summary>
    /// load the map stored locally
    /// </summary>
    public void LoadMap()
    {
        if (File.Exists(Application.persistentDataPath + "/MapData.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MapData.dat", FileMode.Open);

            MapData data = (MapData)bf.Deserialize(file);
            file.Close();

            manager.SendMessage("SetMap", data.mapdata);
            Debug.Log("Loaded " + data.mapdata.mapName);
        }
    }
}
