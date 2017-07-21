using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Other Scripts that are called by this script:
//1. 

//Other Scripts that call this script:
//1. NewEnemySpawning.cs
//2. LaserShooting.cs


public class Score_Manager : MonoBehaviour {

	public int scoreThreshold;
	public int scoreIncremental;
	public int levelUpScore;
	public bool scored = false;

	private int currentScore;
	private int level;
	private int totalLevels = 4;

	// Use this for initialization
	void Start () {
		scoreThreshold = 100;
		scoreIncremental = 10;
		currentScore = 0;
		level = 1;
	}
	
	// Update is called once per frame
	void Update () {
		LevelUpdate ();

		if (scored == true) {
			currentScore = currentScore + scoreIncremental;
			scored = false;
		}
	}

	public int LevelUpdate() {
		if (currentScore == scoreThreshold && level < totalLevels) {
			level = level + 1;
		}

		return level;
	}
}
