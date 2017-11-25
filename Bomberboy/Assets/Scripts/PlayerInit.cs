using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInit : NetworkBehaviour {
	
    // do da stuff
	void Start () {
		if (this.isLocalPlayer) {
            this.gameObject.GetComponent<TouchMove>().enabled = true;
            this.gameObject.GetComponent<BombScript>().enabled = true;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
	}
}
