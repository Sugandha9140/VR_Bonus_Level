using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects_Manager : MonoBehaviour {

	[Header("Beam Prefabs")]
	public GameObject[] beamLineRendererPrefab;
	public GameObject[] beamStartPrefab;
	public GameObject[] beamEndPrefab;

	[Header("Adjustable Variables")]

	public float beamEndOffset = 1f;                                                       // How far from the raycast hit point the end effect is positioned
	public float textureScrollSpeed = 8f;                                                  // How fast the texture scrolls along the beam
	public float textureLengthScale = 3f;                                                  // Length of the beam texture

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
