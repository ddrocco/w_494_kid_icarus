﻿using UnityEngine;
using System.Collections;

public class Start_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.Return)) {
			Application.LoadLevel("_Level_1");
		}
	}
}