using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour {
    public Slider HealthBar;
    public Slider ExperienceBar;
    GameManager manager;
    Player player;
    public int maxHealth, health, attack, attackSpeed, spellpower, experience, maxExperience;
	// ******THIS GOES ON GAME MANAGER, MAKE SURE IT ONLY GETS CALLED AFTER PLAYER IS ASSIGNED *******
	void Start () {
        manager = gameObject.GetComponent<GameManager>();
        player = manager.player;
        health = 100;
        maxHealth = 100;
        attack = player.Athleticism * 10;
        attackSpeed = player.Agility;
        spellpower = player.Intelligence * 10;
        experience = 0;
	}
	
	// Update is called once per frame
	public void hit (int damage) {
        if (health - damage <= 0)
        {
            Debug.Log("DEATH IS UPON YOU");
        }
        else
        {
            health -= damage;
        }
	}
    public void heal(int amount) {
        if (health + amount > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amount;
        }
    }
    public void gainExp(int amount) {
        if (experience + amount > maxExperience)
        {
            Debug.Log("LEVEL UP");
        }
        else
        {
            experience += amount;
        }
    }
}
