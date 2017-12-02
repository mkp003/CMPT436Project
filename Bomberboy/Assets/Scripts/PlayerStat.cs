﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {

	[SerializeField]
	public int health = 4;

	public float invuln_time = 3f;
	public bool invuln = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	public void TakeDamage(){
		invuln = true;
		health--;
		if (health <= 0) {
			//TODO: send information about death to server
			//TODO: ???
			Destroy (gameObject);
			return;
		}
		//trigger damage animation
		//TODO: trigger damage
		//wait to invoke disable invuln
		Invoke("DisableInvuln",invuln_time);
	}

	void DisableInvuln(){
		invuln = false;
	}
}
