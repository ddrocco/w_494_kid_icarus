using UnityEngine;
using System.Collections;

public class Screen_Wrapping : MonoBehaviour {
	private Renderer[] renderers;
	private bool isWrappingX = false;
	private float screenWidth;
	private float screenHeight;
	private Transform[] ghosts = new Transform[2];

	// Use this for initialization
	void Start () {
		renderers = GetComponentsInChildren<Renderer>();
		Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, transform.position.z));
		Vector3 screenTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, transform.position.z));
		screenWidth = screenTopRight.x - screenBottomLeft.x;
		screenHeight = screenTopRight.y - screenBottomLeft.y;
		CreateGhosts ();
		PositionGhosts ();
	}
	
	bool CheckRenderers() {
		foreach (Renderer renderer in renderers) {
			if(renderer.isVisible) {
				return true;
			}
		}
		return false;
	}
	
	void CreateGhosts() {
		for (int i = 0; i < 2; ++i) {
			ghosts[i] = Instantiate (transform, Vector3.zero, Quaternion.identity) as Transform;
			DestroyImmediate(ghosts[i].GetComponent<Screen_Wrapping>());
		}
	}
	
	void PositionGhosts() {
		Vector3 ghostPosition = transform.position;
		
		//right ghost
		ghostPosition.x = transform.position.x + screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts [0].position = ghostPosition;
		
		//left ghost
		ghostPosition.x = transform.position.x - screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts [1].position = ghostPosition;
		
		for (int i = 0; i < 2; ++i) {
			ghosts[i].rotation = transform.rotation;
		}
	}
	
	void Swap() {
		foreach (Transform ghost in ghosts) {
			if (ghost.position.x < screenWidth &&
			    ghost.position.x > -screenWidth &&
			    ghost.position.y < screenHeight &&
			    ghost.position.y > -screenHeight)
			{
				transform.position = ghost.position;
				break;
			}
		}
		PositionGhosts ();
	}
	
	void ScreenWrap() {
		if (CheckRenderers ()) {
			isWrappingX = false;
			return;
		}
		if (isWrappingX) {
			return;
		}
		Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
		Vector3 newPos = transform.position;
		if (!isWrappingX && (viewportPos.x > 1 || viewportPos.x < 0)) {
			newPos.x = -newPos.x;
			isWrappingX = true;
		}
		transform.position = newPos;
	}
	

	// Update is called once per frame
	void Update () {
		ScreenWrap ();
		Swap ();
	}
}
