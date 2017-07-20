using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Other Scripts that are called by this script:
//1. health.cs

//Other Scripts that call this script:
// 


public class Enemy_Laser_Shooting : MonoBehaviour
{
	[Header("Beam Prefabs")]
	public GameObject[] beamLineRendererPrefab;
	public GameObject[] beamStartPrefab;
	public GameObject[] beamEndPrefab;

	[Header("Adjustable Variables")]

	public float beamEndOffset = 1f;                                                       // How far from the raycast hit point the end effect is positioned
	public float textureScrollSpeed = 8f;                                                  // How fast the texture scrolls along the beam
	public float textureLengthScale = 3f;                                                  // Length of the beam texture

	public float weaponRange = 0.5f;                                                       // Distance in Unity units over which the enemy can fire
	public float timeGap = 5f;                                                             // Time betwwen each fire

	private int currentBeam = 0;

	private GameObject beamStart;
	private GameObject beamEnd;
	private GameObject beam;
	private LineRenderer line;
	private GameObject target;                                                             // Refers to the first person character model GameObject

	private float NextFire;                                                                // Store the time the enemy will fire to fire again, after previous fire
	private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);	                       // WaitForSeconds object used by the ShotEffect coroutine: time for which laser line will remain visible
	GameObject enemySpaceship; 
	Vector3 targetDirection;
	bool spaceshipActive;

	// Use this for initialization
	void Start ()
	{
		target = GameObject.FindGameObjectWithTag("PlayerBody");
		enemySpaceship = this.gameObject;
		NextFire = Time.time;
		targetDirection = target.transform.position - enemySpaceship.transform.position;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > NextFire + timeGap) {
			NextFire = NextFire + timeGap;
			 
			beamStart = Instantiate (beamStartPrefab [currentBeam], new Vector3 (0, 0, 0), enemySpaceship.transform.rotation);
			beam = Instantiate (beamLineRendererPrefab [currentBeam], new Vector3 (0, 0, 0), enemySpaceship.transform.rotation);
			beamEnd = Instantiate (beamEndPrefab [currentBeam], new Vector3 (0, 0, 0), enemySpaceship.transform.rotation);
			line = beam.GetComponent<LineRenderer> ();

			// Declares a raycast hit to store raycast information
			RaycastHit hit;

			line.SetPosition (0, enemySpaceship.transform.position);

			// Checks if raycast has hit anything
			if (Physics.Raycast (enemySpaceship.transform.position, enemySpaceship.transform.forward, out hit, weaponRange)) {
				
				Vector3 shotdir = hit.point - enemySpaceship.transform.position;
				ShootBeamInDir (enemySpaceship.transform.position, shotdir);

				line.SetPosition (1, hit.point);
				Debug.Log (hit.collider.gameObject.name + "gameobject was hit with tag" +  hit.collider.gameObject.tag);

				//Checks if the object hit is player
				if (hit.collider.gameObject == target) {
					target.GetComponent<health> ().wasHit = true;
				}
			} else {
				Debug.Log ("nothing hit");
				Vector3 shotempty = (enemySpaceship.transform.position + (targetDirection * weaponRange)) - enemySpaceship.transform.position;
				ShootBeamInDir (enemySpaceship.transform.position, shotempty);
			}

			StartCoroutine (ShotEffect ());
		} 
	}

	void ShootBeamInDir(Vector3 start, Vector3 dir) {
			
		line.SetPosition(0, start);
		beamStart.transform.position = start;

		Vector3 end = Vector3.zero;

		RaycastHit hit;
		if (Physics.Raycast (start, dir, out hit)) {
			end = hit.point - (dir.normalized * beamEndOffset);
		} 
		else {
			end = start + (dir * 100);
		}

		beamEnd.transform.position = end;
		line.SetPosition(1, end);

		beamStart.transform.LookAt(target.transform.position);
		beam.transform.LookAt (target.transform.position);
		beamEnd.transform.LookAt(target.transform.position);

		float distance = Vector3.Distance(start, end);
		line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
		line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
	}

	private IEnumerator ShotEffect()
	{
		beam.SetActive(true);
		beamEnd.SetActive(true);
		beamStart.SetActive(true);

		//Wait for .07 seconds
		yield return shotDuration;

		beam.SetActive(false);
		beamEnd.SetActive(false);
		beamStart.SetActive(false);
	}
}
