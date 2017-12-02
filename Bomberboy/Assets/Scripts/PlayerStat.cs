using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerStat : NetworkBehaviour {
    
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
		health--;
        invuln = true;
        if (health <= 0) {
            // Todo: give player option to share game result
            if (isLocalPlayer)
            {
                int numPlayers = GameObject.FindGameObjectsWithTag("Character").Length;
                PlayerPrefs.SetInt("Score", numPlayers);
                SceneManager.LoadScene("facebookTest");
            }
            Destroy(gameObject);
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
