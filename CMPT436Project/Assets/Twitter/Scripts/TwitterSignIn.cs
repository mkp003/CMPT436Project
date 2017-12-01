using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitterKit.Unity;

public class TwitterSignIn : MonoBehaviour {

	// Use this for initialization
	void Awake() {
		Twitter.Init ();

	}

	public void StartLogin () {
		print ("start login");
		TwitterSession session = Twitter.Session;
		if (session == null) {
			Twitter.LogIn (LoginComplete, LoginFailure);
		} else {
			LoginComplete (session);
		}
	}

	public void LoginComplete (TwitterSession session) {
		// Start composer or request email
	}

	public void LoginFailure (ApiError error) {
		UnityEngine.Debug.Log ("code=" + error.code + " msg=" + error.message);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
