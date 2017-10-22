using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public int numJumps;
	public int initialNumJumps = 12;
	public int numEnemies;
	public float shotForce;
	private Rigidbody2D rb2d;
	public bool canShoot;
	Vector2 mousePos;
	public float horizForce, vertForce;
	private Camera mainCam;
	public float maxVel;
	public GameObject mouseSprite;
	public Transform spawnPoint;
	public int points;
	public int carrotValue;
	public int lives;
	public Sprite ammo0, ammo1, ammo2, ammo3, ammo4, ammo5, ammo6, ammo7, ammo8, ammo9, ammo10, ammo11, ammo12;
	private Animator anim;

	private Sprite[] sprites;
	private GameObject ammoCount;

	public bool grounded, winner, loser, enemyHit;
	public ParticleSystem enemyExplosion;

	// Use this for initialization
	void Start () {
		numJumps = initialNumJumps;
		canShoot = true;


		rb2d = GetComponent<Rigidbody2D> ();
		mainCam = GameObject.Find ("Main Camera").GetComponent<Camera>();

		points = 0;
		lives = 3;
		transform.position = spawnPoint.position;

		sprites = new Sprite[]{ ammo0, ammo1, ammo2, ammo3, ammo4, ammo5, ammo6, ammo7, ammo8, ammo9, ammo10, ammo11, ammo12 };
		ammoCount = transform.FindChild ("AmmoCountUI").gameObject;
//		ammoCount.GetComponent<SpriteRenderer>().sprite = sprites[initialNumJumps];

		
	}
	
	// If player clicks the mouse button, add force to the player
	// calculate the force added with variable
	// add the force at an angle from the mouse position to the player's position
	// create method to find angle between two positions, and convert it into a vector2
	void Update () {
		ammoCount.GetComponent<SpriteRenderer> ().sprite = sprites [numJumps];

		mousePos = toVector2(mainCam.ScreenToWorldPoint(Input.mousePosition));
		mouseSprite.transform.position = new Vector2 (mousePos.x, mousePos.y);
		Debug.DrawLine (transform.position, mousePos);
		RaycastHit2D line = Physics2D.Linecast (transform.position, mousePos);

		if (line.collider.gameObject.CompareTag ("Enemy")) {
			Debug.Log ("Enemy hit");
			enemyHit = true;

		} 
		else {
			enemyHit = false;
		}

		if (canShoot) {
			if (Input.GetMouseButtonDown (0)) {
				Debug.Log ("Left mouse button clicked");
				if (enemyHit) {
					Instantiate (enemyExplosion, mousePos, Quaternion.identity);
					Destroy (line.collider.gameObject);

				}
				if (numJumps > 0) {
					Vector2 direction = new Vector2 ((transform.position.x + 0.34f) - mousePos.x, (transform.position.y - 0.5f) - mousePos.y).normalized;
					rb2d.velocity = Vector2.zero;
					if (mousePos.y < transform.position.y) {
						horizForce = direction.x;
						vertForce = direction.y;
					} 
					else {
						vertForce = 0;
						if (mousePos.x > transform.position.x) {
							horizForce = -1;
						} 
						else if (mousePos.x <= transform.position.x) {
							horizForce = 1;
						}
					}
						numJumps--;
					Debug.Log("Direction: ( " + horizForce + ", " + vertForce + " )");

					rb2d.AddForce (new Vector2 (horizForce * shotForce, vertForce * shotForce) * rb2d.mass);

					canShoot = false;
					canShoot = true;

				}	
			}
		}

		if (numJumps == 0) {
			if (lives > 0) {
				
				StartCoroutine (DelayRestart ());

			}
			lives--;

		}

		if (lives == 0) {
			Application.Quit ();
			Debug.Log ("Application has quit. Insert gameover functionality");
			//Go to GameOver/Main menu scene

		}
		if (rb2d.velocity.x > maxVel) {
			rb2d.velocity = new Vector2 (maxVel, rb2d.velocity.y);
		}
		if (rb2d.velocity.y > maxVel) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, maxVel);
		}

		anim.SetBool ("grounded",grounded);
		anim.SetBool("winner",winner);
		anim.SetBool("loser",loser);
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Carrot")) {
			points += carrotValue;
			Destroy (col.gameObject);
		} 
		else if (col.CompareTag ("Enemy")) {
			numJumps = 0;

		}
	}

	public Vector2 toVector2(Vector3 vec)
	{
		Vector2 result = new Vector2 (vec.x, vec.y);
		return result;
	}

	private IEnumerator DelayRestart()
	{
		loser = true;
		yield return new WaitForSeconds (1);
		numJumps = initialNumJumps;
		transform.position = spawnPoint.position;

	}

}
