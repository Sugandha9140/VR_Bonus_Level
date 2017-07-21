using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour {

	// Use this for initialization
	public Slider healthSlider;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		var Player = GameObject.FindGameObjectWithTag ("PlayerBody");
		healthSlider.value = Player.GetComponentInChildren<health> ().GetHealth ();
	}
}
