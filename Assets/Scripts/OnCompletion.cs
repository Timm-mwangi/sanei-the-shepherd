using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCompletion : MonoBehaviour {

	public void CallGameStart(){
		gameObject.SetActive(false);
		GameController.instance.GameStart();
	}
}
