using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public float speed = 1;
	public bool isAlive;
	Rigidbody2D rb2d;
	Transform enemyPosition;
	float enemyWidth, enemyHeight;
	public LayerMask enemyMask;
	private Animator anim;
	public Color debugLine;

	// Use this for initialization
	void Start () {
		isAlive = true;
		rb2d = this.GetComponent<Rigidbody2D> ();
		enemyPosition = this.transform;
		//use 'this' here because we're not trying to access the general gameobject
		//rather, we're trying to access the sprite itself
		SpriteRenderer sprite = this.GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();

		//finds the width and height of sprite by finding x or y coordinates on the bounds of the image
		enemyWidth = sprite.bounds.extents.x;
		enemyHeight = sprite.bounds.extents.y;
		anim.SetBool ("alive",isAlive);
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		//checking ground in front of enemy
		//Debug.DrawLine draws the line in the scene but not the game
		Vector2 lineCastPos = toVector2(enemyPosition.position) - (toVector2(enemyPosition.right) * enemyWidth) + (Vector2.up * enemyHeight);
		Debug.DrawLine (lineCastPos, lineCastPos + (Vector2.down*2));
		bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + (Vector2.down*2), enemyMask);
		//Debug.Log ("isGrounded: " + isGrounded);

		//always move the enemy forward
		Vector2 velocity = rb2d.velocity;
		velocity.x = enemyPosition.right.x * -speed;
		rb2d.velocity = velocity;
		//Debug.Log ("rb2d.velocity: " + rb2d.velocity);
		//Debug.Log ("isAlive: " + isAlive);

		if (!(isGrounded)) {
			Vector3 rotation = enemyPosition.eulerAngles; //property of rotation
			rotation.y += 180; //rotate current sprite 180 degrees
			enemyPosition.eulerAngles = rotation;
		} 
		else {

		}
	}

	public Vector2 toVector2(Vector3 vec3)
	{
		Vector2 result = new Vector2 (vec3.x, vec3.y);
		return result;
	}
}
