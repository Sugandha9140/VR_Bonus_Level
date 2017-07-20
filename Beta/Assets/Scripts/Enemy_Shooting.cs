using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : MonoBehaviour
{
	public GameObject player;                                                              // Refers to the GameObject that contains the first person character of the player
	public GameObject weaponEnd1;                                                          // Refers to the first muzzle of enemy gun
	public GameObject weaponEnd2;                                                          // Refers to the second muzzle of enemy gun
	public int weaponRange = 50;                                                           // Distance in Unity units over which the enemy can fire
	public float timeGap = 5f;                                                             // Time betwwen each fire
	
	private float NextFire;                                                                // Store the time the enemy will fire to fire again, after previous fire
	private WaitForSeconds shotDuration = new WaitForSeconds(0.08f);	                   // WaitForSeconds object used by the ShotEffect coroutine: time for which laser line will remain visible
	private LineRenderer enemylaser;				       						           // Refers to the LineRenderer component which will display our laserline
	int alternate = 0;                                                                     // Variable used to alternate between the two muzzles
	int firepower = 2;                                                                     // Firepower of each enemy
	GameObject enemySpaceship; 

	// Use this for initialization
	void Start ()
	{
		enemySpaceship = this.gameObject;
		enemylaser = GetComponent<LineRenderer>();                                         // Gets and stores a reference to the LineRenderer component
		NextFire = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > NextFire + timeGap) {
			NextFire = NextFire + timeGap;
			alternate = (alternate + 1) % firepower;
			if (alternate == 0) {
				Shoot (weaponEnd1);
			} 
			else {
				Shoot(weaponEnd2);
			}
		}
	}

	public void Shoot (GameObject weaponEnd)
	{
		StartCoroutine (ShotEffect());

		// Declares a raycast hit to store raycast information
		// RaycastHit hit;

		// Sets the start position for laser at muzle
		// enemylaser.SetPosition (0, weaponEnd.transform.position);
		// Vector3 laserEnd = GetComponentInParent<NewEnemySpawning> ().DirectionFinder (enemySpaceship);
		// enemylaser.SetPosition (1, weaponEnd.transform.position - (laserEnd * weaponRange) );

		// Checks if raycast has hit anything
		// if (Physics.Raycast (weaponEnd.transform.position, laserEnd, out hit, weaponRange))
		// {
			//Checks if the object hit is player
			//if (hit.collider == player) 
			//{
				//Debug.Log("Player Hit!");
				//player.GetComponent<health> ().hit = true;
			//}
		//}
	}

	private IEnumerator ShotEffect()
	{
		// Play the shooting sound effect
		// gunAudio.Play ();

		if (enemylaser.enabled == false)
		{
			// Turn on our line renderer
			enemylaser.enabled = true;
		}
		// Wait for .07 seconds
		yield return shotDuration;
		// Deactivate our line renderer after waiting
		enemylaser.enabled = false;
	}
}
