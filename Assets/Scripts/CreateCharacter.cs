using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour {
    int ath, intel, agil;
    string n;
    Slider[] values;
    InputField name;
    Player player;
    GameObject manager;

	// Use this for initialization
	void Start () {
        values = new Slider[3];
        values = gameObject.GetComponentsInChildren<Slider>();
        name = gameObject.GetComponentInChildren<InputField>();
        manager = GameObject.FindGameObjectWithTag("Manager");
    }
	
	// Update is called once per frame
	public void CreateChar () {
        ath = (int)values[0].value;
        intel = (int)values[1].value;
        agil = (int)values[2].value;
        n = name.text;
        player = new Player(n, ath, intel, agil);
        manager.GetComponent<GameManager>().player = player;
        Debug.Log("Name = " + n + " athe = " + ath + " intel = " + intel + " agil = " + agil);
    }
}
