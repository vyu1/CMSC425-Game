using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHarryPotter : MonoBehaviour {

	public Rigidbody2D rb;
	public float walkingSpeed;
	public float jumpSpeed;

	private Animator animator;
	public GameObject dementor;
	public GameObject patronus;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
//		InvokeRepeating ("SpawnDementor", 2.0f, 5.0f);
		Invoke ("SpawnDementor", 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = Input.GetAxis ("Horizontal");
		if (Input.GetKeyDown (KeyCode.E) && horizontalInput > 0) {
			animator.SetTrigger ("rightAttackState");
			Invoke ("PatronusCharm", 1.0f);
		} else if (Input.GetKeyDown (KeyCode.E) && horizontalInput < 0) {
			animator.SetTrigger ("leftAttackState");
		}
	}

	void FixedUpdate () 
	{
		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");

		if (horizontalInput != 0) {
			animator.SetTrigger ("walkingState");
		} 

		if (Input.GetKey (KeyCode.Space)) {
			verticalInput = 1f;
		}

		if (verticalInput == 1) {
			horizontalInput *= 4;
		}

		var horizontalMove = new Vector3(horizontalInput, 0, 0);
		transform.position += horizontalMove * walkingSpeed * Time.deltaTime;

		var verticalMove = new Vector3(0, verticalInput, 0);
		transform.position += verticalMove * jumpSpeed * Time.deltaTime;

		animator.SetFloat("xInput", horizontalInput);
		animator.SetFloat("yInput", verticalInput);
	}

	void SpawnDementor() {
//		GameObject instance = Instantiate (dementor);
		Instantiate (dementor);
	}

	void PatronusCharm() {
		Instantiate (patronus, this.transform.position, this.transform.rotation);
	}
}
