using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class TouchMove : NetworkBehaviour
{
    private const string VERTICAL_KEY = "Vertical";
    private const string HORIZONTAL_KEY = "Horizontal";

    [SerializeField]
    private float moveForce = 10;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private bool speedInstantStop = false;
    [SerializeField]
    private bool boolForceMove = true;
    [SerializeField]
    private Animator animator;


    private Dictionary<string, int> controlState;
    private Transform creatureTransform;
    private Rigidbody2D creatureBody;

    void Start() {
        creatureBody = this.GetComponent<Rigidbody2D>();
        creatureTransform = this.GetComponent<Transform>();

        this.controlState = new Dictionary<string, int>();
        // Initialize for the case where the player's already holding down keys.
        this.controlState.Add(VERTICAL_KEY, 0);
        this.controlState.Add(HORIZONTAL_KEY, 0);
        this.animator = this.gameObject.GetComponent<Animator>();
    }

    void Update() {
        // Update the state of the controls 
        UpdateMovementState();
        Debug.Log(Input.GetAxis("Vertical"));
        float x = this.controlState[HORIZONTAL_KEY];
        float y = this.controlState[VERTICAL_KEY];
        //Move(new Vector2(x, y)); // uncomment if using keyboard
    }

    void UpdateMovementState() {
        // Update the controlState dictionary to represent the state of the movement being pressed
        int verticalDirection = 0;
        int horizontalDirection = 0;
        // Vertical
        verticalDirection += Input.GetKeyDown(KeyCode.W) ? 1 : 0;
        verticalDirection += Input.GetKeyUp(KeyCode.W) ? -1 : 0;
        verticalDirection += Input.GetKeyDown(KeyCode.S) ? -1 : 0;
        verticalDirection += Input.GetKeyUp(KeyCode.S) ? 1 : 0;
        // Horizontal
        horizontalDirection += Input.GetKeyDown(KeyCode.D) ? 1 : 0;
        horizontalDirection += Input.GetKeyUp(KeyCode.D) ? -1 : 0;
        horizontalDirection += Input.GetKeyDown(KeyCode.A) ? -1 : 0;
        horizontalDirection += Input.GetKeyUp(KeyCode.A) ? 1 : 0;
        // Update the state of the controls
        this.controlState[VERTICAL_KEY] += verticalDirection;
        this.controlState[HORIZONTAL_KEY] += horizontalDirection;
    }

    public void Move(Vector2 myVec) {
        if (boolForceMove) {
            ForceMoveVertical(myVec.y);
            ForceMoveHorizontal(myVec.x);
        }
        else {
            MoveVertical(myVec.y);
            MoveHorizontal(myVec.x);
        }
    }

    //apply force to the character to make them move left or right
    void ForceMoveHorizontal(float x) {
        creatureBody.AddForce(creatureTransform.right * x * moveForce);
    }

    //apply force to the character to make the move up or down
    void ForceMoveVertical(float y) {
        creatureBody.AddForce(creatureTransform.up * y * moveForce);

    }

    //set speed of horizontal axis of the character to make them move left or right
    void MoveHorizontal(float x) {
        if (x != 0 || speedInstantStop) {
            float cury = creatureBody.velocity.y;
            float curx = x * speed;
            creatureBody.velocity = new Vector2(curx, cury);
            if (x < 0) {
                animator.SetInteger("DirectionX", -1);
            } else if (x > 0) {
                animator.SetInteger("DirectionX", 1);
            }
        }
        if (x == 0) {
            animator.SetInteger("DirectionX", 0);
            creatureBody.velocity.Set(0, creatureBody.velocity.y);
        }

    }

    //set speed of vertical axis of the character to make the move up or down
    void MoveVertical(float y) {
        if (y != 0 || speedInstantStop) {
            float cury = y * speed;
            float curx = creatureBody.velocity.x;
            creatureBody.velocity = new Vector2(curx, cury);
            if (y < 0) {
                animator.SetInteger("DirectionY", -1);
            }
            else if (y > 0) {
                animator.SetInteger("DirectionY", 1);
            }
        }
        if (y == 0) {
            animator.SetInteger("DirectionY", 0);
            creatureBody.velocity.Set(creatureBody.velocity.x, 0);
        }
    }
}
