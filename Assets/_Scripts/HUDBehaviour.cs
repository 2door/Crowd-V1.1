using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBehaviour : MonoBehaviour {

	public GameStateController gameState;
	public Text displayObjective;
	public Text displayTime;
	public Text displayTimeResult;
	public Text displauyBeerResult;
	public GameObject modalPannel;

	// Use this for initialization
	void Start () {
		modalPannel.SetActive(false);
		gameState = GameStateController.gameStateController;
	}
	
	// Update is called once per frame
	void Update () {
		displayTime.text = "" + gameState.GetTime();
		if(gameState.GetState() == 0) {
			displayObjective.text = "Find the bar and get some drinks!";
		} else if(gameState.GetState() == 1) {
			displayObjective.text = "Find your friends without spilling the drinks!\nCurrent drinks level: " + gameState.GetBeer();
		} else if(gameState.GetState() == 2) {
			displayObjective.text = "Congratulations! Enjoy the show!";
			displauyBeerResult.text = "" + gameState.GetFinalbeer();
			displayTimeResult.text = "" + gameState.GetFinalTime();
			DisplayResults();
		}
	}

	void DisplayResults() {
		modalPannel.SetActive(true);
	}
}
