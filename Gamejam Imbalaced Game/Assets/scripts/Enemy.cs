using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	Rigidbody enemyRb;
	Transform enemyTf;
	public GameObject[] players;
	public Transform[] playerTf;
	float dist = 0;
	int nearestPlayer;

	// Use this for initialization
	void Start () {
		enemyTf = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Transform>(); 
		players = GameObject.FindGameObjectsWithTag ("Player");
		playerTf = new Transform[players.Length];
		for (int i = 0; i < players.Length; i++) {
			playerTf [i] = players [i].GetComponent<Transform> ();
		}

		enemyRb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < playerTf.Length; i++) {
			if (dist == 0) {
				dist = (Mathf.Pow ((playerTf[i].position.x - enemyTf.position.x), 2f) + Mathf.Pow ((playerTf[i].position.z + enemyTf.position.z), 2f));
				nearestPlayer = i;
			} else if (dist > (Mathf.Pow ((playerTf[i].position.x - enemyTf.position.x), 2f) + Mathf.Pow ((playerTf[i].position.z + enemyTf.position.z), 2f))) {
				dist = (Mathf.Pow ((playerTf[i].position.x - enemyTf.position.x), 2f) + Mathf.Pow ((playerTf[i].position.z + enemyTf.position.z), 2f));
				nearestPlayer = i;
			}
		}
			
		enemyRb.AddForce((playerTf[nearestPlayer].position.x - enemyTf.position.x), 0, (playerTf[nearestPlayer].position.z - enemyTf.position.z));
	
	}
}