using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterScript : MonoBehaviour {

	public Text pointsUI, livesUI;
	public PlayerScript player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		pointsUI.text = "Points: " + player.points;
		livesUI.text = "Number of lives: " + player.lives;
	}
}
