using UnityEngine;
using System.Collections;

public class RayCastShootComplete : MonoBehaviour {

	public float fireRate = 0.25f;										                   // Seconds controlling how often the player can fire
	public int weaponRange = 50;										                   // Distance in Unity units over which the player can fire
	public Transform gunEnd;											                   // Refers to the muzzle location of the gun
	public float score = 0;                                                                // Score of the player: currently its a counter for number of objects shot
	public GameObject enemyManager;

	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);	                   // WaitForSeconds object used by the ShotEffect coroutine: time for which laser line will remain visible
	//private AudioSource gunAudio;										                   // Refers to the shooting sound effect audio source
	private LineRenderer laserLine;										                   // Refers to the LineRenderer component which will display our laserline
	private float nextFire;												                   // Store the time the player will be allowed to fire again, after firing
	private GameObject damaged;                                                            // Refers to the Game object shot by the weapon
                                                                            

	void Start () 
	{
		
		laserLine = GetComponent<LineRenderer>();                                          // Gets and stores a reference to the LineRenderer component
		//gunAudio = GetComponent<AudioSource>();                                          // Gets and stores a reference to the AudioSource component
		enemyManager = GameObject.FindGameObjectWithTag ("enemyholder");
	}

	

	void Update () 
	{
		// Checks if the player has pressed the fire button and if enough time has elapsed since they last fired
		if (Input.GetButtonDown("Fire1") && Time.time > nextFire) 
		{
			// Updates the time when player can fire next
			nextFire = Time.time + fireRate;

            StartCoroutine (ShotEffect());

            // Declares a raycast hit to store raycast information
            RaycastHit hit;

			// Sets the start position for laser at muzle
			laserLine.SetPosition (0, gunEnd.position);

			// Checks if raycast has hit anything
			if (Physics.Raycast (gunEnd.position, gunEnd.transform.forward, out hit, weaponRange))
			{
				// Sets the end position for laser 
				laserLine.SetPosition (1, hit.point);

				// Checks if the object hit is enemy
				if (hit.collider.tag == "enemy") 
				{
					damaged = hit.collider.gameObject;
					damaged.SetActive(false);
					enemyManager.GetComponent<NewEnemySpawning> ().enemyshot = true;
					score = score + 1;
					Debug.Log ("Hit! Score is: " + score);
				}
			}
			else
			{
				// If nothing hit, sets the endpoint of laser to a position directly in front of the gun at the distance of weaponRange
				laserLine.SetPosition (1, gunEnd.position + (gunEnd.forward * weaponRange));
			}
		}
	}

	private IEnumerator ShotEffect()
	{
		// Play the shooting sound effect
		//gunAudio.Play ();

		if (laserLine.enabled == false) {
		// Turn on our line renderer
			laserLine.enabled = true;
		}

		// Wait for .07 seconds
		yield return shotDuration;

		// Deactivate our line renderer after waiting
		laserLine.enabled = false;
	}

	public float GetScore()
	{
		return score;
	}
		
}