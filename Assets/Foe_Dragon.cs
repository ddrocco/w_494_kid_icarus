using UnityEngine;
using System.Collections;

public class Foe_Dragon : Obj_Foe {

	public enum direction {
		left,
		right,
		falling
	};
	private direction facing;

	// Use this for initialization
	void Start () {
		AddToEngine(PE_Gravity.constant);
		facing = direction.falling;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 translation;
		switch (facing) {
			case direction.left:
				translation = Vector3.forward * Time.fixedDeltaTime;
				pos0 += translation;
				break;
			case direction.right:
				translation = Vector3.forward * Time.fixedDeltaTime;
				pos0 -= translation;
				break;
			case direction.falling:
				translation = Vector3.down * Time.deltaTime;
				transform.position += translation;
				break;
		}
		transform.position = pos0;
	}
	
	void OnCollisionEnter (Collision other) {
		print ("Collision");
		FloorCollide();
	}
	
	void FloorCollide () {
		if (transform.position.x > Player_Info.gameObj.transform.position.x) {
			facing = direction.right;
			vel = Vector3.zero;
		}
		else if (transform.position.x < Player_Info.gameObj.transform.position.x) {
			facing = direction.left;
			vel = Vector3.zero;
		}
	}
}
