using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDementor : MonoBehaviour {

	private GameObject leftGoal;
	private GameObject middleGoal;
	private GameObject rightGoal;

	private GameObject player;
	public float moveSpeed;
	public float hitPower;
	private bool bobUpOrDown = false; // false for bob up, true for bob down
	public float bobMovement;
	private Text dementorScoreText;

	private int whichGoal = 0; // 0 for left, 1 for middle, 2 for right 
	private GameObject currentTargetGoal;

	// Use this for initialization
	void Start () {
		leftGoal = GameObject.Find ("LeftGoal");
		middleGoal = GameObject.Find ("MiddleGoal");
		rightGoal = GameObject.Find ("RightGoal");
		dementorScoreText = GameObject.Find ("Dementor Points").GetComponent<Text>();

		whichGoal = Random.Range (0, 3);
		updateTarget ();
		InvokeRepeating ("changeBobMovement", 0.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 targetGoalPosition = new Vector2 (currentTargetGoal.transform.position.x, currentTargetGoal.transform.position.y);
		Vector2 thisDementorPosition = new Vector2 (this.transform.position.x, this.transform.position.y);
		var heading = targetGoalPosition - thisDementorPosition;
		var direction = heading / heading.magnitude;

		int randomSpeedUp = Random.Range(0, 2);
		var horizontalMove = new Vector3 (direction.x * randomSpeedUp, 0, 0);
		this.transform.position += horizontalMove * moveSpeed * Time.deltaTime;

		var verticalMove = new Vector3 (0, direction.y + bobMovement, 0);
		this.transform.position += verticalMove * moveSpeed * Time.deltaTime;

		if (transform.position.x == currentTargetGoal.transform.position.x) {
			dementorScoreText.text = (int.Parse (dementorScoreText.text) + 10).ToString();
			updateTarget ();
		}
	}

	void updateTarget() {
		whichGoal += 1;
		if (whichGoal >= 3) {
			whichGoal = 0;
		}

		if (whichGoal == 0) {
			currentTargetGoal = leftGoal;
		} else if (whichGoal == 1) {
			currentTargetGoal = middleGoal;
		} else {
			currentTargetGoal = rightGoal;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "HarryPotter") {
			attackPlayer ();
		} 
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Goal") {
		}
	}

	void OnTriggerStay2D(Collider2D other) {
//		if (other.gameObject.tag == "Dementor") {
//			int tryToMoveOverOrUnder = Random.Range (20, 50);
//			this.transform.position += Vector3.up * tryToMoveOverOrUnder * moveSpeed * Time.deltaTime;
//		}
	}

	void attackPlayer () {
		player = GameObject.FindGameObjectWithTag ("HarryPotter");
		Vector3 jerkHitMovement;
		if ((player.transform.position.x - this.transform.position.x) > 0) {
			jerkHitMovement = new Vector3 (1f, 1f, 0); // dementor attacking from the left
		} else {
			jerkHitMovement = new Vector3 (-1f, 1f, 0); // dementor attacking from the right
		}
		player.transform.position += jerkHitMovement * hitPower * Time.deltaTime;
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
