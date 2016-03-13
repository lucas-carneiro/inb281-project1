using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	public float maxHP = 5f;
	private float currentHP;

	//Object that represents HP
	public GameObject HP;

	//Object that represents an image which appears when the player loses health
	public Image damageImage;
	public Color damageColor;
	public float damageFade = 5f;

    public KeyCode actionKey = KeyCode.Alpha1;
    public GameObject ActionText;

	void Start(){
		currentHP = maxHP;
		damageColor.a = 0;
	}

	void Update(){
		if (currentHP <= 0){
			//Destroy(this.gameObject);
		}

        //Fade damageImage
		if (damageImage.color.a > 0f){
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, damageFade * Time.deltaTime);
		}
	}

    void OnTriggerEnter(Collider collidingObject){
        //If collidingObject is an Enemy
        if (collidingObject.gameObject.name.Contains("Turret")){
            ActionText.SetActive(true);
        }
    }

    void OnTriggerStay(Collider collidingObject){
        //If collidingObject is an Enemy
        if (collidingObject.gameObject.name.Contains("Turret")){
            if (Input.GetKeyDown(actionKey)){
                collidingObject.gameObject.SendMessage("Dismantle", SendMessageOptions.DontRequireReceiver);
                ActionText.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider collidingObject) {
        //If collidingObject is an Enemy
        if (collidingObject.gameObject.name.Contains("Turret")) {
            ActionText.SetActive(false);
        }
    }

    //Called by external game objects
    void TakeDamage(float damage){
		currentHP -= damage;
		HP.transform.localScale = new Vector3(currentHP / maxHP, 1f, 1f);
		damageImage.color = new Vector4 (damageColor.r, damageColor.g, damageColor.b, 1f);
	}
}