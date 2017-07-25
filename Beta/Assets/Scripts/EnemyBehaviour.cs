using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public float speed = 25f;
	//public GameObject target;
	Vector3 direction;
	GameObject enemySpaceship;


	// Use this for initialization
	void Start () {
		enemySpaceship = GameObject.FindGameObjectWithTag ("enemy");
		//direction = GetComponentInParent<NewEnemySpawning> ().DestinationFinder (enemySpaceship);
		//direction = target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != direction) {
			transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
		}
	}
}
