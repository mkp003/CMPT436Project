using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombScript : NetworkBehaviour {

	[SerializeField]
	private GameObject bombPrefab;
    [SerializeField]
    private float bombDelay;
    [SerializeField]
    private float timeLastBombDropped;


    public void DropBomb(){
        if(timeLastBombDropped + bombDelay < Time.realtimeSinceStartup) {
            timeLastBombDropped = Time.realtimeSinceStartup;
            Vector3 pos = this.gameObject.transform.position;
            pos = new Vector3(pos.x, pos.y, pos.z);

            GameObject bombInst = Instantiate(bombPrefab, pos, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(bombInst);
        }
	}

	void Start () {
        timeLastBombDropped = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.Space) && this.isLocalPlayer) {
			DropBomb ();
		}
	}
}
