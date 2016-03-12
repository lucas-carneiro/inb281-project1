using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform myTransform;

	//Enemy Player reference
	private GameObject enemyPlayer;

	//Turret Variables
	public float range = 25.0f;
	public float fireRate = 1.0f;
	private float fireTime;

	//Projectile
	public GameObject turretProjectile;
	
	//Turret Parts
	public GameObject turretMuzzle;
	public GameObject turretRaycast;

	//Rotation Variables
	public float rotationSpeed = 1.0f;
	private float adjRotSpeed;
	private Quaternion targetRotation;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;

		enemyPlayer = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		//Raycast Detection
		RaycastHit hit;
		if (Physics.Raycast (turretRaycast.transform.position, -(turretRaycast.transform.position - enemyPlayer.transform.position).normalized, out hit, range)) {

			//If hit has "Player" tag...
			if (hit.transform.tag == "Player"){

				//Track Player - Linear Interpolation (LERP)
				targetRotation = Quaternion.LookRotation (enemyPlayer.transform.position - myTransform.position);
				adjRotSpeed = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
				myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRotation, adjRotSpeed);

				//Fire Projectile
				if (Time.time > fireTime) {
					Instantiate (turretProjectile, turretMuzzle.transform.position, turretMuzzle.transform.rotation);
					fireTime = Time.time + fireRate;
				}

				//Draw red debug line
				Debug.DrawLine (turretRaycast.transform.position, hit.point, Color.red);
			} else {
				//Draw green debug line
				Debug.DrawLine (turretRaycast.transform.position, hit.point, Color.green);
			}
		}
	}
}
