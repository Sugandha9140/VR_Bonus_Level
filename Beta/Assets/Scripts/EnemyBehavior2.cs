using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Other Scripts that are called by this script:
//1. NewEnemySpawning.cs

//Other Scripts that call this script:
// 

public class EnemyBehavior2 : MonoBehaviour {

	public float speed = 25f;
	public GameObject player;
	public int smoothening = 5;
	public AudioSource arrival;

	Dictionary <GameObject, Vector3> MovementInfo;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		MovementInfo = GetComponent<NewEnemySpawning> ().GetEnemyInfo ();

		foreach (KeyValuePair<GameObject, Vector3> entry in MovementInfo) {
			GameObject enemyspaceship = entry.Key;
			Vector3 targetdestination = entry.Value;
			EnemyMovement (targetdestination, enemyspaceship);

			Quaternion lookRotation = Quaternion.LookRotation (player.transform.position - enemyspaceship.transform.position );
			enemyspaceship.transform.rotation = Quaternion.Slerp (enemyspaceship.transform.rotation, lookRotation, Time.deltaTime * smoothening);
		}
	}

	public void EnemyMovement(Vector3 targetposition, GameObject InMotion) {
		Vector3 currentpostion = InMotion.transform.position;
		float bounceStrength = 0.04f;
		float bounceSpeed = 4f;

		//Need to fix this conditional. Its hard-coded with the value 2f right now. Lower than that, the spaceships jerks
		if (Vector3.Distance (currentpostion, targetposition) >= 2f) {
			//Debug.Log (Vector3.Distance (currentpostion, targetposition));
			Vector3 direction = targetposition - currentpostion;
			direction.Normalize ();
			InMotion.transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, direction.z * speed * Time.deltaTime, Space.World);
		} 

		else {
			InMotion.transform.position = new Vector3 (InMotion.transform.position.x + 0,InMotion.transform.position.y + bounceStrength * (Mathf.Cos (Time.time * bounceSpeed)), InMotion.transform.position.z + 0);
		}
	}
}
