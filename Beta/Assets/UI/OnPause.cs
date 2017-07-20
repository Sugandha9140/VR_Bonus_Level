using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPause : MonoBehaviour
{
	public GameObject CanvasMenu;
	public GameObject CanvasScore;

	// Use this for initialization
	void Start ()
	{
		CanvasMenu.GetComponent<Canvas> ().enabled = false; 
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown ("escape"))
			Pause ();
	}

	void Pause ()
	{
		CanvasMenu.GetComponent<Canvas> ().enabled = true; 
		Time.timeScale = 0;
	}

	public void unPause ()
	{
		Time.timeScale = 1f;	
		CanvasMenu.GetComponent<Canvas> ().enabled = false; 

	}
	public void Scoring (){
		CanvasMenu.GetComponent<Canvas> ().enabled = false; 
		CanvasScore.GetComponent<Canvas> ().enabled = true;
	}

	public void Exit(){
		Application.Quit ();
	}
}
