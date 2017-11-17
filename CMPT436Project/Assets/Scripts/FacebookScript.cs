using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        // Check if the facebook sdk has not been initalized
        if (!FB.IsInitialized)
        {
            // We need to initalize facebook
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else
                {
                    Debug.Log("Error: cannot initalize Facebook");
                }
            },
            isGameShown =>
            {
                if (!isGameShown)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            });
        }
        // Facebook is already initalized
        else
        {
            FB.ActivateApp();
        }
	}


    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
    }


    public void FacebookLogout()
    {
        FB.LogOut();
    }

}
