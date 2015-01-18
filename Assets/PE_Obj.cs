using UnityEngine;
using System.Collections;

public class PE_Obj : MonoBehaviour {
	public PE_Gravity	grav;
	public Vector3		acc, vel, pos0, pos1;
	
	public void AddToEngine (PE_Gravity gravity = PE_Gravity.constant) {
		grav = gravity;
		PE_Engine.peos.Add(this);
	}
}
