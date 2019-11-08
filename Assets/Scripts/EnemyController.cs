using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovementController {
	//public BoxCollider2D hitTrigger, bodyCollider;
	[Range(0,5)] public float attackDistance, detectionDistance;
	public AudioSource enemyHurt;

	private GameObject player;
	public GameObject coin;
	private PlayerController playerController;
	private float pTimeAfterLastAttack, pTimeBetweenAttacks = 1f;

	private int enemyDamage = 1;
	private int enemyHealth = 3;
	private float jumpForce = 5f;
	private float timeAfterLastAttack;
	[Range(1,3)]public float timeBetweenAttacks = 1f;

	private Vector2 tempV = Vector2.zero;

	void Start(){
		moveSpeed = 4f;
		spriteRenderer.flipX = flipSprite;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();
	}

	void FixedUpdate(){
		pTimeAfterLastAttack += Time.deltaTime;

		CheckIfDead ();

		if(ShouldMove (ref tempV)){
			EnemyMove ();
		}

		if(CanAttack ()){
			EnemyAttack ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && enemyHealth > 0 && pTimeAfterLastAttack > pTimeBetweenAttacks){
			//Debug.Log ("Trigger Entered");
			pTimeAfterLastAttack = 0f;
			takeDamage (playerController.playerDamage);
			PlayerOnHead ();
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag=="Player")
		PlayerOnHead ();
	}

	private void enemyDie(){
		animator.SetTrigger ("Die");
		Instantiate(coin, transform.position, Quaternion.identity);
		Destroy (gameObject, 3f);
	}

	private void EnemyAttack(){
		timeAfterLastAttack = 0;
		if(dead || playerController.dead){
			return;
		}
		animator.SetTrigger ("attack");
		playerController.takeDamage(enemyDamage);
	}

	private bool CheckIfDead(){
		if(enemyHealth<=0 && !dead){
			dead = true;
			enemyDie ();
		}
		return dead;
	}

	public override void takeDamage(int damage){
		enemyHurt.Play();
		enemyHealth -= damage;
	}

	private void PlayerOnHead(){
		if(dead){
			return;
		}
		float[] directions = {1f,-1f};
		float moveDirection = directions [Random.Range (0, 2)];

		animator.SetBool ("isGrounded", isGrounded ());
		if(!isGrounded ()){
			return;
		}

		rb2d.velocity = new Vector2 (moveDirection*jumpForce, jumpForce);
	
	}

	private bool ShouldMove(ref Vector2 displacement){
		displacement = player.gameObject.transform.position - gameObject.transform.position;

		if(dead || playerController.dead){
			return false;
		}

		if(displacement.sqrMagnitude > Mathf.Pow (detectionDistance, 2f) || displacement.magnitude < attackDistance){
			animator.SetFloat ("speed", 0.0f);
			return false;
		}
			return true;
	}

	private bool CanAttack(){
		Vector2 displacement = player.gameObject.transform.position - gameObject.transform.position;

		if(displacement.magnitude <= attackDistance){
			timeAfterLastAttack += Time.deltaTime;
			if(timeAfterLastAttack > timeBetweenAttacks){
				return true;
			}
		}

		return false;
	}

	private void EnemyMove(){
		float moveDirection;
		Vector2 h =  player.gameObject.transform.position - gameObject.transform.position;

		moveDirection = h.normalized.x;
		Move (moveDirection);
	}	
}
