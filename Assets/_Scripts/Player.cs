/*
Based on FPSWalker from : http://wiki.unity3d.com/index.php?title=RigidbodyFPSWalker
Edited slightly
*/

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;
	public float maxVelocityChange;
	//transforms set from Unity
	public Transform Beer;
	public Transform SpawnTo;
	private bool loaded;
	private bool won;
	
	void Start () {
		this.tag = "Player";
		loaded = false;
		won = false;
	}

	void Awake () {
	    GetComponent<Rigidbody>().freezeRotation = true;
	    GetComponent<Rigidbody>().useGravity = true;
	}
 
	void FixedUpdate () {
		// Calculate how fast player should be moving
		Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"),
									0, Input.GetAxis("Vertical"));

		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= moveSpeed;

		// Apply a force that attempts to reach our target velocity
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x,
							-maxVelocityChange, maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z,
							-maxVelocityChange, maxVelocityChange);
		GetComponent<Rigidbody>().AddForce(velocityChange,
										ForceMode.VelocityChange);

		// Rotate camera with Mouse
		if(Input.GetAxis("Mouse X") > 0) {
			transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
		} else if(Input.GetAxis("Mouse X") < 0) {
			transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
		}

		if(won) {
			Debug.Log("YOU WIN!");
		} 
	}

	void OnTriggerStay(Collider coll) {
		if(coll.gameObject.name == "Bar" && !loaded) {
			//player has arrived at bar for first time
			loaded = true;
			Beer = (Transform) Instantiate(Beer, new Vector3(0, 0, 0),
												Quaternion.identity);
			Beer.parent = SpawnTo;
			Beer.transform.position = SpawnTo.transform.position;
			Beer.transform.rotation = SpawnTo.transform.rotation;
		} else if(coll.gameObject.tag == "Friend" && loaded) {
			won = true;
		}
	}
}
