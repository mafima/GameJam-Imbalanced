using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoregetter : MonoBehaviour {
	public GameObject[] plist = new GameObject[99];

	public TextMeshProUGUI t;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		plist = GameObject.FindGameObjectsWithTag("Player");
		string tex="";
		foreach(GameObject p in plist)tex+= 
		p.transform.FindChild("NameCanvas").GetChild(0).GetComponent<NameTag>().name
		+ " Level "+ p.GetComponent<ConvictionController>().level.Value.ToString("0.0") +" "
		+ " Health "+ p.GetComponent<PlayerHealth>().currentHealth.Value.ToString("0.0");
		t.SetText(tex);
	}
}
