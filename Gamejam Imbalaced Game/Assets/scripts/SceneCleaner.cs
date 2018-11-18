using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConvictionController))]
public class SceneCleaner : MonoBehaviour {

    [SerializeField] float cleaningDistance = 25;

    GameObject[] terrains, smallAssets, mediumAssets, bigAssets;

    Transform terrainParent, smallParent, medParent, bigParent;

    int counter = 0;

	public void StartCleaning () {
        InvokeRepeating("Recurrent", 0f, 3f);
        terrainParent = GameObject.Find("Terrains").transform;
        smallParent = GameObject.Find("SmallAssets").transform;
        medParent = GameObject.Find("MediumAssets").transform;
        bigParent = GameObject.Find("BigAssets").transform;
    }

    void Recurrent() {
        Rebuild();
        Clean();
        counter = counter >= 3 ? 0 : counter + 1;
    }

    void Rebuild() {
        switch (counter) {
            case 0:
                terrains = ParentToList(terrainParent);
                break;
            case 1:
                smallAssets = ParentToList(smallParent);
                break;
            case 2:
                mediumAssets = ParentToList(medParent);
                break;
            case 3:
                bigAssets = ParentToList(bigParent);
                break;
            default:
                break;
        }
    }

    GameObject[] ParentToList(Transform parent) {
        GameObject[] temp = new GameObject[parent.childCount];
        for (int i = 0; i < parent.childCount; i++) {
            temp[i] = parent.GetChild(i).gameObject;
            temp[i].SetActive(true);
        }
        //Debug.Log("Rebuilt " + parent.name + " with " + temp.Length + " children");
        return temp;
    }

    void Clean() {
        switch (counter) {
            case 0:
                DisableList(terrains);
                break;
            case 1:
                DisableList(smallAssets);
                break;
            case 2:
                DisableList(mediumAssets);
                break;
            case 3:
                DisableList(bigAssets);
                break;
            default:
                break;
        }
    }

    void DisableList(GameObject[] list) {
        float dist = GetComponent<ConvictionController>().level.Value * cleaningDistance;
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
