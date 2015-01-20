using UnityEngine;
using System.Collections;

public class Player_Action : MonoBehaviour {
	public float xMaxSpeed = .15f;
	public float vSpeed;
	public Player_Fall_Handler fallHandler;
	static public bool faceRight = true;
	static public bool faceUp = false;
	static public bool faceDown = false;
	
	void Start () {
		fallHandler = GetComponentInChildren<Player_Fall_Handler>();
	}

	void FixedUpdate () {
		if (transform.position.y > Camera.main.transform.position.y) {
			Camera.main.transform.Translate (new Vector3(0, .1f, 0));
		}
		//falls of the bottom of the screen
		Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));		
		if (transform.position.y < screenBottomLeft.y) {
			print ("I failed!");
		}
		
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
		transform.Translate (Vector3.up * vSpeed);
	}
	
	float abs(float x) {
		if (x < 0) {
			return -x;
		} else {
			return x;
		}
	}
	
	void OnTriggerEnter (Collider other) {
		//Falling on blocks:
		if (transform.position.y - other.transform.position.y > 1.6f * abs(transform.position.x
					- other.transform.position.x)) {
			transform.position += Vector3.up * (other.gameObject.transform.position.y
						+ 0.51f + transform.localScale.y / 2 - transform.position.y);
			vSpeed = 0;
		}
		
		//Bumping head on blocks:
		else if (other.transform.position.y - transform.position.y > 1.6f * abs(transform.position.x
					- other.transform.position.x)) {
			transform.position += Vector3.up * (other.gameObject.transform.position.y
                    	- 0.51f - transform.localScale.y / 2 - transform.position.y);
			vSpeed = 0;
			fallHandler.jumpState = Player_Fall_Handler.JumpState.falling;
		}
		
		//From the left-collision:
		else if (other.transform.position.x > transform.position.x) {
			transform.position += Vector3.right * (other.gameObject.transform.position.x
					- 1.01f - transform.position.x);
			fallHandler.jumpState = Player_Fall_Handler.JumpState.falling;
		}
		
		//From the right:
		else {
			transform.position += Vector3.right * (other.gameObject.transform.position.x
					+ 1.01f - transform.position.x);
			fallHandler.jumpState = Player_Fall_Handler.JumpState.falling;
		}
	}
	
	void OnTriggerStay (Collider other) {
		OnTriggerEnter(other);
	}
}
