﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

    ConvictionController cc;

	// Use this for initialization
	void Start () {
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<ConvictionController>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q")) {
            cc.GenerateConviction(true, 5);
        }
        if (Input.GetKeyDown("w")) {
            cc.GenerateConviction(false, 5);
        }
    }
}
