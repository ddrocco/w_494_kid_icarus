using UnityEngine;
using System.Collections;

public class Player_Fall_Handler : MonoBehaviour {
	public enum JumpState {
		jumping,
		floating,
		falling
	};
	public JumpState jumpState;
	public bool buttonHeld;
	public int jumpTimeMin = 10; //in steps
	public int jumpTimeMax = 18; //in steps
	public int floatTime = 15; //in steps
	public int currentTime;
	public int collisions;
	public float jumpSpeed = 0.2f;
	public float floatSpeed = 0.05f;
	public float floatGravity = 0.005f;
	public float fallGravity = 0.3f;
	public float maxFallSpeed = 0.15f;
	public Player_Action parent;
	
	void Start () {
		jumpState = JumpState.falling;
		collisions = 0;
		currentTime = 0;
		parent = GetComponentInParent<Player_Action>();
	}
	
	void OnTriggerEnter() {
		++collisions;
	}
	
	void OnTriggerExit() {
		--collisions;
	}
	
	void FixedUpdate() {
		if (!Input.GetKey ("x") && !Input.GetKey ("l")) {
			buttonHeld = false;
		}
		switch(jumpState) {
			case JumpState.jumping:
				if (++currentTime >= jumpTimeMax || (!buttonHeld && currentTime >= jumpTimeMin)) {
					jumpState = JumpState.floating;
					parent.vSpeed = floatSpeed;
					currentTime = 0;
				}	
				break;
			case JumpState.floating:
				parent.vSpeed -= floatGravity;
				if (++currentTime >= floatTime) {
					jumpState = JumpState.falling;
				}
				break;
			case JumpState.falling:
				if (collisions == 0) {
					parent.vSpeed -= fallGravity;
					if (parent.vSpeed < -maxFallSpeed) {
						parent.vSpeed = -maxFallSpeed;
					}
				}
				break;
		}
	}
	
	public void startJump() {
		if (collisions > 0 && jumpState == JumpState.falling) {
			parent.vSpeed = jumpSpeed;
			jumpState = JumpState.jumping;
			buttonHeld = true;
			currentTime = 0;
		}
	}
}
