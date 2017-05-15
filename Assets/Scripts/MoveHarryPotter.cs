using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHarryPotter : MonoBehaviour {

	public Rigidbody2D rb;
	public float walkingSpeed;
	public float jumpSpeed;

	private Animator animator;
	public GameObject attackDementor;
	public GameObject scoringDementor;

	public GameObject leftPatronus;
	public GameObject rightPatronus;

	private float lastDirection = 0; // -1 for left, 1 for right
	private bool fellToGroundYet = true;
	private int lastSeenSnitchPlacement;

	private bool bobUpOrDown = false; // false for bob up, true for bob down
	private float bobMovement = 1f;
	private bool isBobbing = false;

	private SpriteRenderer healthBar;
	private Vector3 healthScale;
	public float health = 100f;					// The player's health.

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
		InvokeRepeating ("changeBobMovement", 0.0f, 0.5f);
//		InvokeRepeating ("SpawnDementor", 2.0f, 5.0f);
		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		healthScale = healthBar.transform.localScale;
		Invoke ("SpawnAttackDementor", 0.0f);
//		Invoke ("SpawnScoringDementor", 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = lastDirection;
		if (!animator.GetBool ("fallingState") && !animator.GetBool("floatingState")) {
			if (Input.GetKeyDown (KeyCode.E) && horizontalInput <= 0) {
				animator.SetBool ("leftAttackState", true);
				Invoke ("LeftPatronusCharm", 1.0f);
			} else if (Input.GetKeyDown (KeyCode.E) && horizontalInput > 0) {
				animator.SetBool ("rightAttackState", true);
				Invoke ("RightPatronusCharm", 1.0f);
			}
		}
	}

	void FixedUpdate () 
	{
		RaycastHit2D[] hit2 = Physics2D.RaycastAll (new Vector2 (transform.position.x, transform.position.y - 2f), 
			Vector2.down, 100f, Physics.DefaultRaycastLayers, -Mathf.Infinity, Mathf.Infinity);
		GameObject platform = null;
		for (int i = 0; i < hit2.Length; i++) {
			if (hit2 [i].collider.gameObject.tag == "platform") {
				platform = hit2 [i].collider.gameObject;
			}
		}

		if (fellToGroundYet) {
			float horizontalInput = Input.GetAxis ("Horizontal");
			float verticalInput = Input.GetAxis ("Vertical");
			if (Input.GetKey (KeyCode.Space)) {
				verticalInput = 1f;
			}

			var verticalMove = new Vector2 (0, verticalInput);
			transform.Translate (verticalMove * jumpSpeed * Time.deltaTime);
			var horizontalMove = new Vector2 (horizontalInput, 0);
			if (platform != null && this.transform.position.y <= platform.transform.position.y + 4f) { // walking
				transform.Translate (horizontalMove * walkingSpeed * 0.5f * Time.deltaTime);
			} else { // flying horizontally
				if (animator.GetBool ("floatingState") && verticalInput == 0) { // flying horizontally only
					transform.Translate (horizontalMove * 1.5f * walkingSpeed * Time.deltaTime);
				} else { // flying horizontally and vertically then
					transform.Translate (horizontalMove * walkingSpeed * Time.deltaTime);
				}
			}

			animator.SetFloat ("xInput", horizontalInput);
			animator.SetFloat ("yInput", verticalInput);
			if (horizontalInput != 0) {
				lastDirection = horizontalInput;
			} else {
				animator.SetFloat ("xInput", lastDirection);
				if (animator.GetBool ("floatingState")) {
					animator.SetFloat ("yInput", 1);
				}
			}

			// must be near or on ground
			if (platform != null && this.transform.position.y <= platform.transform.position.y + 4f) {
				isBobbing = false;
				if (verticalInput <= 0) {
					transform.Translate (Physics2D.gravity * 0.1f * Time.deltaTime);
					if (animator.GetBool ("floatingState")) {
						animator.SetBool ("fallingState", true);
						animator.SetBool ("floatingState", false);
					}
//					if (animator.GetBool ("flyingState")) {
//						animator.SetBool ("fallingState", true);
//						animator.SetBool ("flyingState", false);
//					}

					if (horizontalInput == 0) {
						if (!animator.GetBool ("idleState")) {
							animator.SetBool ("idleState", true);
							animator.SetBool ("walkingState", false);
						}
					} else if (horizontalInput != 0) {
						if (!animator.GetBool ("walkingState")) {
							animator.SetBool ("walkingState", true);
							animator.SetBool ("idleState", false);
						}
					}
				} else if (verticalInput > 0) {
					if (!animator.GetBool ("idleState")) {
						animator.SetBool ("idleState", true);
						animator.SetBool ("walkingState", false);
					}
					if (!animator.GetBool ("floatingState")) {
						animator.SetBool ("floatingState", true);
						animator.SetBool ("fallingState", false);
					}
				}
			} 
			// must be above ground 
			else if (platform != null) {
				if (animator.GetBool ("fallingState")) {
					animator.SetBool ("fallingState", false);
				}
				if (verticalInput == 0 && horizontalInput == 0) {
//					if (animator.GetBool ("flyingState")) {
//						animator.SetBool ("flyingState", false);
//						animator.SetBool ("floatingState", true);
//					}
					isBobbing = true;
					transform.position += (new Vector3(0, 1, 0) * bobMovement * Time.deltaTime);
				} else {
//					if (horizontalInput != 0) {
//						if (animator.GetBool ("floatingState")) {
//							animator.SetBool ("floatingState", false);
//							animator.SetBool ("flyingState", true);
//						}
//					} else if (horizontalInput == 0) {
//						if (animator.GetBool ("flyingState")) {
//							animator.SetBool ("flyingState", false);
//							animator.SetBool ("floatingState", true);
//						}
//					}
					isBobbing = false;
				}
			} 
			// nothing is underneath the player (player just falling)
			else if (platform == null) {
				if (verticalInput == 0 && horizontalInput == 0) {
					isBobbing = true;
					transform.position += (new Vector3 (0, 1, 0) * bobMovement * Time.deltaTime);
				} else {
					isBobbing = false;
				}
				if (!animator.GetBool ("floatingState")) {
					if (!animator.GetBool ("idleState")) {
						animator.SetBool ("idleState", true);
						animator.SetBool ("walkingState", false);
					}
					animator.SetBool ("floatingState", true);
					animator.SetBool ("fallingState", false);
				}
			}
		} else {
			transform.Translate (Physics2D.gravity * 0.5f * Time.deltaTime);
		}
	}

	void SpawnAttackDementor() {
		Instantiate (attackDementor);
	}

	void SpawnScoringDementor() {
		Instantiate (scoringDementor);
	}

	void LeftPatronusCharm() {
		Vector3 patronusPosition = this.transform.position + new Vector3 (-2, 0, 0);
		Instantiate (leftPatronus, patronusPosition, this.transform.rotation);
		animator.SetBool ("leftAttackState", false);
	}

	void RightPatronusCharm() {
		Vector3 patronusPosition = this.transform.position + new Vector3 (2, 0, 0);
		Instantiate (rightPatronus, patronusPosition, this.transform.rotation);
		animator.SetBool ("rightAttackState", false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "SnitchPlacements") {
			lastSeenSnitchPlacement = System.Convert.ToInt32(other.gameObject.name.Substring (15));
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "dementor") {
			if (animator.GetBool ("floatingState")) {
				fellToGroundYet = false;
				isBobbing = false;
				animator.SetBool ("fallingState", true);
				animator.SetBool ("floatingState", false);
			}
//			if (animator.GetBool ("flyingState")) {
//				fellToGroundYet = false;
//				isBobbing = false;
//				animator.SetBool ("fallingState", true);
//				animator.SetBool ("flyingState", false);
//			}

			health -= 10;
			// Set the health bar's colour to proportion of the way between green and red based on the player's health.
			healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
			// Set the scale of the health bar to be proportional to the player's health.
			healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 0.5f, 1);

			// when dementor hits Harry and he falls off his broom, 
			// if a rock isn't underneath him, then allow him to fly again after X seconds
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 2f), Vector2.down);
			if (hit.collider == null) {
				Invoke ("CanFlyAgain", 1f);
			}
		}
		if (coll.gameObject.tag == "platform") {
			if (animator.GetBool ("fallingState")) {
				animator.SetBool ("fallingState", false);
			}
			fellToGroundYet = true;
		} 
	}

	void CanFlyAgain() {
		if (animator.GetBool ("fallingState")) {
			animator.SetBool ("fallingState", false);
		}
		fellToGroundYet = true;
	}

	public bool playerIsBobbing() {
		return isBobbing;
	}

	public int lastSeen() {
		return lastSeenSnitchPlacement;
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
