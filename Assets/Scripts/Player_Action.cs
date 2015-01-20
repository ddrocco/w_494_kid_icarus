using UnityEngine;
using System.Collections;

public class Player_Action : MonoBehaviour {
	public float xMaxSpeed = .15f;
	public float vSpeed;
	public float xSpeed;
	public Player_Fall_Handler fallHandler;
	public Player_Collision_Resolution bumpHandler;
	static public bool faceRight = true;
	static public bool faceUp = false;
	static public bool faceDown = false;
	
	public bool leftMoveRestricted = false;
	public bool rightMoveRestricted = false;
	public float blockOffsetLR = 1.025f;
	
	public float health = 7;
	public int invulnTime = 50; //2 seconds
	public int timeSinceHit = 51;
	public bool invulnerable = false;
	
	public Sprite standingPit;
	public Sprite duckingPit;
	public Sprite upwardPit;
	
	void Start () {
		fallHandler = GetComponentInChildren<Player_Fall_Handler>();
		bumpHandler = GetComponentInChildren<Player_Collision_Resolution>();	
	}

	void FixedUpdate () {
		if (invulnerable == true) {
			UpdateHitControl();
		}
		if (transform.position.y > Camera.main.transform.position.y) {
			Camera.main.transform.Translate (new Vector3(0, 0.1f, 0));
		}
		//falls of the bottom of the screen
		Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));		
		if (transform.position.y < screenBottomLeft.y) {
			print ("I failed!");
		}
		
		//Horizontal/inplace movement
		GetComponent<SpriteRenderer>().sprite = standingPit;
		if ((Input.GetKey ("right") || Input.GetKey ("d")) && rightMoveRestricted == false) {
			if (!faceUp) {
				bool no_collisions = true;
				foreach (Collider cld in bumpHandler.blocksAdjacent) {
					if (cld.transform.position.x > transform.position.x) {
						no_collisions = false;
						transform.Translate (Vector3.right * (cld.gameObject.transform.position.x
                      				- blockOffsetLR - transform.position.x));
						break;
					}
				}
				if (no_collisions) {
					transform.Translate (new Vector3(xMaxSpeed, 0, 0));
				}
			}
			transform.localScale = new Vector3(-1f,1f,1f);
			faceRight = true;
		} 
		if ((Input.GetKey ("left") || Input.GetKey ("a")) && leftMoveRestricted == false) {
			if (!faceUp) {
				bool no_collisions = true;
				foreach (Collider cld in bumpHandler.blocksAdjacent) {
					if (cld.transform.position.x < transform.position.x) {
						no_collisions = false;
						transform.Translate (Vector3.right * (cld.gameObject.transform.position.x
                     				+ blockOffsetLR - transform.position.x));
						break;
					}
				}
				if (no_collisions) {
					transform.Translate (new Vector3(-xMaxSpeed, 0, 0));
				}
			}
			transform.localScale = new Vector3(1f,1f,1f);
			faceRight = false;
		}
		if (Input.GetKey ("up") || Input.GetKey ("w")) {
			faceUp = true;
			GetComponent<SpriteRenderer>().sprite = upwardPit;
		} else {
			faceUp = false;
		}
		if (Input.GetKey ("down") || Input.GetKey ("s")) {
			transform.localScale = new Vector3 (1, 1, 1);
			faceDown = true;
			GetComponent<SpriteRenderer>().sprite = duckingPit;
		} else {
			faceDown = false;
		}
		transform.Translate (Vector3.up * vSpeed);
		leftMoveRestricted = false;
		rightMoveRestricted = false;
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
		if (fallHandler.blocksBeneath.Contains(other)) {
			transform.position += Vector3.up * (other.gameObject.transform.position.y
						+ 0.51f + transform.localScale.y / 2 - transform.position.y);
			if (vSpeed < 0) {
				vSpeed = 0;
			}
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
		if (bumpHandler.blocksAdjacent.Contains(other)) {
			if (other.transform.position.x > transform.position.x && rightMoveRestricted == false) {
				rightMoveRestricted = true;
				transform.position += Vector3.right * (other.gameObject.transform.position.x
						- blockOffsetLR - transform.position.x);
				//fallHandler.jumpState = Player_Fall_Handler.JumpState.falling;
			}
			//From the right:
			else if (other.transform.position.x < transform.position.x && leftMoveRestricted == false) {
				leftMoveRestricted = true;
				transform.position += Vector3.right * (other.gameObject.transform.position.x
						+ blockOffsetLR - transform.position.x);
				//fallHandler.jumpState = Player_Fall_Handler.JumpState.falling;
			}
		}
	}
	
	void OnTriggerStay (Collider other) {
		
	}
	
	void UpdateHitControl() {
		if (++timeSinceHit == invulnTime) {
			invulnerable = false;
			renderer.material.color = Color.white;
		} else if (timeSinceHit % 2 != 0) {
			renderer.material.color = Color.red;
		} else {
			renderer.material.color = Color.yellow;
		}
	}
	
	public void HitByEnemy() {
		if (invulnerable == false) {
			--health;
			invulnerable = true;
			timeSinceHit = 0;
		}
	}
}
