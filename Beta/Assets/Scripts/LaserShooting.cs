using UnityEngine;
using System.Collections;

//Other Scripts that are called by this script:
//1. NewEnemySpawning.cs
//2. Score_Manager.cs

//Other Scripts that call this script:
// 


public class LaserShooting : MonoBehaviour {

	[Header("Beam Prefabs")]
	public GameObject[] beamLineRendererPrefab;
	public GameObject[] beamStartPrefab;
	public GameObject[] beamEndPrefab;

	[Header("Adjustable Variables")]

	public float beamEndOffset = 1f;                                                       //How far from the raycast hit point the end effect is positioned
	public float textureScrollSpeed = 8f;                                                  //How fast the texture scrolls along the beam
	public float textureLengthScale = 3f;                                                  //Length of the beam texture

	public float fireRate = 0.25f;										                   // Seconds controlling how often the player can fire
	public float weaponRange = 50f;										                   // Distance in Unity units over which the player can fire
	public GameObject gunEnd;											                   // Refers to the muzzle location of the gun
	public GameObject fpscamera; 
	public GameObject portal;
	private int currentBeam = 0;

	private GameObject beamStart;
	private GameObject beamEnd;
	private GameObject beam;
	private LineRenderer line;

	private float nextFire;												                   // Store the time the player will be allowed to fire again, after firing
	private GameObject damaged;                                                            // Refers to the Game object shot by the weapon
	private GameObject enemyManager;
	private GameObject gameManager;

	void Start () 
	{
		enemyManager = GameObject.FindGameObjectWithTag ("enemyholder");
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
	}
	

	void Update () 
	{
		// Checks if the player has pressed the fire button and if enough time has elapsed since they last fired
		if (Input.GetMouseButtonDown(0) && Time.time > nextFire) 
		{
			// Updates the time when player can fire next
			nextFire = Time.time + fireRate;

			beamStart = Instantiate (beamStartPrefab [currentBeam], gunEnd.transform.position, gunEnd.transform.rotation);
			beam = Instantiate(beamLineRendererPrefab[currentBeam], new Vector3(0, 0, 0), fpscamera.transform.rotation);
			line = beam.GetComponent<LineRenderer>();

            // Declares a raycast hit to store raycast information
            RaycastHit hit;

			// Checks if raycast has hit anything
			if (Physics.Raycast (gunEnd.transform.position, gunEnd.transform.forward, out hit, weaponRange))
			{
				beamEnd = Instantiate(beamEndPrefab[currentBeam], hit.point, gunEnd.transform.rotation);
				Vector3 shotdir = hit.point - gunEnd.transform.position;
				ShootBeamInDir(gunEnd.transform.position, shotdir);


				// Checks if the object hit is enemy
				if (hit.collider.tag == "enemy") 
				{
					damaged = hit.collider.gameObject;
					damaged.GetComponent<Enemy_Laser_Shooting> ().beam.SetActive (false);
					damaged.GetComponent<Enemy_Laser_Shooting> ().beamEnd.SetActive(false);
					damaged.GetComponent<Enemy_Laser_Shooting> ().beamStart.SetActive(false);
					damaged.SetActive (false);

					GameObject portalClone = Instantiate (portal, damaged.transform.position, portal.transform.rotation);
					//portalClone.SetActive (false);
					Debug.Log ("portal made at" + damaged.transform.position);
					enemyManager.GetComponent<NewEnemySpawning> ().enemyshot = true;
					gameManager.GetComponent<Score_Manager> ().scored = true;
				}
			}
			else
			{
				beamEnd = Instantiate(beamEndPrefab[currentBeam],gunEnd.transform.position + (gunEnd.transform.forward * weaponRange) , gunEnd.transform.rotation);
				Vector3 shotempty = (gunEnd.transform.position + (gunEnd.transform.forward * weaponRange)) - gunEnd.transform.position;
				ShootBeamInDir(gunEnd.transform.position, shotempty);
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			Destroy(beamStart);
			Destroy(beamEnd);
			Destroy(beam);
		}
	}
		
	void ShootBeamInDir(Vector3 start, Vector3 dir)
	{
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

		float distance = Vector3.Distance(start, end);
		line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
		line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
	}
}