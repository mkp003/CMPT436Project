using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

	[SerializeField]
	private GameObject bombPrefab;

	public void DropBomb(){
		GameObject bombInst = Instantiate (bombPrefab, this.gameObject.transform.position, Quaternion.identity);
	}

	void Start () {		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			DropBomb ();
		}
	}
}
