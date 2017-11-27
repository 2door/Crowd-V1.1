using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour {

	public static GameStateController gameStateController;
	private uint state;
	private uint finalBeer;
	private float currentBeer;
	private float currentTime;
	private uint finalTime;

	void Awake() {
		DontDestroyOnLoad(gameObject);
		gameStateController = this;
		state = 0;
		currentBeer = 0;
		currentTime = 0;
	}

	void FixedUpdate() {
		if(state != 2) {
			currentTime += Time.deltaTime;
		}
	}

	public bool SetState(uint newState) {
		// Only allow switching between valid states at right stages
		if(newState == 1) {
			if(state == 0) {
				state = newState;
				return true;
			} else {
				return false;
			}
		} else if(newState == 2) {
			if(state == 1) {
				state = newState;
				finalBeer = (uint) currentBeer;
				finalTime = (uint) currentTime;
				return true;
			} else {
				return false;
			}
		} else if (newState == 0) {
			state = 0;
			return true;
		} else {
			return false;
		}
	}

	public uint GetState() {
		return state;
	}

	public void SetBeer(float val) {
		currentBeer = val;
	}
	public float GetBeer() {
		return currentBeer;
	}

	public void SetFinalBeer(uint val) {
		finalBeer = val;
	}

	public uint GetFinalbeer() {
		return finalBeer;
	}

	public uint GetFinalTime() {
		return finalTime;
	}
	public float GetTime() {
		return (float) System.Math.Round(currentTime, 2);
	}

}
