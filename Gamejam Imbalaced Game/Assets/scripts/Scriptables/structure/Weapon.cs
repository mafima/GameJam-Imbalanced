using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject {
	public int id;
	public float damage=20, AtkPerSec=1, range=100;
	public GameObject look;
}
