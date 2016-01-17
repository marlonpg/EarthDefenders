using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm;

	// public variables
	public int score=0;

	public float startTime=5.0f;
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;

	public bool gameIsOver = false;

	public bool mainMenu = false;

	public GameObject playAgainButtons;
	public string playAgainLevelToLoad;

	public GameObject nextLevelButtons;
	public string nextLevelToLoad;

	public GameObject restartLevelButtons;
	public string restartLevelToLoad;

	public GameObject quitGameButtons;
	public string quitGame;

	public GameObject scoreboardCanvas;
	public Text scoreboardText;

	private float currentTime;
	private string COUNTRYCODE = "countrycode";

	// setup the game
	void Start () {
		PlayerPrefs.SetString ("whereIAm", "Inicial");
		if (gm == null) 
			gm = this.gameObject.GetComponent<GameManager> ();

		if (mainMenu == false) {
			// set the current time to the startTime specified
			currentTime = startTime;

			// init scoreboard to 0
			mainScoreDisplay.text = "0";

			// inactivate the gameOverScoreOutline gameObject, if it is set
			if (gameOverScoreOutline)
				gameOverScoreOutline.SetActive (false);

			// inactivate the playAgainButtons gameObject, if it is set
			if (playAgainButtons)
				playAgainButtons.SetActive (false);

			// inactivate the nextLevelButtons gameObject, if it is set
			if (nextLevelButtons)
				nextLevelButtons.SetActive (false);

			// inactivate the restartLevelButtons gameObject, if it is set
			if (restartLevelButtons)
				restartLevelButtons.SetActive (false);

			if (quitGameButtons)
				quitGameButtons.SetActive (false);
		} else {

			if (restartLevelButtons)
				restartLevelButtons.SetActive (true);
			
			if (quitGameButtons)
				quitGameButtons.SetActive (true);
		}
	}

	// this is the main game event loop
	void Update () {
		if (mainMenu == false) {
			if (!gameIsOver) {

                if (currentTime < 0) { // check to see if timer has run out
					EndGame ();
				} else { // game playing state, so update the timer
					currentTime -= Time.deltaTime;
					mainTimerDisplay.text = currentTime.ToString ("0.00");	
				}
			}
		} 
	}

	void EndGame() {
		gameIsOver = true;

		mainTimerDisplay.text = "GAME OVER";

		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

		if (mainScoreDisplay.text != "0") {
			scoreboardText.text = mainScoreDisplay.text;
			mainScoreDisplay.gameObject.SetActive (false);
			mainTimerDisplay.gameObject.SetActive (false);
			gameOverScoreOutline.gameObject.SetActive (false);
			scoreboardCanvas.gameObject.SetActive (true);
			StartCoroutine(GetCountryCode());
            mainScoreDisplay.gameObject.transform.parent.gameObject.SetActive(false);


        } else {
			
			if (playAgainButtons)
				playAgainButtons.SetActive (true);

			if (restartLevelButtons)
				restartLevelButtons.SetActive (true);

			if (nextLevelButtons)
				nextLevelButtons.SetActive (true);

			if (quitGameButtons)
				quitGameButtons.SetActive (true);

			if (musicAudioSource)
				musicAudioSource.pitch = 0.5f; // slow down the music
		
		}
	}

	IEnumerator GetCountryCode(){
		WWW myExtIPWWW = new WWW("http://ipinfo.io/country");
		yield return myExtIPWWW;
		if (myExtIPWWW.error != null) {
			Debug.Log ("There was an error getting the high score: " + myExtIPWWW.error);
		} else {
			var myCountry = myExtIPWWW.text;
			PlayerPrefs.SetString (COUNTRYCODE, myCountry);
		}
	}

	public void targetHit (int scoreAmount, float timeAmount)
	{
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString ();

		currentTime += timeAmount;
		
		if (currentTime < 0)
			currentTime = 0.0f;

		mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

	public void RestartGame ()
	{
        SceneManager.LoadScene(restartLevelToLoad);
	}

	public void NextLevel ()
	{
		SceneManager.LoadScene(nextLevelToLoad);
    }

	public void PlayAgain ()
	{
        SceneManager.LoadScene(playAgainLevelToLoad);
    }

	public void QuitGame() {
		Application.Quit();
	}

}
