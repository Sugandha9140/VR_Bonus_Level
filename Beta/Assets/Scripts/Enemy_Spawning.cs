using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using BUtils;

public class Enemy_Spawning : MonoBehaviour {

	public float visualradius = 200f;                                                                      // Radius of the visual cone of the player
	public GameObject enemy; 
	public GameObject player;                                                                              
	GameObject enemyHolder;                                                                              
	public int enemysquadsize = 1;                                                                         // Refers to the number of enemies spawned per frame
	public float distanceRange = 50f;
	public float shieldRadius = 20f;
	public Vector3 direction;
	public float enemyinRange = 75f;

	// Use this for initialization
	void Start () {
		enemyHolder = GameObject.FindGameObjectWithTag ("enemyholder");                                    //Gets and stores reference to the GameObject Enemy_Manage
	}
	
	// Update is called once per frame
	void Update () {
		SpawnEnemies ();
	}


	void SpawnEnemies () {

		//Spawning enemies on the surface of the concave hemisphere in front of the player, whose radius is visualradius and the center is player's position

		for (int n = 0; n <= enemysquadsize; n++) {
			float spawn_x = Random.Range (-visualradius,visualradius);
			float spawn_y = Random.Range (-visualradius,visualradius);
			float spawn_z = Mathf.Sqrt(Mathf.Pow(visualradius,2)-Mathf.Pow(spawn_x,2) - Mathf.Pow(spawn_y,2));
			Vector3 instantiation_position = new Vector3 (player.transform.position.x + spawn_x, player.transform.position.y + spawn_y, player.transform.position.z + spawn_z);
			GameObject enemyClone = Instantiate (enemy, instantiation_position, enemy.transform.rotation, enemyHolder.transform);

			DirectionFinder (enemyClone);

			GetDirection ();
		}
	}

	//Giving each instantiated enemy a point on Player's shield to move towards	
	private Vector3 DirectionFinder(GameObject enemy) {
	
		float spawn_x2 = Random.Range (-distanceRange, distanceRange);

		if (spawn_x2 != Random.Range (-shieldRadius, shieldRadius)) {
			float spawn_y2 = Random.Range (-distanceRange, distanceRange);
			float spawn_z2 = Mathf.Sqrt (Mathf.Pow (distanceRange, 2) - Mathf.Pow (spawn_x2, 2) - Mathf.Pow (spawn_y2, 2));
			Vector3 pointOnShield = new Vector3 (player.transform.position.x + spawn_x2, player.transform.position.y + spawn_y2, player.transform.position.z + Mathf.Abs (spawn_z2));
			direction = (player.transform.position + pointOnShield) - enemy.transform.position;

			return direction;
		}

		else {
			DirectionFinder(enemy);
			return direction;
		}
	}

	public Vector3 GetDirection() {
		return direction;
	}
}