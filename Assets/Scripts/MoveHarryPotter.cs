using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHarryPotter : MonoBehaviour {

	public Rigidbody2D rb;
	public float walkingSpeed;
	public float jumpSpeed;

	private Animator animator;
	public GameObject dementor;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
		InvokeRepeating ("SpawnDementor", 2.0f, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			animator.SetTrigger ("attackState");
		}
	}

	void FixedUpdate () 
	{
		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");

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
}
