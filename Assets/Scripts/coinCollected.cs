using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollected : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			GameController.instance.coinCollected ();
			gameObject.SetActive (false);
			Destroy(gameObject);
		}
	}
}
