using UnityEngine;
using System.Collections;

public class Player_Info : MonoBehaviour {
	public GameObject playerPrefab;
	public static GameObject gameObj;

	// Use this for initialization
	void Start () {
		GameObject player = Instantiate (playerPrefab) as GameObject;
		gameObj = player;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}