using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHarryPotter : MonoBehaviour {

	public Rigidbody2D rb;
	public float walkingSpeed;
	public float jumpSpeed;

	private Animator animator;
	public GameObject dementor;

	public GameObject leftPatronus;
	public GameObject rightPatronus;

	private int lastDirection = 0; // 0 for left, 1 for right
	private bool fellToGroundYet = true;
	private int lastSeenSnitchPlacement;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
//		InvokeRepeating ("SpawnDementor", 2.0f, 5.0f);
//		Invoke ("SpawnDementor", 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = Input.GetAxis ("Horizontal");
		if (Input.GetKeyDown (KeyCode.E) && horizontalInput < 0) {
			animator.SetTrigger ("leftAttackState");
			Invoke ("LeftPatronusCharm", 1.0f);
		} else if (Input.GetKeyDown (KeyCode.E) && horizontalInput > 0) {
			animator.SetTrigger ("rightAttackState");
			Invoke ("RightPatronusCharm", 1.0f);
		}
	}

	void FixedUpdate () 
	{
		if (fellToGroundYet) {
			float horizontalInput = Input.GetAxis ("Horizontal");
			float verticalInput = Input.GetAxis ("Vertical");
			if (Input.GetKey (KeyCode.Space)) {
				verticalInput = 1f;
			}

			var horizontalMove = new Vector2 (horizontalInput, 0);
			transform.Translate (horizontalMove * walkingSpeed * Time.deltaTime);
			var verticalMove = new Vector2 (0, verticalInput);
			transform.Translate (verticalMove * jumpSpeed * Time.deltaTime);
			animator.SetFloat ("xInput", horizontalInput);
			animator.SetFloat ("yInput", verticalInput);

			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - 2f), Vector2.down);
			// must be near or on ground
			if (hit.collider != null && hit.collider.gameObject.tag == "platform"
			    && this.transform.position.y <= hit.collider.gameObject.transform.position.y + 6f) {
				if (verticalInput <= 0) {
					transform.Translate (Physics2D.gravity * 0.1f * Time.deltaTime);
					if (animator.GetBool ("flyingState")) {
						animator.SetBool ("fallingState", true);
						animator.SetBool ("flyingState", false);
					}

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
					if (!animator.GetBool ("flyingState")) {
						animator.SetBool ("flyingState", true);
						animator.SetBool ("fallingState", false);
					}
				}
			} 
			// must be above ground 
			else if (hit.collider != null && hit.collider.gameObject.tag == "platform") {
				
			} 
			// nothing is underneath the player (player just falling)
			else if (hit.collider == null) {
				if (!animator.GetBool ("flyingState")) {
					if (!animator.GetBool ("idleState")) {
						animator.SetBool ("idleState", true);
						animator.SetBool ("walkingState", false);
					}
					animator.SetBool ("flyingState", true);
					animator.SetBool ("fallingState", false);
				}
			}
		} else {
			transform.Translate (Physics2D.gravity * 0.5f * Time.deltaTime);
		}
	}

	void SpawnDementor() {
		Instantiate (dementor);
	}

	void LeftPatronusCharm() {
		Vector3 patronusPosition = this.transform.position + new Vector3 (-2, 0, 0);
		Instantiate (leftPatronus, patronusPosition, this.transform.rotation);
	}

	void RightPatronusCharm() {
		Vector3 patronusPosition = this.transform.position + new Vector3 (2, 0, 0);
		Instantiate (rightPatronus, patronusPosition, this.transform.rotation);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "dementor") {
			if (animator.GetBool ("flyingState")) {
				fellToGroundYet = false;
				animator.SetBool ("fallingState", true);
				animator.SetBool ("flyingState", false);
			}

			// when dementor hits Harry and he falls off his broom, 
			// if a rock isn't underneath him, then allow him to fly again after X seconds
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 2f), Vector2.down);
			if (hit.collider == null) {
				Invoke ("CanFlyAgain", 1f);
			}
		}
		if (other.gameObject.tag == "SnitchPlacements") {
			lastSeenSnitchPlacement = System.Convert.ToInt32(other.gameObject.name.Substring (15));
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "platform") {
			fellToGroundYet = true;
		} 
	}

	void CanFlyAgain() {
		fellToGroundYet = true;
	}

	public int lastSeen() {
		return lastSeenSnitchPlacement;
	}
}
