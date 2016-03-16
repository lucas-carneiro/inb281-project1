using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public float maxHP = 5f;
	private float currentHP;

	//Object that represents HP
	public GameObject HP;

	//Object that represents an image which appears when the player loses health
	public Image damageImage;
	public Color damageColor;
	public float damageFade = 5f;

    //Action variables
    public KeyCode actionKey = KeyCode.Alpha1;
    public Text ActionText;

    //Teleport variables
    public bool canTeleport = false;
    public GameObject teleportTarget;
    public AudioClip teleportSound;
    public KeyCode teleportKey = KeyCode.E;
    public float teleportCooldown = 5.0f;
    private bool inCooldown = false;
    private float cooldownRemaining;
    public Text teleportText;

    void Start(){
		currentHP = maxHP;
		damageColor.a = 0;
        teleportTarget.SetActive(canTeleport);
    }

	void Update(){
		if (currentHP <= 0){
			//Destroy(this.gameObject);
		}

        if (inCooldown) {
            cooldownRemaining -= teleportCooldown * Time.deltaTime;
            inCooldown = cooldownRemaining > 0f;
            teleportText.text = Mathf.CeilToInt(cooldownRemaining).ToString();
            if (!inCooldown) {
                cooldownRemaining = 0f;
                teleportTarget.SetActive(true);
                teleportText.text = "";
            }
        }
        else {
            if (Input.GetKeyDown(teleportKey)) {
                Teleport();
            }
        }        

        //Fade damageImage
		if (damageImage.color.a > 0f){
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, damageFade * Time.deltaTime);
		}
	}

    //Interaction between player and objects
    void OnTriggerEnter(Collider collidingObject){
        //If collidingObject is an action object
        if (collidingObject.gameObject.tag == "Action") {
            ActionText.text = "" +
                collidingObject.gameObject.GetComponent<Turret>() +
                collidingObject.gameObject.GetComponent<EmergencyGlass>() + 
                " " + actionKey;
            ActionText.gameObject.SetActive(true);
        }
    }
    void OnTriggerStay(Collider collidingObject){
        //If collidingObject is an action object
        if (collidingObject.gameObject.tag == "Action") {
            if (Input.GetKeyDown(actionKey)){
                collidingObject.gameObject.SendMessage("Act", SendMessageOptions.DontRequireReceiver);
                ActionText.gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider collidingObject) {
        //If collidingObject is an action object
        if (collidingObject.gameObject.tag == "Action") {
            ActionText.gameObject.SetActive(false);
        }
    }

    //Called by external game objects
    void TakeDamage(float damage){
		currentHP -= damage;
		HP.transform.localScale = new Vector3(currentHP / maxHP, 1f, 1f);
		damageImage.color = new Vector4 (damageColor.r, damageColor.g, damageColor.b, 1f);
	}

    //Called by external game objects
    public void getPower() {
        canTeleport = true;
        teleportTarget.SetActive(true);
    }

    void Teleport() {
        if (canTeleport) {
            transform.position = new Vector3(teleportTarget.transform.position.x, transform.position.y, teleportTarget.transform.position.z);
            teleportTarget.SetActive(false);
            inCooldown = true;
            cooldownRemaining = teleportCooldown;
            AudioSource.PlayClipAtPoint(teleportSound, transform.position);
        }
    }
}