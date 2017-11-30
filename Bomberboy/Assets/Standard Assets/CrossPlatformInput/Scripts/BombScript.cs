using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombScript : NetworkBehaviour {

	[SerializeField]
	private GameObject bombPrefab;

	public void DropBomb(){
        Vector3 pos = this.gameObject.transform.position;
        pos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), pos.z);

        GameObject bombInst = Instantiate (bombPrefab, pos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(bombInst);
	}

	void Start () {		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.Space) && this.isLocalPlayer) {
			DropBomb ();
		}
	}
}
