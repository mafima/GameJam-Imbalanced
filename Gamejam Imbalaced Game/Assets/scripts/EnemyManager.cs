using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Photon.MonoBehaviour {


	public static GameObject[] players;
	public static Transform[] playerTf;
	[SerializeField] bool spawn = false;

    public static Queue<GameObject> deadPepegas;

    Transform pepegaParent;

	// Use this for initialization
	void Start () {
        deadPepegas = new Queue<GameObject>();
        pepegaParent = new GameObject("Pepegas").transform;
		InvokeRepeating ("PepegaTest", 0f, 7f);
	}
	
	// Update is called once per frame
	void Update () {
		if (spawn) {
			SpawnSingleEnemy ("Pepega");
			spawn = false;
		}
	}

    void PepegaTest() {
        SpawnSingleEnemy("Pepega");
    }

	void SpawnSingleEnemy(string name) {
		if (PhotonNetwork.playerList.Length > 0) {
			photonView.RPC ("SpawnEnemy", PhotonTargets.All, name, new Vector3 (20f, 1f, -3f));
		}
	}

	[PunRPC]
	void SpawnEnemy(string obj, Vector3 pos) {
		Instantiate(Resources.Load("Enemies/" + obj), pos, Quaternion.identity, pepegaParent);
	}

	public static void UpdatePlayers() {
		players = GameObject.FindGameObjectsWithTag ("Player");
		playerTf = new Transform[players.Length];
		for (int i = 0; i < players.Length; i++) {
			playerTf [i] = players [i].GetComponent<Transform> ();
		}
	}
}