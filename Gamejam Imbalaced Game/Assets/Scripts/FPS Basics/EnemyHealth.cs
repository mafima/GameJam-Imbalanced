﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : Photon.MonoBehaviour {

	public int startingHealth = 100;
	public IntVariable currentHealth;
	public float sinkSpeed = 0.12f;
	public AudioClip deathClip;
	public AudioClip hurtClip;
	public Animator anim;

	private AudioSource playerAudio;
	private FirstPersonController fps;
	private GunShooting playerShooting;
	private CapsuleCollider capsuleCollider;
	private IKControl ikControl;
	private bool isDead;
	private bool isSinking;
	private bool damaged;
	//PlayerScore score;

	public bool invincible = false;
	public Transform Ui;

	// Called when script awake in editor
	void Awake() {
		playerAudio = GetComponent<AudioSource>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		fps = GetComponent<FirstPersonController>();
		ikControl = GetComponentInChildren<IKControl>();
		currentHealth.Value = startingHealth;
	}

	// Update is called once per frame
	void Update() {
		if (damaged) {
		} else {
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

		if (isSinking) {
			transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
		}
	}

	// The RPC function to let the player take damage
	[PunRPC]
	public void TakeDamage(int amount, string enemyName) {
		if (isDead) return;
		if (invincible) return;

		currentHealth.Value -= amount;

		if (photonView.isMine) {
			damaged = true;
			healthSlider.value = currentHealth.Value;
		}

		anim.SetTrigger("IsHurt");

		playerAudio.clip = hurtClip;
		playerAudio.Play();

		if (currentHealth.Value <= 0) {
			Death(enemyName);
		}
	}

	// The RPC function for player death
	[PunRPC]
	void Death(string enemyName) {
		isDead = true;
		capsuleCollider.isTrigger = true;

		playerShooting.DisableEffects();

		anim.SetTrigger("IsDead");

		playerAudio.clip = deathClip;
		playerAudio.Play();

		fps.enabled = false;
		playerShooting.enabled = false;
		ikControl.enabled = false;

		gameObject.transform.Find("NameCanvas/NameTag").gameObject.SetActive(false);

		if (photonView.isMine) {

			if (SendNetworkMessage != null) {
				SendNetworkMessage(PhotonNetwork.player.NickName + " was killed by " + enemyName + "!");
			}

			if (RespawnMe != null) {
				RespawnMe(8.0f);
			}
			StartCoroutine("DestoryPlayer", 7.9f);
		}

		StartCoroutine("StartSinking", 2.5f);
	}

	// The RPC function to destory player game object
	[PunRPC]
	IEnumerator DestoryPlayer(float time) {
		yield return new WaitForSeconds(time);
		PhotonNetwork.Destroy(gameObject);
	}

	// The RPC function to start sink the player game object
	[PunRPC]
	IEnumerator StartSinking(float time) {
		yield return new WaitForSeconds(time);
		GetComponent<Rigidbody>().isKinematic = true;
		isSinking = true;
	}

	// Synchronize data on the network
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext(currentHealth.Value);
		} else {
			currentHealth.Value = (int)stream.ReceiveNext();
		}
	}

}