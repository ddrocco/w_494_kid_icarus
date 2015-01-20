using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Eye : MonoBehaviour {
	public GameObject eye;
	private Transform[] eyes = new Transform[4];
	public float speed = 3f;
	
	// Use this for initialization
	void Start () {
		Vector3 spawnPoint = Camera.main.ViewportToWorldPoint (new Vector3 (.25f, 1, 0));
		for (int i = 0; i < 4; ++i) {
			eyes[i] = Instantiate(eye, spawnPoint, Quaternion.identity) as Transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 4; ++i) {
			eyes[i].transform.Translate (new Vector3(0, -speed, 0));
		}
	}
}
