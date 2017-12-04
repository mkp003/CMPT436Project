using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerStat : NetworkBehaviour {
    
	public int health = 4;

	public float invuln_time = 3f;
	public bool invuln = false;

    public bool isDead;


	// Use this for initialization
	void Start () {
        isDead = false;
	}
	
    void Update () {
    }

	// Update is called once per frame
	void FixedUpdate () {
		
	}

	public void TakeDamage(){
		health--;
        invuln = true;
        if (isLocalPlayer) {
            this.gameObject.GetComponent<PlayerLife>().UpdateLife();
        }
        if (health <= 0) {
            // Todo: give player option to share game result
            isDead = true;
            if (isLocalPlayer)
            {
                int numPlayers = GameObject.FindGameObjectsWithTag("Character").Length;
                PlayerPrefs.SetInt("Score", numPlayers);
            }
            gameObject.SetActive(false);
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
