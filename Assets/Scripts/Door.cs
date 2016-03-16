using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public float openingSpeed = 1f;
    public float openingDistance = 1f;

    private GameObject player;
    private Transform door;
    private Vector3 initialPosition;
    private bool isOpening = false;
    private bool isClosing = false;
    private bool start = true;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (isOpening) {
            Open();
        }
        else {
            if (isClosing) {
                Close();
            }
        }
    }

    void OnTriggerEnter(Collider collidingObject) {
        if (collidingObject.gameObject == player) {
            if (start) {
                door = transform.GetChild(0);
                initialPosition = door.position;
                start = false;
            }
            isOpening = true;
        }
    }

    void OnTriggerExit(Collider collidingObject) {
        if (collidingObject.gameObject == player) {
            isOpening = false;
            isClosing = true;
        }
    }

    void Open() {
        door.Translate(Vector3.left * openingSpeed * Time.deltaTime);
        isOpening = Vector3.Distance(door.position, initialPosition) < openingDistance;
    }

    void Close() {
        door.Translate(Vector3.right * openingSpeed * Time.deltaTime);
        isClosing = Vector3.Distance(door.position, initialPosition) > 0.1f;
        if (!isClosing) {
            door.transform.position = initialPosition;
        }
    }
}
