using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour {

	public Weapon weapon;

	public Transform spawnhere;
	// Use this for initialization
	void Start () {
		if (spawnhere.childCount==0)Instantiate(weapon.look,spawnhere.position,Quaternion.identity,spawnhere);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
