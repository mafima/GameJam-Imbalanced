﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpd : MonoBehaviour {

	public WeaponSystem weaponSystem;
	Weapon lastweapon;

	public Transform onlineWeapons,localWeapons;

	// Use this for initialization
	void Start () {
		if(weaponSystem ==null) this.enabled=false;
		else{
			//lastweapon=weaponSystem.weapon;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(weaponSystem.weapon!=null)return;
			int id = weaponSystem.weapon.id;
			if(lastweapon==null && weaponSystem.weapon.id != lastweapon.id){
				if(lastweapon.id>=0){
					onlineWeapons.GetChild(lastweapon.id).gameObject.SetActive(false);
					localWeapons.GetChild(lastweapon.id).gameObject.SetActive(false);
					}
				localWeapons.GetChild(id).gameObject.SetActive(true);
				onlineWeapons.GetChild(id).gameObject.SetActive(true);
				lastweapon=weaponSystem.weapon;
			}
		
	}
}
