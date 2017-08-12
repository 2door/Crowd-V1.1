using UnityEngine;
using System.Collections;

public class BeerScript : MonoBehaviour {

	public float quantity;

	void Start () {

	}
	
	void Update () {
		
	}

	void OnTriggerStay(Collider coll) {
		//decrease beer in cups on frontal collision
		if(coll.gameObject.tag == "Fan") {
			quantity -= 0.3f;
		}
	}
}
