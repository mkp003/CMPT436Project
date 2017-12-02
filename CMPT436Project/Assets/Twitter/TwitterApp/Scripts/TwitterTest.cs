using UnityEngine;
using System.Collections;
using TwitterKit.Unity;

public class TwitterTest : MonoBehaviour
{
	void Start ()
	{
	}

	public void startLogin() {
		Twitter.Init ();

		Twitter.LogIn (ComposeTweet, (ApiError error) => {
			UnityEngine.Debug.Log (error.message);
		});
	}

	public void ComposeTweet(TwitterSession session) {
		ScreenCapture.CaptureScreenshot("Screenshot.png");
		string imageUri = "file://" + Application.persistentDataPath + "/Screenshot.png";
		Twitter.Compose (session,imageUri, "", new string[]{"#Bomberboy"},
			(string tweetId) => { UnityEngine.Debug.Log ("Tweet Success, tweetId=" + tweetId); },
			(ApiError error) => { UnityEngine.Debug.Log ("Tweet Failed " + error.message); },
			() => { Debug.Log ("Compose cancelled"); }
		 );
	}
}
