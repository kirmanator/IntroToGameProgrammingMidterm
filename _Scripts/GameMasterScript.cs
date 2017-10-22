using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMasterScript : MonoBehaviour {

	public Text pointsUI;
	public PlayerScript player;
	public Image minimap, minimapExit, lives, pause, pauseOverlay;
	public Sprite lives3, lives2, lives1, lives0;
	private Sprite[] lifeSprites;

	//public bool paused;

	// Use this for initialization
	void Start () {
		minimapExit.GetComponent<Image> ().enabled = false;
		lifeSprites = new Sprite[]{ lives0, lives1, lives2, lives3 };
		pause.GetComponent<Image> ().enabled = false;
		pauseOverlay.GetComponent<Image> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		pointsUI.text = "Points: " + player.points;
		lives.sprite = lifeSprites [player.lives];

		//Edit: 
		//For some reason, you can only pause when mouse is outside of the screen? 
		// Pause works when pressing P, but when mouse is on screen, unpause does not work

		if (Input.GetKeyDown(KeyCode.P)) {
			//if not paused, pause the game
			if (Time.timeScale == 1) {
				pause.GetComponent<Image> ().enabled = true;
				pauseOverlay.GetComponent<Image> ().enabled = true;
				Time.timeScale = 0;
			} 
			//if paused, unpause the game
			else {
				pause.GetComponent<Image> ().enabled = false;
				pauseOverlay.GetComponent<Image> ().enabled = false;
				Time.timeScale = 1;
			}
		}

	}
}
