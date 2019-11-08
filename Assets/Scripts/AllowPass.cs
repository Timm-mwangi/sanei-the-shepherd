using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowPass : MonoBehaviour {
	private PolygonCollider2D polyCollider;
	private bool disabled = false;
	void Start(){
		polyCollider = GetComponent<PolygonCollider2D>();
	}
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay2D(Collision2D other){{
			if(other.collider.tag =="Player" && other.gameObject.transform.position.y > gameObject.transform.position.y){
				CheckToDisable();
			}
		}
		
	}
	void CheckToDisable(){
		if(Input.GetAxisRaw("Vertical")==-1 && !disabled){
			DisableCollider();
		}
	}
	void DisableCollider(){
		polyCollider.enabled = false;
		disabled = true;
		StartCoroutine(EnableCollider());
	}

	IEnumerator EnableCollider(){
		yield return new WaitForSeconds(1f);
		polyCollider.enabled = true;
		disabled = false;
	}
}
