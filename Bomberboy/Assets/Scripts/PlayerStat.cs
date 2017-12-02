using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {
    
	public int health = 4;

	public float invuln_time = 3f;
	private bool invuln = false;

    private KeyValuePair<string, string> playerID; // key: player id/facebook id, value: playername

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag.Equals("Explosion") && !invuln) {
            //coll.gameObject.SendMessage("damage", coll.gameObject);
            invuln = true;
            this.TakeDamage();
		}
	}

	void TakeDamage(){
		health--;
		if (health <= 0) {
            GameObject.FindGameObjectWithTag("StatsManager").GetComponent<Stats>().RegisterPlayerDeath(playerID);
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
