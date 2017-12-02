using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {
    
	public int health = 4;

	public float invuln_time = 3f;
	public bool invuln = false;

    private KeyValuePair<string, string> playerID; // key: player id/twitter id, value: playername

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
            GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Stats>().RegisterPlayerDeath(playerID);
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
