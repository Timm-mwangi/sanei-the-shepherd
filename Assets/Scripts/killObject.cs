using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killObject : MonoBehaviour {
	void OnTriggerExit2D(Collider2D other){
		if(other.tag=="Environment Movables" || other.tag=="Coin"){
			Destroy(other.gameObject);
		}
	}
}
