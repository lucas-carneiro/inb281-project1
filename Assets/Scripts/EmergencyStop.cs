using UnityEngine;
using System.Collections;

public class EmergencyStop : MonoBehaviour {

    public string actionText;
    public AudioClip actionSound;

    public override string ToString() {
        return actionText;
    }

    void Start() {
        actionText = "Stop the machines!";
    }

    public void Act() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SendMessage("win", SendMessageOptions.DontRequireReceiver);
        AudioSource.PlayClipAtPoint(actionSound, transform.position);
        Destroy(this.gameObject);
    }
}
