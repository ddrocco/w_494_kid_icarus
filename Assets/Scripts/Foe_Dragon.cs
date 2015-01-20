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
	public int collisionPauseTimer = 25;
	
	void Start () {
		facePlayer ();
		falling = true;
	}
	
	void FixedUpdate () { //enemy continues moving where it's moving
		Vector3 translation = Vector3.zero; //Should never be used
		collidedSinceLastUpdate = false;
		if (falling == true) {
			translation = Vector3.down * fallingSpeed;
			transform.position += translation * Time.fixedDeltaTime;
		} else if (collisionPauseTimer == collisionPauseTime) {
			if (facing == direction.left) {
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
			if (falling == true) { //Correct for falling; Height of collision block from center is 0.5
				transform.position += Vector3.up * (other.gameObject.transform.position.y + 1.01f - transform.position.y);
				falling = false;
			} else if (facing == direction.left) {
				transform.position += Vector3.right * (other.gameObject.transform.position.x + 1.01f - transform.position.x);
				flipDirection();
			} else {
				transform.position += Vector3.right * (other.gameObject.transform.position.x - 1.01f - transform.position.x);
				flipDirection();
			}
		}
	}
	
	public void facePlayer() {
		if (transform.position.x < Player_Info.gameObj.transform.position.x && facing == direction.left) {
			flipDirection();
		} else if (transform.position.x >= Player_Info.gameObj.transform.position.x && facing == direction.right) {
			flipDirection ();
		}
	}
	
	public void flipDirection() {
		collisionPauseTimer = 0;
		if (facing == direction.right) {
			facing = direction.left;
		} else if (facing == direction.left) {
			facing = direction.right;
		}
	}
}