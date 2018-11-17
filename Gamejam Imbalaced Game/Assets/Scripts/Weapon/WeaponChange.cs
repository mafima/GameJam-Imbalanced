using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : Photon.MonoBehaviour {

	public Weapon weapon;
	public WeaponSystem weaponSystem;
	// Use this for initialization
	void Start () {
		if(!photonView.isMine) this.enabled=false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.mouseScrollDelta.y<0){
			if((weapon.id+1) >= weaponSystem.allweapons.Length) return;
			else weapon.id++;
		}
		else if(Input.mouseScrollDelta.y>0){
			if((weapon.id) >0) return;
			else weapon.id--;
		}
	}
}
