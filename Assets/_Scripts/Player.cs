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
	// Transforms set from Unity
	public Transform Beer;
	public Transform SpawnTo;
	public GameStateController gameState;
	//public BeerScript beerScript;
	private bool loaded;
	private bool won;
	
	void Start () {
		this.tag = "Player";
		loaded = false;
		won = false;
		gameState = GameStateController.gameStateController;
	}

	void Awake () {
	    GetComponent<Rigidbody>().freezeRotation = true;
	    GetComponent<Rigidbody>().useGravity = true;
		transform.Find("Body").GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
	}

	void Update() {
		// if(beerScript != null) {
		// 	Debug.Log("Have Beer");
		// 	gameState.SetBeer(beerScript.GetQuantity());
		// }
	}
 
	void FixedUpdate () {
		if(gameState.GetState() != 2) {
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
		}
	}

	void OnTriggerStay(Collider coll) {
		if(coll.gameObject.tag == "Bar" && !loaded) {
			//player has arrived at bar for first time
			loaded = true;
			// Set to 2nd stage of game: carrying beers
			if(gameState.SetState(1)) {
				Beer = (Transform) Instantiate(Beer, new Vector3(0, 0, 0),
													Quaternion.identity);
				Beer.parent = SpawnTo;
				Beer.transform.position = SpawnTo.transform.position;
				Beer.transform.rotation = SpawnTo.transform.rotation;
			}
			//beerScript = Beer.GetComponent<BeerScript>();
		} else if(coll.gameObject.tag == "Friend" && loaded) {
			// float finalBeer = beerScript.GetQuantity();
			// Destroy(Beer);
			gameState.SetState(2);	// Set to winning state
			//gameState.SetFinalBeer(finalBeer);
			won = true;
		}
	}
}
