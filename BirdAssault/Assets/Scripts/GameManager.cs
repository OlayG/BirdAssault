using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public Text continueText;
	public Text scoreText;

	private float timeElapsed = 0f;
	private float bestTime = 0f;
	private float blinkTime = 0f;
	private bool blink;
	private bool gameStarted;
	private TimeManager timeManager;
	private GameObject player;
	private GameObject floor;
    private GameObject midground;
    private Spawner spawner;
    private Spawner spawner1;
    private Spawner spawner2;
	private bool beatBestTime;

	void Awake(){
        timeManager = GetComponent<TimeManager> ();
	}

	// Use this for initialization
	void Start () {

		Time.timeScale = 0;

		continueText.text = "PRESS ANY BUTTON TO START";

		bestTime = PlayerPrefs.GetFloat ("BestTime");
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted && Time.timeScale == 0) {

			if(Input.anyKeyDown){

				timeManager.ManipulateTime(1, 1f);
				ResetGame();
			}
		}

		if (!gameStarted) {
			blinkTime ++;

			if (blinkTime % 40 == 0) {
				blink = !blink;
			}

			continueText.canvasRenderer.SetAlpha (blink ? 0 : 1);

			var textColor = beatBestTime ? "#FFF" : "#FFF";

			scoreText.text = "TIME: " + FormatTime (timeElapsed) + "\n<color="+textColor+">BEST: " + FormatTime (bestTime)+"</color>";
		} else {
			timeElapsed += Time.deltaTime;
			scoreText.text = "TIME: "+FormatTime(timeElapsed);
		}


		if (timeElapsed > bestTime) {
			bestTime = timeElapsed;
			PlayerPrefs.SetFloat("BestTime", bestTime);
			beatBestTime = true;
		}
	}

	void OnPlayerKilled(){

		gameStarted = false;

		continueText.text = "PRESS ANY BUTTON TO RESTART";

	}

	void ResetGame(){
		gameStarted = true;

		continueText.canvasRenderer.SetAlpha(0);

		timeElapsed = 0;
		beatBestTime = false;
	}

	string FormatTime(float value){
		TimeSpan t = TimeSpan.FromSeconds (value);

		return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
	}

}
