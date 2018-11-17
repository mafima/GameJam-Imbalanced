using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : Photon.MonoBehaviour {

	public Weapon weapon;
	//public int id;
	// Use this for initialization
	void Start () {
		if(! photonView.isMine) this.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E)){
			weapon.id++;
		} else if(Input.GetKeyDown(KeyCode.Q) && weapon.id >0){
			weapon.id--;
		} 
	}
	    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(weapon.id);
        } else {
            weapon.id = (int) stream.ReceiveNext();
		}
    }
}
