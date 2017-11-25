using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using Facebook.MiniJSON;

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

    // Reference to profile picture
    [SerializeField]
    private GameObject profilePicture;

	// Use this for initialization
	void Awake () {
        FB.Init(SetInit, OnHideUnity);
	}

    public GameObject scoreEntryPanel;
    public GameObject scrollScoreList;
    public Text randomScoreText;
    public Text currentHighScore;

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

    public void FacebookLogout()
    {
        FB.LogOut();
        FacebookMenus(FB.IsLoggedIn);
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
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayFacebookPicture);
        }
        else
        {
            this.loggedInObject.SetActive(false);
            this.loggedOutObject.SetActive(true);
        }
    }


    /// <summary>
    /// DisplayFacebookName() will display the name of the user's Facebook name to the screen.
    /// </summary>
    /// <param name="result"></param>
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

    
    /// <summary>
    /// DisplayFacebookPicture() will display image of the user's Facebook picture to the screen.
    /// </summary>
    /// <param name="result"></param>
    private void DisplayFacebookPicture(IGraphResult result)
    {
        if (result.Error == null)
        {
            Image picture = profilePicture.GetComponent<Image>();
            picture.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.Log("Cant create picture! " + result.Error);
        }
    }

    //////////////////////////////////////////////SCORES STUFF/////////////////////////////////////////////////////////

    public void randomScore()
    {
        int randomNumber = Random.Range(0, 1000);
        randomScoreText.text = randomNumber.ToString();
    }
    public void queryScores()
    {
        FB.API("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, ScoresCallback);
    }

    private void ScoresCallback(IResult result)
    {
        IDictionary<string, object> data = result.ResultDictionary;
        List<object> listOfScores = (List<object>) data["data"];

        foreach (object objects in listOfScores)
        {
            var entry = (Dictionary<string, object>)objects;
            var user = (Dictionary<string, object>)entry["user"];

            GameObject scorePanel;
            scorePanel = Instantiate(scoreEntryPanel) as GameObject;
            scorePanel.transform.SetParent(scrollScoreList.transform, false);

            Transform friendName = scorePanel.transform.Find("FriendName");
            Transform friendScore = scorePanel.transform.Find("FriendScore");
            Transform friendImage = scorePanel.transform.Find("FriendImage");

            Text FNtext = friendName.GetComponent<Text>();
            Text FStext = friendScore.GetComponent<Text>();
            Image FIImage = friendImage.GetComponent<Image>();

            FNtext.text = user["name"].ToString();
            FStext.text = entry["score"].ToString();
            
            FB.API(user["id"].ToString() + "/picture?width=120&height=120", HttpMethod.GET, delegate (IGraphResult profileImage)
            {
                if(profileImage.Error != null)
                {
                    Debug.Log(profileImage.RawResult);
                }
                else
                {
                    FIImage.sprite = Sprite.Create(profileImage.Texture, new Rect(0, 0, 120, 120), new Vector2());
                }
            });
        }
    }

    public void setScores()
    {
        FB.API("/me/scores", HttpMethod.GET, userScoresCallback);

    }
    public void userScoresCallback(IResult result)
    {
        string newHighScore = "0";
        //Debug.Log("User score is: " + result.RawResult);
        IDictionary<string, object> data = result.ResultDictionary;
        List<object> listOfScores = (List<object>)data["data"];
        foreach (object objects in listOfScores)
        {
            var entry = (Dictionary<string, object>)objects;
            newHighScore = entry["score"].ToString();
        }
        setNewHighScore(newHighScore);
    }
    public void setNewHighScore(string score)
    {

        if (int.Parse(randomScoreText.text) > int.Parse(score))
        {
            Debug.Log("entered if");
            var query = new Dictionary<string, string>();
            query["score"] = randomScoreText.text;
            FB.API("/me/scores", HttpMethod.POST, delegate(IGraphResult result) {
                Debug.Log("Score submit result: " + result.RawResult);
            }, query);
            currentHighScore.text = "High Score" + randomScoreText.text;
        }
        else
        {
            Debug.Log("entered else");
            currentHighScore.text = "High Score" + score;
        }
    }
}
