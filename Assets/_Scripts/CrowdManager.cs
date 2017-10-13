using UnityEngine;
using System.Collections;

/**
 * Be aware that at the some point barZ was actually its
 * Y and barY was actually its Z and may still be wrong somewhere
 */
public class CrowdManager : MonoBehaviour {

	//store coordinates for NPC spawn points
	private float[] spawnPointsX = new float[22];
	private float[] spawnPointsZ = new float[22];
	public GameObject bar;		//Bar prefab
	public GameObject fan;		//the Fan NPC prefab
	public GameObject player;	//the player prefab
	public GameObject friends;	//the friend group prefab
	private Vector3 playerPos;
	private Vector3 friendsPos;
	//as seen from above, Z pointing to right
	int barWall;
	float barX;
	float barZ;
	public const float TOP = -15.39f;
	public const float RIGHT = 14.851f;	
	public const float BOTTOM = 14.93f;
	public const float LEFT = -15.39f;
	private const float barY = 3.681025f;
	//public GameObject thug;
	//public GameObject sec;

	void Start () {
		//print(gameObject.name);
		
		genBar();
		genCrowd();
	}

	//function to randomly place bar on one of the walls at some location
	private void genBar() {
		barWall = Random.Range(1, 5);
		if (barWall == 1) {
			Debug.Log("TOP");
			barX = TOP;
			barZ = Random.Range(-20.46f, 17.27f);
			Instantiate(bar, new Vector3(barX, barY, barZ), Quaternion.identity);
		} else if (barWall == 2) {
			Debug.Log("RIGHT");			
			barX = Random.Range(-20.46f, 17.27f);
			barZ = RIGHT;
			Instantiate(bar, new Vector3(barX, barY, barZ), Quaternion.Euler(0, 90, 0));			
		} else if (barWall == 3) {
			Debug.Log("BOTTOM");
			barX = BOTTOM;
			barZ = Random.Range(-17.27f, 20.46f);
			Instantiate(bar, new Vector3(barX, barY, barZ), Quaternion.Euler(0, 180, 0));			
		} else {
			Debug.Log("LEFT");
			barX = Random.Range(-17.27f, 20.46f);
			barZ = LEFT;
			Instantiate(bar, new Vector3(barX, barY, barZ), Quaternion.Euler(0, -90, 0));			
		}
	}

	private void genCrowd() {

		//select random coordinates for player spawn
		int playerX = Random.Range(1, 21);
		int playerZ = Random.Range(1, 21);
		//select random coordinates for goal spawn
		int friendsX;
		int friendsZ;
		do {
			friendsX = Random.Range(1, 21);
		} while(friendsX == playerX);
		do {
			friendsZ = Random.Range(1, 21);
		} while(friendsZ == playerZ);

		//calculate spawn coords for fans
		for(int i=0; i<22; i++) {
			spawnPointsX[i] = i*2 - 21;
			spawnPointsZ[i] = i*2 - 21;
		}
		//FIX: ACTUALLY NOT i AND j BUT THEIR ELEMENTS IN ARRAY DUH
		//should be 0-21 for full public gen
		for(int i=0; i<22; i++) {
			for(int j=0; j<22; j++) {
				//if(! ( (i==11 || i==12)&&(j==11 || j==12) ) ) {
					if(i == playerX && j == playerZ) {
						Instantiate(player, new Vector3(spawnPointsX[i],
								0.0f, spawnPointsZ[j]), Quaternion.Euler(0, 
									Random.Range(-180.0f, 180.0f), 0));
					}else if(i == friendsX && j == friendsZ) {
						Instantiate(friends, new Vector3(spawnPointsX[i] + 4.5f,
								0.0f, spawnPointsZ[j] - 4.5f), Quaternion.identity);
					} else {
						if(barWall == 1) {
							//on TOP
							if(i == 0) {
								if(spawnPointsZ[j] < barZ-2.4 || spawnPointsZ[j] > barZ+5.2) {
									Instantiate(fan, new Vector3(spawnPointsX[i],
										0.0f, spawnPointsZ[j]), Quaternion.identity);
								}
							} else {
								Instantiate(fan, new Vector3(spawnPointsX[i],
									0.0f, spawnPointsZ[j]), Quaternion.identity);
							}
						} else if(barWall == 2) {
							//on RIGHT
								if(j == 21) {
									if(spawnPointsX[i] < barX-2.4 || spawnPointsX[i] > barX+5.2) {
										Instantiate(fan, new Vector3(spawnPointsX[i],
											0.0f, spawnPointsZ[j]), Quaternion.identity);
									}
								} else {
								Instantiate(fan, new Vector3(spawnPointsX[i],
									0.0f, spawnPointsZ[j]), Quaternion.identity);
							}
						} else if(barWall == 3) {
							//on BOTTOM
							if(i == 21) {
								if(spawnPointsZ[j] < barZ-5.2 || spawnPointsZ[j] > barZ+2.4) {
									Instantiate(fan, new Vector3(spawnPointsX[i],
										0.0f, spawnPointsZ[j]), Quaternion.identity);
								}
							} else {
								Instantiate(fan, new Vector3(spawnPointsX[i],
									0.0f, spawnPointsZ[j]), Quaternion.identity);
							}
						} else {
							//on LEFT
							if(j == 0) {
								if(spawnPointsX[i] < barX-5.2 || spawnPointsX[i] > barX+2.4) {
									Instantiate(fan, new Vector3(spawnPointsX[i],
										0.0f, spawnPointsZ[j]), Quaternion.identity);
								}
							} else {
								Instantiate(fan, new Vector3(spawnPointsX[i],
									0.0f, spawnPointsZ[j]), Quaternion.identity);
							}
						}
					}
				//}
			}
		}
	}
}
