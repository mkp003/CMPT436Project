﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleSignIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	/*
	public void GoogleAuthentication()
    {

        // Configure Google Sign In
        //GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
        //      .requestIdToken(getString(R.string.default_web_client_id))
        //    .requestEmail()
        //  .build();

        string googleIdToken = "";

        string googleAccessToken = "";

        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        Firebase.Auth.Credential credential =
    Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }*/
}
