using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController {
	public float jumpForce = 8f;
	public AudioSource playerJump;
	//public AudioSource playerHurt;
	private PolygonCollider2D playerCollider;
	private float shiftCollider = -.30f;
	[HideInInspector]public int playerHealth = 5;
	[HideInInspector]public int playerDamage = 1;
//	private Collider2D[] colliders = new Collider2D[5];

	void Start(){
		moveSpeed = 4f;
		spriteRenderer.flipX = flipSprite;
		playerCollider = GetComponent<PolygonCollider2D>();
	}

	public override void takeDamage(int damage){
		if (playerHealth > 0) {
			//playerHurt.Play();
			playerHealth -= damage;
			StartCoroutine(GameController.instance.flashDamage());
			GameController.instance.removeHeart();
		}
		CheckIfGameOver ();
	}
	void Update(){
		Jump();
	}
	void FixedUpdate(){
		PlayerMove ();	
	}

	private void CheckIfGameOver(){
		if(playerHealth<=0 && !dead){
			animator.SetTrigger("Die");
			dead = true;
			GetComponent<PlayerController>().enabled= false;
			GameController.instance.GameOver ();
		}
	}
		
	private void PlayerMove(){
		float h = Input.GetAxisRaw ("Horizontal");
		if(h > 0){
			playerCollider.offset = new Vector2(shiftCollider, 0.0f);
		}
		else if(h < 0){
			playerCollider.offset = new Vector2(0.0f, 0.0f);
		}
		else{}
		Move (h);
	}

	void Jump(){
		if (grounded) {
			if (Input.GetButtonDown("Jump")) {
				jumping = true;
				playerJump.Play();
				rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
				animator.SetBool("isGrounded", isGrounded());
			}
		}
	}
}
