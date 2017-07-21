using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Other Scripts that are called by this script:
//1. 

//Other Scripts that call this script:
//1. LaserShooting.cs 


public class NewEnemySpawning : MonoBehaviour {

	public float visualradius = 100f; 
	public float shieldRadius = 20f;
	public float timeGap = 30f;
	public GameObject enemyBasic; 
	public GameObject enemyAdvanced;
	public GameObject enemyholder;
	public GameObject player;                                                                              
	public int minEnemies = 1;
	public int maxEnemies = 3;
	public int activeEnemies = 1;
	public bool enemyshot = false;
	public Dictionary<GameObject, Vector3> spawnedEnemyInfos = new Dictionary<GameObject,Vector3>();

	private int enemysquadsize; 
	private int survivingEnemies;
	private GameObject gameManager;
	private float nextRare;


	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		survivingEnemies = 0;
		nextRare = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		survivingEnemies = TrackEnemies ();
		enemysquadsize = Random.Range (minEnemies, maxEnemies);
		if (survivingEnemies < activeEnemies) {
			if (gameManager.GetComponent<Score_Manager> ().LevelUpdate () == 1) {
				SpawnBasicEnemies ();
				survivingEnemies = survivingEnemies + enemysquadsize;
			} 
			else {
				SpawnBasicEnemies ();
				survivingEnemies = survivingEnemies + enemysquadsize;

				if (Time.time > nextRare + timeGap) {
					SpawnAdvancedEnemies ();
					nextRare = nextRare + timeGap;
				}
			}
		} 
	}

	public void SpawnBasicEnemies () {

		for (int n = 1; n <= enemysquadsize; n++) {
			float spawn_x = Random.Range (-visualradius, visualradius);
			float spawn_y = Random.Range (0, visualradius);
			float spawn_z = visualradius;
			Vector3 instantiation_position = new Vector3 (spawn_x, spawn_y, spawn_z);
				
			Vector3 destination = DestinationFinder ();
				
			GameObject enemyClone = Instantiate (enemyBasic, instantiation_position, enemyBasic.transform.rotation,enemyholder.transform);

			spawnedEnemyInfos.Add (enemyClone, destination);
		}
		GetEnemyInfo();
	}

	public void SpawnAdvancedEnemies () {

		float spawn_x = Random.Range (-visualradius, visualradius);
		float spawn_y = Random.Range (0, visualradius);
		float spawn_z = visualradius;
		Vector3 instantiation_position = new Vector3 (spawn_x, spawn_y, spawn_z);

		Vector3 destination = DestinationFinder ();

		GameObject Advanced = Instantiate (enemyAdvanced, instantiation_position, enemyBasic.transform.rotation,enemyholder.transform);

		spawnedEnemyInfos.Add (Advanced, destination);

		GetEnemyInfo();
	}

	//Giving each instantiated enemy a point on Player's shield to move towards	
	public Vector3 DestinationFinder() {

		float spawn_x2 = Random.Range (-shieldRadius, shieldRadius);
		float spawn_y2 = Random.Range (0, shieldRadius);
		float spawn_z2 = Mathf.Sqrt(Mathf.Abs(Mathf.Pow (shieldRadius, 2) - Mathf.Pow (spawn_x2, 2) - Mathf.Pow (spawn_y2, 2)));
		Vector3 pointOnshield = new Vector3 (spawn_x2, spawn_y2, spawn_z2);

		return pointOnshield;
	}

	private int TrackEnemies() {
		int leftover = survivingEnemies;

		if (enemyshot == true) {
			leftover = leftover - 1;
			enemyshot = false;
		}

		return leftover;
	}

	public Dictionary<GameObject,Vector3> GetEnemyInfo() {
		return spawnedEnemyInfos;
	}
}
