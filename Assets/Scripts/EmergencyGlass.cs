using UnityEngine;
using System.Collections;

public class EmergencyGlass : MonoBehaviour {

    public string actionText;
    public AudioClip actionSound;

    public override string ToString() {
        return actionText;
    }

    void Start() {
        actionText = "Break glass:";
    }

    public void Act() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SendMessage("getPower", SendMessageOptions.DontRequireReceiver);
        AudioSource.PlayClipAtPoint(actionSound, transform.position);
        Destroy(this.gameObject);
    }
}
