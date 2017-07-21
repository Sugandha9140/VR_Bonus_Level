using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour {

	public float step = 0.5f;
	public float leftbound = 1f;
	public float rightbound = 1f;
	public float forwardbound = 2f;
	public float backwardbound = 2f;

	private GameObject playerPos;
	private Vector3 initialPos;

	// Use this for initialization
	void Start () {
		initialPos = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		playerPos = this.gameObject;

		if (Input.GetKey(KeyCode.A) == true && transform.position.x >= initialPos.x - leftbound){
			playerPos.transform.position = new Vector3 (playerPos.transform.position.x - step, playerPos.transform.position.y, playerPos.transform.position.z);
		}	

		if (Input.GetKey(KeyCode.D) == true && transform.position.x <= initialPos.x + rightbound) {
			playerPos.transform.position = new Vector3 (playerPos.transform.position.x + step, playerPos.transform.position.y, playerPos.transform.position.z);
		}

		if (Input.GetKey(KeyCode.W) == true && transform.position.z <= initialPos.z + forwardbound){
			playerPos.transform.position = new Vector3 (playerPos.transform.position.x, playerPos.transform.position.y, playerPos.transform.position.z + step);
		}	

		if (Input.GetKey(KeyCode.S) == true && transform.position.z >= initialPos.x - backwardbound) {
			playerPos.transform.position = new Vector3 (playerPos.transform.position.x, playerPos.transform.position.y, playerPos.transform.position.z - step);
		}
	}
}