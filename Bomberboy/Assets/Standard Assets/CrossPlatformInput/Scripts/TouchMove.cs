using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TouchMove : NetworkBehaviour
{
	[SerializeField]
	private float speed;

	[SerializeField]
	private Animator animator;
	Rigidbody2D r_body;

	Vector2 RIGHT;
	Vector2 LEFT;
	Vector2 UP;
	Vector2 DOWN;
	Vector2 STOP;


	void Start(){
		r_body = gameObject.GetComponent<Rigidbody2D> ();
		speed = 2f;
		animator = gameObject.GetComponent<Animator> ();


		UP = new Vector2 (0, 1);
		DOWN = new Vector2 (0, -1);
		RIGHT = new Vector2 (1, 0);
		LEFT = new Vector2 (-1, 0);
		STOP = new Vector2 (0, 0);
	}

	public void Move(Vector2 v){
		if (this.isLocalPlayer) {
			r_body.velocity = v * speed;
		}
	}

	void FixedUpdate()
	{
		
		Vector2 currentVelocity = r_body.velocity;


		// Get movement input
		// Touch input
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Move (UP);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Move (DOWN);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Move (RIGHT);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
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

		float deadzone = 0.1f;

		if (currentVelocity.x < -deadzone) {
			animator.SetInteger ("DirectionX", -1);
		} else if (currentVelocity.x > deadzone) {
			animator.SetInteger ("DirectionX", 1);
		} else {
			animator.SetInteger ("DirectionX", 0);
		}

		if (currentVelocity.y < -deadzone) {
			animator.SetInteger ("DirectionY", -1);
		} else if (currentVelocity.y > deadzone) {
			animator.SetInteger ("DirectionY", 1);
		} else {
			animator.SetInteger ("DirectionY", 0);
		}
	}
}
