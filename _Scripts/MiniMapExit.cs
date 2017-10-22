using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MiniMapExit : MonoBehaviour, IPointerClickHandler{

	public MouseOver minimapButton;

	private Camera minimapCam;
	public Sprite hover, nonHover;
	public bool mouseOver;
	private PlayerScript player;

	// Use this for initialization
	void Start () {
		minimapCam = GameObject.Find ("Minimap").GetComponent<Camera> ();
		player = GameObject.Find ("Player").GetComponent<PlayerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (minimapButton.Minimap == true) {
			minimapCam.enabled = true;
			GetComponent<Image> ().enabled = true;
		} 
		else {
			minimapCam.enabled = false;
			GetComponent<Image> ().enabled = false;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		minimapButton.Minimap = false;
		minimapButton.GetComponent<Image> ().enabled = true;
		player.canShoot = true;
	}
}
