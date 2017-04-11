using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementorAI : MonoBehaviour {

	public float distanceForCollisionX;
	public float distanceForCollisionY;
	private GameObject player;
	private float playerDistanceX;
	private float playerDistanceY;
	public float moveSpeed;
	public float hitPower;
	private bool bobUpOrDown = false; // false for bob up, true for bob down
	public float bobMovement;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("changeBobMovement", 0.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("HarryPotter");
		playerDistanceX = player.transform.position.x - this.transform.position.x;
		playerDistanceY = player.transform.position.y - this.transform.position.y;
		if (Mathf.Abs(playerDistanceX) < distanceForCollisionX && Mathf.Abs(playerDistanceY) < distanceForCollisionY) {
			attackPlayer ();
		} else {
			chasePlayer (); 
		}
	}

	void chasePlayer () {
		var horizontalMove = new Vector3 (playerDistanceX, 0, 0);
		this.transform.position += horizontalMove * moveSpeed * Time.deltaTime;

		var verticalMove = new Vector3 (0, playerDistanceY + bobMovement, 0);
		this.transform.position += verticalMove * moveSpeed * Time.deltaTime;
	}

	void attackPlayer () {
		var jerkMovement = new Vector3 (1f, 1f, 0);
		player.transform.position += jerkMovement * hitPower * Time.deltaTime;
	}

	void changeBobMovement() {
		if (bobUpOrDown) {
			bobMovement *= -1;
			bobUpOrDown = false;
		} else {
			bobMovement *= -1;
			bobUpOrDown = true;
		}
	}
}
