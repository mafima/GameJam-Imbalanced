﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : Photon.MonoBehaviour {

	public Weapon weapon;
	//public int id;

	public Weapon[] allweapons;

	// Use this for initialization
	void Start () {
		if(photonView.isMine==false) this.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E) && weapon.id < allweapons.Length+1){
			weapon.id++;
			weapon.setWeapon(allweapons[weapon.id]);
		} else if(Input.GetKeyDown(KeyCode.Q) && weapon.id >0){
			weapon.id--;
			weapon.setWeapon(allweapons[weapon.id]);
		} 
	}
	    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(weapon.id);
        } else if (stream.isReading){
            weapon.id = (int) stream.ReceiveNext();
		}
    }
}
