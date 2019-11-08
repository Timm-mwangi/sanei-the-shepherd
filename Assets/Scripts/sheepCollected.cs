using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheepCollected : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			GameController.instance.sheepCollected ();
			Destroy(gameObject);
		}
	}
}
