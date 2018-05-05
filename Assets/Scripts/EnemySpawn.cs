using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	
	public Character player;

	public GameObject enemyA;
	public GameObject enemyB;
	public float spawnTime = 3f;

	private int totalSpawn;
	private int spawned;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<Character>();

		spawned = 0;
		totalSpawn = 10;
		enemyA = Resources.Load ("prefabs/foe/Types/Foe", typeof (GameObject)) as GameObject;
		enemyB = Resources.Load ("prefabs/foe/Types/Foe2", typeof (GameObject)) as GameObject;
		InvokeRepeating ("Spawn", spawnTime, spawnTime);

	}

	void Spawn () {
		float distance = Mathf.Abs (player.transform.position.x - this.transform.position.x);
		if (distance > 5.2f && distance < 25.0f) {
			if ((++spawned % 4) == 0) {
				Instantiate (enemyB, this.transform.position, Quaternion.identity); 

			} else {
				Instantiate (enemyA, this.transform.position, Quaternion.identity); 
			}	
		}
	}
}
