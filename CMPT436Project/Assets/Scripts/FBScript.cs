using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FBScript : MonoBehaviour {

    // Parent Object of the logged in objects
    [SerializeField]
    private GameObject loggedInObject;

    // Parent Object of the logged out objects
    [SerializeField]
    private GameObject loggedOutObject;

    // Reference to username text
    [SerializeField]
    private GameObject usernameText;

	// Use this for initialization
	void Awake () {
        FB.Init(SetInit, OnHideUnity);
	}


    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Logged in to Facebook.");
        }
        else
        {
            Debug.Log("NOT logged in to Facebook.");
        }
        FacebookMenus(FB.IsLoggedIn);
    }


    void OnHideUnity(bool isGameShown)
    {
        if (isGameShown)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }


    /// <summary>
    /// FacebookLogin() will be called when the user selects the login button.  It will attempt to log 
    /// the user into facebook.
    /// </summary>
    public void FacebookLogin()
    {
        // Create a list of permissions which represent Facebook elements we want to access.
        List<string> listOfPermissions = new List<string>();
        listOfPermissions.Add("public_profile");
        FB.LogInWithReadPermissions(listOfPermissions, AuthCallBack);
    }


    void AuthCallBack(IResult result)
    {
        if(result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("Logged in to Facebook.");
            }
            else
            {
                Debug.Log("NOT logged in to Facebook.");
            }
            FacebookMenus(FB.IsLoggedIn);
        }
    }


    /// <summary>
    /// FacebookMenus() will determine what is displayed according to if the user is logged in or 
    /// not.
    /// </summary>
    /// <param name="isLoggedIn">bool true if user is logged in, false otherwise</param>
    void FacebookMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            this.loggedInObject.SetActive(true);
            this.loggedOutObject.SetActive(false);
            // Get users' Facebook name
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayFacebookName);
        }
        else
        {
            this.loggedInObject.SetActive(false);
            this.loggedOutObject.SetActive(true);
        }
    }


    private void DisplayFacebookName(IResult result)
    {
        if (result.Error == null)
        {
            Text name = usernameText.GetComponent<Text>();
            name.text = "Welcome " + result.ResultDictionary["first_name"];
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
	
}
