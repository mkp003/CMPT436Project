using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

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

	void FixedUpdate()
	{
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

		bool isBomb = CrossPlatformInputManager.GetButton ("DropBomb");
		if (isBomb == true) {


		}
				
		if (Input.GetKey (KeyCode.Space)) {
			//Instantiate(
		}


	}
}
