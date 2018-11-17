using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    int worldSize = 10;
    GameObject[,] ground;
    public GameObject groundPrefab;
    public GameObject[] smallAssets;
    public GameObject[] mediumAssets;
    public GameObject[] bigAssets;

    Transform smallAssetsT, mediumAssetsT, bigAssetsT;

    // Use this for initialization
    void Start () {
        smallAssetsT = new GameObject("SmallAssets").transform;
        mediumAssetsT = new GameObject("MediumAssets").transform;
        bigAssetsT = new GameObject("BigAssets").transform;
        if (PhotonNetwork.isMasterClient) {
            PhotonView pv = GetComponent<PhotonView>();
            ground = new GameObject[worldSize, worldSize];
            Transform terrain = new GameObject("Terrains").GetComponent<Transform>();
            for (int i = 0; i < worldSize; i++) {
                for (int j = 0; j < worldSize; j++) {
                    ground[i, j] = Instantiate(groundPrefab, new Vector3((i - worldSize / 2) * 500, 0f, (j - worldSize / 2) * 500), groundPrefab.transform.rotation, terrain);
                    Vector2 from, to;
                    from = new Vector2(i - worldSize / 2 - 250, j - worldSize / 2 - 250);

                    int amount = Random.Range(10, 50);
                    for (int k = 0; k < amount; k++) {
                        int index = Random.Range(0, smallAssets.Length - 1);
                        Vector2 pos = from;
                        pos.x += Random.value * 500f;
                        pos.y += Random.value * 500f;
                        pv.RPC("SpawnNewObject", PhotonTargets.All, smallAssets[index], pos, 0);
                    }
                }
            }
        }
	}

    [PunRPC]
    void SpawnNewObject(GameObject obj, Vector2 pos, int size) {
        Instantiate(obj, pos, obj.transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
        return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 500f, 32768)) {
            Debug.Log(hit.transform.name);
        } else {
            if (transform.position.y < 300f) {

            }
        }
	}
}
