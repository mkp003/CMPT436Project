using UnityEngine;
using System.Collections;

public class touchMove : MonoBehaviour
{
	public float speed = 0.1F;
	Rigidbody2D r_body;

	void Start(){
		r_body = gameObject.GetComponent<Rigidbody2D> ();
	}
	void Update()
	{
		if (Input.touchCount > 0 && Input.GetTouch (0).phase != TouchPhase.Ended) {
			// Get movement of the finger since last frame
			Vector3 movePosition = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position) - transform.position;
			movePosition.Normalize ();


			// Move object across XY plane
			transform.Translate (movePosition*speed);
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate(new Vector3(0,speed,0));
		}else if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate(new Vector3(0,-speed,0));
		}else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(new Vector3(speed,0,0));
		}else if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(new Vector3(-speed,0,0));
		}
	}
}
