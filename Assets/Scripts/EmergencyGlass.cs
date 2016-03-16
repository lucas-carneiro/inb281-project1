using UnityEngine;
using System.Collections;

public class EmergencyGlass : MonoBehaviour {

    public string actionText;

    public override string ToString() {
        return actionText;
    }

    void Start() {
        actionText = "Break glass:";
    }

    public void Act() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SendMessage("getPower", SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }
}
