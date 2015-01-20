using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Eye : MonoBehaviour {
	public GameObject eye;
	private Vector3 spawnPoint;
	//private Transform[] eyes = new Transform[3];
	public float mSpeed = 1f;
	public float mXScale = 1f;
	public float mYScale = 1f;
	
	private Vector3 mPivot;
	private Vector3 mPivotOffset;
	private float mPhase;
	private bool mInvert = false;
	private float m2Pi = Mathf.PI * 2;
	
	// Use this for initialization
	void Awake () {
		Vector3 screenTopLeft = Camera.main.ViewportToWorldPoint (new Vector3 (.25f, 1, 0));
		spawnPoint = new Vector3(screenTopLeft.x, screenTopLeft.y, 0);
	}
	
	void Start() {
		mPivot = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		mPivotOffset = Vector3.up * 2 * mXScale;
		mPhase += mSpeed * Time.deltaTime;
		if (mPhase > m2Pi) {
			mInvert = !mInvert;
			mPhase -= m2Pi;
		}
		if (mPhase < 0) {
			mPhase += m2Pi;
		}
		
		transform.position = mPivot + (mInvert ? mPivotOffset : Vector3.zero);
		Vector3 newPos = transform.position;
		newPos.y += Mathf.Sin (mPhase) * mYScale;
		newPos.x += Mathf.Cos (mPhase) * (mInvert ? -1 : 1) * mXScale;
		transform.position = newPos;
	}
}
