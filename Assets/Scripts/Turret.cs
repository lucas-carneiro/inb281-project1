using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    public string actionText;
	public Transform myTransform;

	//Enemy Player reference
	private GameObject enemyPlayer;

	//Turret Variables
	public float range = 25.0f;
	public float fireRate = 1.0f;
    public bool rotationOn = true;
	private float fireTime;

	//Projectile
	public GameObject turretProjectile;
	
	//Turret Parts
	public GameObject turretMuzzle;
	public GameObject turretRaycast;
    public GameObject turretPrincipal;

	//Rotation Variables
	public float rotationSpeed = 1.0f;
	private float adjRotSpeed;
	private Quaternion targetRotation;

    //Songs for destruction and projectiles
    public AudioClip projectileSound;
    public AudioClip actionSound;

    public override string ToString() {
        return actionText;
    }

	// Use this for initialization
	void Start () {
        actionText = "Dismantle:";
        ////If not using muzzle as a rotation point and not addressing the turret object to this script
        if (turretPrincipal == null) {
            myTransform = this.transform;
        }
		enemyPlayer = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		//Raycast Detection
		RaycastHit hit;
		if (Physics.Raycast (turretRaycast.transform.position, -(turretRaycast.transform.position - enemyPlayer.transform.position).normalized, out hit, range)) {

			//If hit has "Player" tag...
			if (hit.transform.tag == "Player"){
                
                //If turret is set to rotate				
                if (rotationOn) {
                    //Track Player - Linear Interpolation (LERP)
                    targetRotation = Quaternion.LookRotation(enemyPlayer.transform.position - myTransform.position);
                    adjRotSpeed = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
                    myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRotation, adjRotSpeed);
                }				

                //If using muzzle as a rotation point and addressing the turret object to this script
                if (turretPrincipal != null) {
                    turretPrincipal.transform.rotation = myTransform.rotation;
                }

				//Fire Projectile
				if (Time.time > fireTime) {
					Instantiate (turretProjectile, turretMuzzle.transform.position, turretMuzzle.transform.rotation);
					fireTime = Time.time + fireRate;
                    AudioSource.PlayClipAtPoint(projectileSound, transform.position);
				}

				//Draw red debug line
				Debug.DrawLine (turretRaycast.transform.position, hit.point, Color.red);
			} else {
				//Draw green debug line
				Debug.DrawLine (turretRaycast.transform.position, hit.point, Color.green);
			}
		}
	}

    public void Act() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SendMessage("getScrap", SendMessageOptions.DontRequireReceiver);
        AudioSource.PlayClipAtPoint(actionSound, transform.position);
        Destroy(this.gameObject);
    }
}
