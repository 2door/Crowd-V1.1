using UnityEngine;
using System.Collections;

public class BeerScript : MonoBehaviour {

	private float quantity;
	public GameStateController gameState;

	void Start () {
		gameState = GameStateController.gameStateController;
		quantity = 100;
		gameState.SetBeer(quantity);
	}
	
	void Update () {
		if(gameState.GetState() == 2) {
			Debug.Log("WON. Beer: " + (uint) quantity );
			gameState.SetFinalBeer((uint) quantity);
			Destroy(gameObject);
		}
	}

	void OnTriggerStay(Collider coll) {
		// Decrease beer in cup on frontal collision
			Debug.Log("Hit!");
			quantity -= 0.3f;
			gameState.SetBeer((float) System.Math.Round(quantity, 2));
	}

	public float GetQuantity() {
		return quantity;
	}
}
