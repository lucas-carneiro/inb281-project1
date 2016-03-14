using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private Transform myTransform;

	public float projectileSpeed = 20.0f;
	public float projectileDamage = 5.0f;
	public float despawnTime = 5.0f;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;

		despawnTime = Time.time + despawnTime;
	}
	
	// Update is called once per frame
	void Update () {

		//Projectile Movement
		myTransform.Translate(0f, 0f, projectileSpeed * Time.deltaTime);

		//Projectile Expiry
		if(Time.time >= despawnTime){
			Destroy (this.gameObject);
		}
	}

	//When colliding with another object...
	void OnCollisionEnter(Collision collidingObject){

		//If collidingObject is an Enemy
		if(collidingObject.gameObject.tag == "Player"){
			collidingObject.transform.SendMessage("TakeDamage", projectileDamage, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(this.gameObject);
    }
}
