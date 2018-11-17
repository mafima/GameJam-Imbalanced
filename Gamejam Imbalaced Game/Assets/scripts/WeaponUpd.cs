using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpd : MonoBehaviour {

	public WeaponSystem weaponSystem;
	// Use this for initialization
	void Start () {
		if(weaponSystem ==null) this.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
		if(weaponSystem!=null && weaponSystem.weapon!=null){
		}
	}
}
