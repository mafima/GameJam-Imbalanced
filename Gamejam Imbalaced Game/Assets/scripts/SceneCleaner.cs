using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConvictionController))]
public class SceneCleaner : MonoBehaviour {

    GameObject[] terrains, smallAssets, mediumAssets, bigAssets;

    Transform terrainParent, smallParent, medParent, bigParent;

	void Start () {
        InvokeRepeating("Recurrent", 0f, 3f);
        terrainParent = GameObject.Find("Terrains").transform;
        smallParent = GameObject.Find("SmallAssets").transform;
        medParent = GameObject.Find("MediumAssets").transform;
        bigParent = GameObject.Find("BigAssets").transform;
    }

    void Recurrent() {
        Rebuild();
        Clean();
    }

    void Rebuild() {
        terrains = ParentToList(terrainParent);
        smallAssets = ParentToList(smallParent);
        mediumAssets = ParentToList(medParent);
        bigAssets = ParentToList(bigParent);
    }

    GameObject[] ParentToList(Transform parent) {
        GameObject[] temp = new GameObject[terrainParent.childCount];
        for (int i = 0; i < terrainParent.childCount; i++) {
            temp[i] = terrainParent.GetChild(i).gameObject;
            temp[i].SetActive(true);
        }
        return temp;
    }

    void Clean() {
        DisableList(terrains);
        DisableList(smallAssets);
        DisableList(mediumAssets);
        DisableList(bigAssets);
    }

    void DisableList(GameObject[] list) {
        float dist = GetComponent<ConvictionController>().level * 25f;
        foreach (GameObject obj in list) {
            float xDist, yDist;
            xDist = Mathf.Abs(transform.position.x - obj.transform.position.x);
            yDist = Mathf.Abs(transform.position.y - obj.transform.position.y);
            if (xDist + yDist > dist) {
                obj.SetActive(false);
            }
        }
    }
}
