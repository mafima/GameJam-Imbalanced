using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpd : Photon.MonoBehaviour {

	public Weapon weapon,lastweapon;

	public Transform onlineWeapons,localWeapons;

	// Use this for initialization
	void Start () {
		if(!GetComponent<PhotonView>().isMine && weapon ==null) this.enabled=false;
		else{
			//lastweapon=weaponSystem.weapon;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(weapon==null)return;
		int id = weapon.id;
		if(id != lastweapon.id){

				if(lastweapon && lastweapon.id>=0){
					onlineWeapons.GetChild(lastweapon.id).gameObject.SetActive(false);
					localWeapons.GetChild(lastweapon.id).gameObject.SetActive(false);
					}
				if (localWeapons.childCount>=id)localWeapons.GetChild(id).gameObject.SetActive(true);
				if (onlineWeapons.childCount>=id)onlineWeapons.GetChild(id).gameObject.SetActive(true);

				lastweapon.id=id;
		}
		
	}
}
