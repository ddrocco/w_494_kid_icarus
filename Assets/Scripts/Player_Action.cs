using UnityEngine;
using System.Collections;

public class Player_Action : MonoBehaviour {
	public float xMaxSpeed = .15f;
	public float gravity = .08f;
	public float maxFallSpeed = .2f;
	public float vSpeed;
	public Player_Fall_Handler fallHandler;
	static public bool faceRight = true;
	static public bool faceUp = false;
	static public bool faceDown = false;
//	private bool grounded = true;

	// Use this for initialization
	void Start () {
		fallHandler = GetComponentInChildren<Player_Fall_Handler>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Horizontal/inplace movement
		transform.localScale = new Vector3 (1f, 1.5f, 1f);
		if (Input.GetKey ("right") || Input.GetKey ("d")) {
			if (!faceUp) {
				transform.Translate (new Vector3(xMaxSpeed, 0, 0));
			}
			faceRight = true;
		} 
		if (Input.GetKey ("left") || Input.GetKey ("a")) {
			if (!faceUp) {
				transform.Translate (new Vector3(-xMaxSpeed, 0, 0));
			}
			faceRight = false;
		}
		if (Input.GetKey ("up") || Input.GetKey ("w")) {
			faceUp = true;
		} else {
			faceUp = false;
		}
		if (Input.GetKey ("down") || Input.GetKey ("s")) {
			transform.localScale = new Vector3 (1, 1, 1);
			faceDown = true;
		} else {
			faceDown = false;
		}
		
		if (Input.GetKeyDown ("l") || Input.GetKeyDown ("x")) {
			fallHandler.startJump();
		}
		/*if (Input.GetKey ("l") || Input.GetKey ("x")) {
			vSpeed += gravity;
		}*/
		
		transform.Translate (Vector3.up * vSpeed);
	}
	
	float abs(float x) {
		if (x < 0) {
			return -x;
		} else {
			return x;
		}
	}
	
	void OnTriggerEnter (Collider other) { //Turn around or stop falling and face player
		if (transform.position.y - other.transform.position.y > abs(transform.position.x
					- other.transform.position.x)) {
			transform.position += Vector3.up * (other.gameObject.transform.position.y
						+ 0.5f + transform.localScale.y / 2 - transform.position.y);
			vSpeed = 0;
		}		
/*		if (vSpeed ) { //Correct for falling; Height of collision block from center is 0.5
			transform.position += Vector3.up * (other.gameObject.transform.position.y + 1.01f - transform.position.y);
			falling = false;
		} else if (facing == direction.left) {
			transform.position += Vector3.right * (other.gameObject.transform.position.x + 1.01f - transform.position.x);
			flipDirection();
		} else {
			transform.position += Vector3.right * (other.gameObject.transform.position.x - 1.01f - transform.position.x);
			flipDirection();
		}*/
	}
}
