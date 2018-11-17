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
	Transform target;

	// Use this for initialization
	void Start () {
		enemyTf = GetComponent<Transform>(); 
		players = GameObject.FindGameObjectsWithTag ("Player");
		playerTf = new Transform[players.Length];
		for (int i = 0; i < players.Length; i++) {
			playerTf [i] = players [i].GetComponent<Transform> ();
		}

		enemyRb = GetComponent<Rigidbody> ();
		InvokeRepeating ("SearchTarget", 0f, 2f);
	}

	void SearchTarget () {

		for (int i = 0; i < playerTf.Length; i++) {
			if (dist == 0) {
				dist = (Mathf.Pow ((playerTf[i].position.x - enemyTf.position.x), 2f) + Mathf.Pow ((playerTf[i].position.z + enemyTf.position.z), 2f));
				nearestPlayer = i;
			} else if (dist > (Mathf.Pow ((playerTf[i].position.x - enemyTf.position.x), 2f) + Mathf.Pow ((playerTf[i].position.z + enemyTf.position.z), 2f))) {
				dist = (Mathf.Pow ((playerTf[i].position.x - enemyTf.position.x), 2f) + Mathf.Pow ((playerTf[i].position.z + enemyTf.position.z), 2f));
				nearestPlayer = i;
			}
		}

		target = playerTf [nearestPlayer];
	}

	void FixedUpdate() {
	
		if (target == null) {
			SearchTarget ();
		}

		enemyRb.AddForce((target.position.x - enemyTf.position.x), 0, (target.position.z - enemyTf.position.z));
	
	}

}