using UnityEngine;
using System.Collections;

public class CrowdManager : MonoBehaviour {

	//store coordinates for NPC spawn points
	private float[] spawnPointsX = new float[22];
	private float[] spawnPointsZ = new float[22];
	public GameObject fan;		//the Fan NPC prefab
	public GameObject player;	//the player prefab
	public GameObject friends;	//the friend group prefab
	private Vector3 playerPos;
	private Vector3 friendsPos;
	//public GameObject thug;
	//public GameObject sec;

	void Start () {
		print(gameObject.name);
		//calculate spawn coords for fans
		for(int i=0; i<22; i++) {
			spawnPointsX[i] = i*2 - 21;
			spawnPointsZ[i] = i*2 - 21;
		}
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

		//should be 0-21 for full public gen
		for(int i=1; i<22; i++) {
			for(int j=1; j<22; j++) {
				//if(! ( (i==11 || i==12)&&(j==11 || j==12) ) ) {
					if(i == playerX && j == playerZ) {
						Instantiate(player, new Vector3(spawnPointsX[i],
								0.0f, spawnPointsZ[j]), Quaternion.identity);
					} else if(i == friendsX && j == friendsZ) {
						Instantiate(friends, new Vector3(spawnPointsX[i] + 4.5f,
								0.0f, spawnPointsZ[j] - 4.5f), Quaternion.identity);
					} else {
						Instantiate(fan, new Vector3(spawnPointsX[i],
								0.0f, spawnPointsZ[j]), Quaternion.identity);
					}
				//}
			}
		}
	}
}
