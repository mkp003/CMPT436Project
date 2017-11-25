using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSpawn : MonoBehaviour {

	[SerializeField]
	private NetworkManager manager;

	// Use this for initialization
	void Start () {
		this.manager = gameObject.GetComponent<NetworkManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
