using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	public float gravityModifier = 1f;

	public float moveSpeed;
	public bool flipSprite;
	bool flip = false;
	[HideInInspector]public bool dead = false;

	protected Rigidbody2D rb2d;
	protected SpriteRenderer spriteRenderer;
	protected Animator animator;

	protected bool grounded;
	protected bool jumping;

	void Awake(){
		rb2d = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent <SpriteRenderer> ();
		animator = GetComponent <Animator> ();
	}
		
	void OnCollisionEnter2D(Collision2D other){
		if(other.collider.tag=="Ground" || other.collider.tag=="Platform"){
			animator.SetBool ("isGrounded", true);
			grounded = true;
			jumping = false;
		}
	}

	void OnCollisionStay2D(Collision2D other){
		if(other.collider.tag=="Ground" || other.collider.tag=="Platform"){
			animator.SetBool ("isGrounded", true);
			grounded = true;
			jumping = false;
		}
	}
	void OnCollisionExit2D(Collision2D other){
		if (other.collider.tag == "Ground" || other.collider.tag=="Platform") {
			animator.SetBool("isGrounded", false);
			grounded = false;
		}
	}



	public virtual void takeDamage(int damage){}

	protected bool isGrounded(){
		return grounded;
	}

	protected void Move(float xDir){		
		if(flipSprite){
			if(xDir>0){
				flip = true;
			}
			else if(xDir<0){
				
				flip = false;
			}
			else{}
		}
		else{
			if(xDir>0){
				flip = false;
			}
			else if(xDir<0){
				flip = true;
			}
			else{}
		}
					
		spriteRenderer.flipX = flip;

		if (isGrounded()) {
			animator.SetBool ("isGrounded", isGrounded ());
			animator.SetFloat("speed", Mathf.Abs(xDir));
		}

		Vector2 newPosition = new Vector2 (xDir * moveSpeed * Time.deltaTime,0.0f);// + gravityMove ();

		rb2d.position += newPosition;

	}
		
}
