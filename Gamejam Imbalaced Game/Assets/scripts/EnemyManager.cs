using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Photon.MonoBehaviour {


	public static GameObject[] players;
	public static Transform[] playerTf;
	[SerializeField] bool spawn = false;

    public static Queue<GameObject> deadPepegas;

    Transform pepegaParent;

    int enemyPhotonIds = 100;

	// Use this for initialization
	void Start () {
        UpdatePlayers();
        deadPepegas = new Queue<GameObject>();
        pepegaParent = new GameObject("Pepegas").transform;
        InvokeRepeating("PepegaTest", 0f, 7f);
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
        switch (obj) {
            case "Pepega":
                if (deadPepegas.Count > 0) {
                    GameObject spawn = deadPepegas.Dequeue();
                    spawn.transform.position = pos;
                    spawn.SetActive(true);
                    return;
                }
                break;
            default:
                break;
        }
        GameObject temp = (GameObject)Instantiate(Resources.Load("Enemies/" + obj), pos, Quaternion.identity, pepegaParent);
        temp.GetComponent<PhotonView>().viewID = enemyPhotonIds++;
        if (enemyPhotonIds>998) {
            enemyPhotonIds = 100;
        }
	}

	public static void UpdatePlayers() {
		players = GameObject.FindGameObjectsWithTag ("Player");
		playerTf = new Transform[players.Length];
		for (int i = 0; i < players.Length; i++) {
			playerTf [i] = players [i].GetComponent<Transform> ();
		}
	}
}