using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : Photon.MonoBehaviour {

    int worldSize = 10;
    GameObject[,] ground;
    public GameObject groundPrefab;
    public GameObject[] smallAssets;
    public GameObject[] mediumAssets;
    public GameObject[] bigAssets;

    Transform smallAssetsT, mediumAssetsT, bigAssetsT;

    // Use this for initialization
    public void Make() {
        StartCoroutine(Making());
    }


    IEnumerator Making() { 
        smallAssetsT = new GameObject("SmallAssets").transform;
        mediumAssetsT = new GameObject("MediumAssets").transform;
        bigAssetsT = new GameObject("BigAssets").transform;
        ground = new GameObject[worldSize, worldSize];
        Transform terrain = new GameObject("Terrains").GetComponent<Transform>();
        for (int i = 0; i < worldSize; i++) {
            for (int j = 0; j < worldSize; j++) {
                ground[i, j] = Instantiate(groundPrefab, new Vector3((i - worldSize / 2) * 500, 0f, (j - worldSize / 2) * 500), groundPrefab.transform.rotation, terrain);
                Vector2 from;
                from = new Vector2((i - worldSize / 2) * 500 - 250, (j - worldSize / 2) * 500 - 250);
                Debug.Log("from:" + from);
                int amount = Random.Range(7, 25);
                for (int k = 0; k < amount; k++) {
                    int index = Random.Range(0, smallAssets.Length - 1);
                    Vector2 pos = from;
                    pos.x += Random.value * 500f;
                    pos.y += Random.value * 500f;
                    photonView.RPC("SpawnNewObject", PhotonTargets.All, smallAssets[index].name, pos, 0);
                }

                amount = Random.Range(4, 15);
                for (int k = 0; k < amount; k++) {
                    int index = Random.Range(0, mediumAssets.Length - 1);
                    Vector2 pos = from;
                    pos.x += Random.value * 500f;
                    pos.y += Random.value * 500f;
                    photonView.RPC("SpawnNewObject", PhotonTargets.All, mediumAssets[index].name, pos, 1);
                }

                amount = Random.Range(1, 5);
                for (int k = 0; k < amount; k++) {
                    int index = Random.Range(0, bigAssets.Length - 1);
                    Vector2 pos = from;
                    pos.x += Random.value * 500f;
                    pos.y += Random.value * 500f;
                    photonView.RPC("SpawnNewObject", PhotonTargets.All, bigAssets[index].name, pos, 2);
                }

                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return null;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            Vector3 dest = player.transform.position;
            dest.y = 10f;
            player.transform.position = dest;
        }
	}

    [PunRPC]
    void SpawnNewObject(string obj, Vector2 pos, int size) {
        Transform parent = size == 0 ? smallAssetsT : size == 1 ? mediumAssetsT : bigAssetsT;
        Instantiate(Resources.Load(obj), new Vector3(pos.x, 0f, pos.y), Quaternion.identity, parent);
    }
}
