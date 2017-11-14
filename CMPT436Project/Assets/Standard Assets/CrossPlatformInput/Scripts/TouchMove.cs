using UnityEngine;
using System.Collections;

public class TouchMove : MonoBehaviour
{
	public float speed;
	Rigidbody2D r_body;

	Vector2 RIGHT;
	Vector2 LEFT;
	Vector2 UP;
	Vector2 DOWN;
	Vector2 STOP;


	void Start(){
		r_body = gameObject.GetComponent<Rigidbody2D> ();
		speed = 4f;



		UP = new Vector2 (0, 1);
		DOWN = new Vector2 (0, -1);
		RIGHT = new Vector2 (1, 0);
		LEFT = new Vector2 (-1, 0);
		STOP = new Vector2 (0, 0);
	}

	public void Move(Vector2 v){
		r_body.velocity = v*speed;
	}

	void Update()
	{

		// Get movement input
		// Touch input
		// TODO: change to control by virtual joystick
		/*
		if (Input.touchCount > 0 && Input.GetTouch (0).phase != TouchPhase.Ended) {
			// Get movement of the finger since last frame
			Vector3 movePosition = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position) - transform.position;
			movePosition.z = 0;
			movePosition.Normalize ();

			// Move object across XY plane
			movePosition *= speed;
			r_body.velocity =new Vector2(movePosition.x,movePosition.y);

		//Arrow key movement
		} else 
		*/
		if (Input.GetKey (KeyCode.UpArrow)) {
			Move (UP);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			Move (DOWN);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			Move (RIGHT);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			Move (LEFT);
		} else if (Input.GetKeyUp (KeyCode.UpArrow)) {
			Move (STOP);
		} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
			Move (STOP);
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
			Move (STOP);
		} else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			Move (STOP);
		}


		// Drop Bomb Command
		if (Input.GetKey (KeyCode.Space)) {
			//Instantiate(
		}


	}

	void FixedUpdate(){

	}
}
