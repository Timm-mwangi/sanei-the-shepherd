using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveEnvironment : MonoBehaviour {
	public GameObject[] environmentObjects = new GameObject[4];
	private GameObject toInstantiate, instantiated = null;
	private float instantiateWait;
	private float timeWaited;
	public GameObject start, end;
	private int speed;

	void Start(){
		instantiateWait = Random.Range(15f, 25f);
	}

	void Update () {
		timeWaited += Time.deltaTime;

		if (timeWaited >= instantiateWait && instantiated==null) {
			timeWaited = 0f;
			instantiateWait = Random.Range(15f, 25f);
			toInstantiate = environmentObjects[Random.Range (0, environmentObjects.Length)];
			instantiated = Instantiate (toInstantiate, start.transform.position, Quaternion.identity);
			speed = Random.Range(2, 6);
		}

		if(instantiated != null){
			instantiated.transform.Translate(Vector3.left*speed*Time.deltaTime);

		}

	}
}
