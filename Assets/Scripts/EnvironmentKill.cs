using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentKill : MonoBehaviour {
	public AudioSource killFX;

	void OnTriggerEnter2D(Collider2D other){
		if ((other.tag == "Player" || other.tag == "Enemy")) {
			killFX.Play();
			if (!other.gameObject.GetComponent<MovementController>().dead){
				other.gameObject.GetComponent<MovementController>().takeDamage(100);
			}
		}
	}
}
