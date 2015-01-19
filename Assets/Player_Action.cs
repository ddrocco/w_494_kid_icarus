using UnityEngine;
using System.Collections;

public class Player_Action : MonoBehaviour {
	public float xSpeed = .15f;
	public GameObject arrowPrefab;
	static public bool shotArrow = false;
	static public bool faceRight = true;
	static public bool faceUp = false;
	static public bool faceDown = false;
	private bool grounded = true;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//Horizontal/inplace movement
		transform.localScale = new Vector3 (1f, 1.5f, 1f);
		if (Input.GetKey ("right") || Input.GetKey ("d")) {
			if (!faceUp) {
				transform.Translate (new Vector3(xSpeed, 0, 0));
			}
			faceRight = true;
		} 
		if (Input.GetKey ("left") || Input.GetKey ("a")) {
			if (!faceUp) {
				transform.Translate (new Vector3(-xSpeed, 0, 0));
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
	
		//Shooting
		if (Input.GetButtonDown ("Fire1")) {
			if (!faceDown && !shotArrow) {
				shotArrow = true;
				GameObject arrow = Instantiate (arrowPrefab, transform.position, transform.rotation) as GameObject;
			}
		}
	}

}
