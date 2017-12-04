using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

    private bool gameStarted;

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("Score", default(int));
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        int deadPlayers = 0;
        foreach (GameObject player in players) {
            if (player.GetComponent<PlayerStat>().isDead) {
                deadPlayers++;
            }
        }
        int alivePlayers = players.Length - deadPlayers;
        Debug.Log(players.Length);
        Debug.Log(deadPlayers);
        if (alivePlayers > 1) {
            gameStarted = true;
        }
        if (alivePlayers <= 1 && gameStarted) {
            if (PlayerPrefs.GetInt("Score") == default(int)) { // if local, and getint not set yet
                PlayerPrefs.SetInt("Score", Math.Max(alivePlayers, 1));
            }
            SceneManager.LoadScene("facebookTest");
        }
    }
}
