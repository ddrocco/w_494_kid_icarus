using UnityEngine;
using System.Collections;

public class Obj_Foe : MonoBehaviour {
	public int health;
	public enum item {
		smallHeart,
		halfHeart,
		fullHeart
	}
	
	public item itemDropOnDeath;
	public GameObject heartPrefab;

	/*// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
	
	public void HitByArrow() {
		if (--health == 0) {
			GameObject heart;
			heart = Instantiate(heartPrefab,transform.position,Quaternion.identity) as GameObject;
			Heart_Pickup_Script heartScript = heart.GetComponent<Heart_Pickup_Script>();
			if (itemDropOnDeath == item.smallHeart) {
				heartScript.value = 1;
			} else if (itemDropOnDeath == item.halfHeart) {
				heartScript.value = 5;
			} else {
				heartScript.value = 10;
			}
			
			Destroy (this.gameObject);
		}
	}
	
	void OnDestroy() {
		//Drop item:		
		//Puff of smoke (should spawn item when smoke clears
	}
}
