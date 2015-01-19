using UnityEngine;
using System.Collections;

public class Foe_Dragon : Obj_Foe {

	public enum direction {
		left,
		right
	};
	
	public direction facing;
	public bool falling;
	private bool collidedSinceLastUpdate;
	public float horizontalSpeed = 3;
	public float fallingSpeed = 3;
	public float minTimeToFlip = 0;
	public int collisionPauseTime = 25; //500 ms
	private int collisionPauseTimer = 25;
	
	void Start () {
		facePlayer ();
		falling = true;
	}
	
	void FixedUpdate () { //enemy continues moving where it's moving
		if (collisionPauseTimer == collisionPauseTime) {
			Vector3 translation = Vector3.zero; //Should never be used
			collidedSinceLastUpdate = false;		
			if (falling == true) {
				translation = Vector3.down * fallingSpeed;
			} else if (facing == direction.left) {
				translation = Vector3.left * horizontalSpeed;
			} else {
				translation = Vector3.right * horizontalSpeed;
			}
			transform.position += translation * Time.fixedDeltaTime;
		} else {
			++collisionPauseTimer;
		}
	}
	
	void OnTriggerEnter (Collider other) { //Turn around or stop falling and face player
		if (collidedSinceLastUpdate == false) {
			collidedSinceLastUpdate = true;
			collisionPauseTimer = 0;
			if (falling == true) { //Correct for falling; Height of collision block from center is 0.5
				transform.position += Vector3.up * (other.gameObject.transform.position.y + 1.01f - transform.position.y);
				falling = false;
			} else if (facing == direction.left) {
				transform.position += Vector3.right * (other.gameObject.transform.position.x + 1.01f - transform.position.x);
				facing = direction.right;
			} else {
				transform.position += Vector3.right * (other.gameObject.transform.position.x - 1.01f - transform.position.x);
				facing = direction.left;
			}
		}
	}
	
	public void facePlayer() {
		if (transform.position.x < Player_Info.gameObj.transform.position.x) {
			facing = direction.right;
		} else if (transform.position.x >= Player_Info.gameObj.transform.position.x) {
			facing = direction.left;
		}
	}
	
	public void flipDirection() {
		if (facing == direction.right) {
			facing = direction.left;
		} else if (facing == direction.left) {
			facing = direction.right;
		}
	}
}