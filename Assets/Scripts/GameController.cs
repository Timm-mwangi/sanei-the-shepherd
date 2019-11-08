using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public static GameController instance = null;
	private int sheepCount;
	private int coinCount;
	private int totalSheep;
	private GameObject player;
	private bool paused = false;
	private bool gameOver = false;
	private bool gameWon = false;
	private bool gameStarted = false;

	[HideInInspector] public int heartsLeft = 5;
	public Text sheepTxt, coinTxt, gameWonText, leftText;
	public GameObject startMenu, hud, gameOverScreen, pauseMenu, gameWonScreen, damageFlash, storyImage;
	public AudioSource backgroundMusic, collectedAllSheep, notCollectedAllSheep, pickupSheep, pickupCoin;
	public GameObject[] hearts = new GameObject[5];
	private MoveEnvironment envMovement;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
		else{
			Destroy (this);
		}
		startMenu.SetActive(true);
		envMovement = GetComponent<MoveEnvironment>();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
		
	void Start(){
		startMenu.SetActive (true);
		totalSheep = int.Parse(sheepTxt.text);
		Time.timeScale = 0;
		envMovement.enabled = false;
		player.GetComponent<PlayerController>().enabled = gameStarted;
	}
		
	public void Pause(){
		Time.timeScale = 0;
		paused = true;
		pauseMenu.SetActive (true);
		backgroundMusic.Pause ();
	}

	public void Unpause(){
		Time.timeScale = 1;
		paused = false;
		pauseMenu.SetActive (false);
		backgroundMusic.Play ();
	}

	void Update(){
		if(!gameStarted && !startMenu.activeSelf && Input.GetKey(KeyCode.Space)){
			storyImage.SetActive(false);
			GameStart();
		}
		if(Input.GetKeyDown("escape") && gameStarted){
			if(paused){
				Unpause ();
			}
			else{
				Pause ();
			}
		}
		if (Input.GetKeyDown(KeyCode.P) && gameStarted) {
			if (!paused) {
				if (Time.timeScale == 0) {
					Time.timeScale = 1;
					if (!backgroundMusic.isPlaying) {
						backgroundMusic.Play();
					}
				}
				else {
					if (backgroundMusic.isPlaying) {
						backgroundMusic.Pause();
					}
					Time.timeScale = 0;
				}

			}
		}

	
		if(gameOver && Input.GetKeyDown (KeyCode.Escape)){
			gameOverScreen.SetActive (false);
			MainMenu ();
		}

		if(gameWon && Input.GetKeyDown (KeyCode.Escape)){
			gameWon = false;
			sheepTxt.color = Color.black;
			leftText.color = Color.black;
			coinTxt.color = Color.black;
			gameWonScreen.SetActive (false);
			player.GetComponent<PlayerController>().enabled = true;
			Time.timeScale = 1;
		}
	}

	public void MainMenu(){
		SceneManager.LoadScene (0);
	}


	public IEnumerator flashDamage(){
		if(!damageFlash.activeSelf){
			damageFlash.SetActive(true);
			yield return new WaitForSeconds(.1f);
			damageFlash.SetActive(false);
			yield return new WaitForSeconds(.1f);
			damageFlash.SetActive(true);
			yield return new WaitForSeconds(.1f);
		}
		damageFlash.SetActive(false);
	}

	public void StartStory(){
		startMenu.SetActive(false);
		Time.timeScale = 1;
	}

	public void GameStart(){
		envMovement.enabled = true;
		sheepTxt.text = "" + totalSheep;
		coinTxt.text = "" + 0;

		for (int i = 0; i < hearts.Length; ++i) {
			hearts[i].SetActive(true);
		}

		Time.timeScale = 1;
		backgroundMusic.Play();

		hud.SetActive(true);
		gameStarted = true;
		player.GetComponent<PlayerController>().enabled = gameStarted;
	}

	public void removeHeart(){
		if(heartsLeft > 0){
			hearts [--heartsLeft].SetActive (false);
		}
	}

	public void sheepCollected(){
		++sheepCount;
		pickupSheep.Play();
		sheepTxt.text = (totalSheep - sheepCount).ToString();

	}

	public void coinCollected(){
		++coinCount;
		pickupCoin.Play();
		coinTxt.text = coinCount.ToString();
	}

	public void GameWon(){
		sheepTxt.color = Color.white;
		leftText.color = Color.white;
		coinTxt.color = Color.white;
		Time.timeScale = 0;
		player.GetComponent<PlayerController>().enabled = false;
		gameWon = true;
		if(sheepCount==totalSheep){
			collectedAllSheep.Play();
			gameWonText.text = "Congratulations!\n Sanei found all sheep";
			backgroundMusic.volume /= 4;
			gameWonScreen.SetActive (true);
			gameOver = true;
		}
		else{
			notCollectedAllSheep.Play();
			gameWonText.text = "Still " + sheepTxt.text + " sheep left to find";
			gameWonScreen.SetActive (true);
		}
	}


	public void GameOver(){
		sheepTxt.color = Color.white;
		leftText.color = Color.white;
		coinTxt.color = Color.white;
		for(int i=0; i<hearts.Length; ++i){
			hearts [i].SetActive (false);
		}

		backgroundMusic.volume /= 4;
		gameOverScreen.SetActive (true);
		gameOver = true;
	}

	public void QuitGame(){
		Application.Quit ();
	}

}
