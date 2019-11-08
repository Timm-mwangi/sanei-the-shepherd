using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	private Transform fixedCam;
	private Transform player;
	private float distance;
	public float followSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		fixedCam = Camera.main.gameObject.transform;
		if(GameObject.FindGameObjectWithTag ("Player")){
			player = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		else{
			GetComponent<CameraFollow>().enabled = false;
		}

	}

	// Update is called once per frame
	void Update () {
		distance = Mathf.Abs(player.position.x - fixedCam.position.x);
		fixedCam.position = Vector3.Lerp (fixedCam.position, new Vector3(player.position.x, fixedCam.position.y, fixedCam.position.z), followSpeed*distance*Time.deltaTime);
	}
}
