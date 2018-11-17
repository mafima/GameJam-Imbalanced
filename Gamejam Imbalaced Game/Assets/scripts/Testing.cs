using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : Photon.MonoBehaviour {

    ConvictionController cc;

	// Use this for initialization
	void Start () {
        cc = GetComponent<ConvictionController>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!photonView.isMine) {
            return;
        }
        if(!cc)return;
        if (Input.GetKeyDown("q")) {
            cc.GenerateConviction(true, 5);
        }
        if (Input.GetKeyDown("e")) {
            cc.GenerateConviction(false, 5);
        }
    }
}
