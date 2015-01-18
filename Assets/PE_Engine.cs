using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PE_Gravity {
	none,
	constant,
	planetary
}

public class PE_Engine : MonoBehaviour {
	public static List<PE_Obj>	peos;
	public Vector3				gravity = new Vector3(0,-9.8f,0);
	
	// Use this for initialization
	void Awake () {
		peos = new List<PE_Obj>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float dt = Time.fixedDeltaTime;
		foreach (PE_Obj peo in peos) {
			Vector3 a = peo.acc;
			if (peo.grav == PE_Gravity.constant) {
				a += gravity;
			}
			
			//
			Vector3 v = peo.vel;
			v += a * dt;
			peo.vel = v;
			
			peo.pos0 = peo.transform.position + peo.vel;
		}
	}
}
