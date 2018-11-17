using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpd : MonoBehaviour {

	public WeaponSystem weaponSystem;
	Weapon lastweapon;

	public Transform weapons;

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
		
			if(lastweapon==null && weaponSystem.weapon.id != lastweapon.id){
				if(lastweapon.id>=0)weapons.GetChild(lastweapon.id).gameObject.SetActive(false);
				weapons.GetChild(weaponSystem.weapon.id).gameObject.SetActive(true);
				lastweapon=weaponSystem.weapon;
			}
		
	}
}
