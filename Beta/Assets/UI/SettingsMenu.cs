using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
	public GameObject CanvasScore;
	public GameObject CanvasMenu;
	// Use this for initialization
	void Start () {
		CanvasScore.GetComponent<Canvas> ().enabled = false; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Back() {
		CanvasMenu.GetComponent<Canvas> ().enabled = true; 
		CanvasScore.GetComponent<Canvas> ().enabled = false;
	}
}
