using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public Sprite hover, nonHover;
	private PlayerScript player;
	/*
		Used code from gamedev.stackexchange.com, searching how to detect mouse over UI Image
	*/

	public bool mouseOver;
	public bool Minimap;

	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<PlayerScript> ();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		GetComponent<Image> ().sprite = hover;
		mouseOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		GetComponent<Image> ().sprite = nonHover;
		mouseOver = false;
	}	

	public void OnPointerClick(PointerEventData eventData)
	{
		if (mouseOver) {
			GetComponent<Image> ().sprite = nonHover;
			Minimap = true;
			GetComponent<Image> ().enabled = false;
			player.canShoot = true;
		}
	}
}
