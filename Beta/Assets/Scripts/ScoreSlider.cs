using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour {

	public Slider scoreSlider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var Player = GameObject.FindGameObjectWithTag ("Player");
		//scoreSlider.value = Player.GetComponentInChildren<LaserShooting> ().GetScore ();
	}
}
