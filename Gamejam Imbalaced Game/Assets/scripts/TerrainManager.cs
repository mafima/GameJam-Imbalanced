using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    int worldSize = 10;
    GameObject[,] ground;
    public GameObject groundPrefab;
    

	// Use this for initialization
	void Start () {
        ground = new GameObject[worldSize, worldSize];
        Transform terrain = new GameObject("Terrains").GetComponent<Transform>();
        for (int i = 0; i < worldSize; i++) {
            for (int j = 0; j < worldSize; j++) {
                ground[i, j] = Instantiate(groundPrefab, new Vector3( (i - worldSize / 2) * 500, 0f, (j - worldSize / 2) * 500), groundPrefab.transform.rotation, terrain);
            }
        }
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
