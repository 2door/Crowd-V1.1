using UnityEngine;
using System.Collections;

public class FanBehaviour : MonoBehaviour {

	public float jumpSpeed;
	public float gravity;
	//components of force left to apply
	private float forceX;
	private float forceZ;
	public int jumpFreq;	//for NPC jump probability
	private Vector3 moveDirection = Vector3.zero;
	private bool grounded = false;
	private CharacterController controller;
	private Vector3 initial;
	
	void Start () {
		controller = GetComponent<CharacterController>();
		forceX = 0;
		forceZ = 0;
		//store spawn point as location to return to
		initial = GetComponent<Rigidbody>().position;
		this.tag = "Fan";
	}

	void Update() {

		//hop randomly
		if(grounded && Random.Range(0, jumpFreq)==0) {
			moveDirection.y = jumpSpeed;
		} else {
			moveDirection.y = 0;
		}
		//fall after jump
		moveDirection.y -= gravity * Time.deltaTime;

		//get the current position to compare with initial
		var position = GetComponent<Rigidbody>().position;
		//return to initial position if pushed
		if(initial.x != position.x) {
			forceX += (initial.x - position.x)/5;
		}
		if(initial.z != position.z) {
			forceZ += (initial.z - position.z)/5;
		}
		controller.Move( (moveDirection * Time.deltaTime) +
							new Vector3(forceX, 0, forceZ) );
		//if was able to move, then no more need for force
		if(GetComponent<Rigidbody>().position.x != position.x ||
			GetComponent<Rigidbody>().position.z != position.z) {
			forceX = 0;
			forceZ = 0;
		}
		grounded = controller.isGrounded;
	}

	void OnTriggerStay(Collider coll) {
		//move on collision with player
		if(coll.gameObject.tag == "Player") {		
			var force = gameObject.transform.position -
						coll.gameObject.transform.position;
			forceX = force.x/5;
			forceZ = force.z/5;
		} 
	}
}
