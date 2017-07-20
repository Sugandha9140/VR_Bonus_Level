using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Other Scripts that are called by this script:
//1. 

//Other Scripts that call this script:
//1. Enemy_Laser_Shooting.cs 


public class health : MonoBehaviour
{

	public int startingHealth;
	public int currentHealth;
	public Slider healthSlider;
	public int damage = 1;

	public Sprite game_over;
	public bool isDead = false;
	public bool wasHit = false;

	// Use this for initialization
	void Awake ()
	{
		currentHealth = startingHealth;
	}

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (wasHit == true) {
			TakeDamage ();
			wasHit = false;
		}
	}
		
	public void TakeDamage ()
	{
		// Reduce the current health by the damage amount.
		currentHealth = currentHealth - damage;

		// Set the health bar's value to the current health.
		//healthSlider.value = currentHealth;

		// Play the hurt sound effect.
		//	playerAudio.Play ();

		if (currentHealth <= 0 && !isDead) {
			// ... it should die.
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;
		//Debug.Log ("Death");
		Instantiate (game_over); 
		DestroyObject (gameObject);

	}

	public float GetHealth()
	{
		return currentHealth;
	}

}
