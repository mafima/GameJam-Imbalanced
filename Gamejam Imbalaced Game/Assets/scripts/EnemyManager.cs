using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Photon.MonoBehaviour {


	public static GameObject[] players;
	public static Transform[] playerTf;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnSingleEnemy", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnSingleEnemy() {
		UpdatePlayers ();
		if (PhotonNetwork.playerList.Length > 0) {
			photonView.RPC ("SpawnEnemy", PhotonTargets.All, "BadCube", new Vector3 (20f, 1f, -3f));
		}
	}

	[PunRPC]
	void SpawnEnemy(string obj, Vector3 pos) {
		Instantiate(Resources.Load("Enemies/" + obj), pos, Quaternion.identity);
	}

	public static void UpdatePlayers() {
		players = GameObject.FindGameObjectsWithTag ("Player");
		playerTf = new Transform[players.Length];
		for (int i = 0; i < players.Length; i++) {
			playerTf [i] = players [i].GetComponent<Transform> ();
		}
	}

}
