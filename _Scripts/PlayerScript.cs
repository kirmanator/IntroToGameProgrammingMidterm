using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public int numJumps;
	public int initialNumJumps = 20;
	public float shotForce;
	private Rigidbody2D rb2d;
	public bool canShoot;
	Vector2 mousePos;
	float horizForce, vertForce;
	private Camera mainCam;
	public float maxVel;
	public GameObject mouseSprite;
	public Transform spawnPoint;
	public int points;
	public int carrotValue;
	public int lives;

	// Use this for initialization
	void Start () {
		numJumps = initialNumJumps;
		canShoot = true;

		rb2d = GetComponent<Rigidbody2D> ();
		mainCam = GameObject.Find ("Main Camera").GetComponent<Camera>();
		points = 0;
		lives = 3;
		transform.position = spawnPoint.position;

		
	}
	
	// If player clicks the mouse button, add force to the player
	// calculate the force added with variable
	// add the force at an angle from the mouse position to the player's position
	// create method to find angle between two positions, and convert it into a vector2
	void Update () {
		mousePos = toVector2(mainCam.ScreenToWorldPoint(Input.mousePosition));
		mouseSprite.transform.position = new Vector2 (mousePos.x, mousePos.y);
		Debug.DrawLine (transform.position, mousePos);
		RaycastHit2D line = Physics2D.Linecast (transform.position, mousePos);

		if (line.collider.gameObject.CompareTag ("Enemy")) {
			Debug.Log ("Enemy hit");

		}

		if (canShoot) {
			if (Input.GetMouseButtonDown (0)) {
				Debug.Log ("Left mouse button clicked");

				if (numJumps > 0) {
					if (mousePos.y >= transform.position.y) {
						vertForce = 0;
						if (mousePos.x < transform.position.x) {
							horizForce = 1;
							Debug.Log ("mouse X");
						} 
						else {
							horizForce = -1;
						}

					} 
					else {
						vertForce = Mathf.Sin (Vector2.Angle (mousePos, toVector2(transform.position)));
						horizForce = Mathf.Cos (Vector2.Angle (mousePos, toVector2(transform.position)));

						numJumps--;
					}
					//Debug.Log ("VertForce: " + vertForce);
					Debug.Log ("Angle: " + Vector2.Angle (mousePos, transform.position));
//					Debug.Log ("HorizForce: " + horizForce); 
//					Debug.Log ("MousePos: " + mousePos);
//					Debug.Log ("Player Pos: " + transform.position);
					rb2d.AddForce (new Vector2 (horizForce * shotForce, vertForce * shotForce).normalized);

					canShoot = false;
					canShoot = true;

		
				}	
			}
		}

		if (numJumps == 0) {
			if (lives > 0) {
				StartCoroutine (DelayRestart ());
			}

		}

		if (lives == 0) {
			Application.Quit ();
			Debug.Log ("Application has quit. Insert gameover functionality");
		}
		if (rb2d.velocity.x > maxVel) {
			rb2d.velocity = new Vector2 (maxVel, rb2d.velocity.y);
		}
		if (rb2d.velocity.y > maxVel) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, maxVel);
		}
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Carrot")) {
			points += carrotValue;
			Destroy (col.gameObject);
		}
	}

	public Vector2 toVector2(Vector3 vec)
	{
		Vector2 result = new Vector2 (vec.x, vec.y);
		return result;
	}

	private IEnumerator DelayRestart()
	{
		yield return new WaitForSeconds (1);
		numJumps = initialNumJumps;
		transform.position = spawnPoint.position;
		lives--;

	}

}
