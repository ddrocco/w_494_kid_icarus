using UnityEngine;
using System.Collections;

public class Player_Info : MonoBehaviour {
	public GameObject playerPrefab;
	public static GameObject gameObj;

	// Use this for initialization
	void Awake () {
		Vector3 location = new Vector3(-6, 4, 0);
		GameObject player = Instantiate (playerPrefab, location, Quaternion.identity) as GameObject;
		gameObj = player;
	}
	
	// Update is called once per frame
	void Update () {

	}

}