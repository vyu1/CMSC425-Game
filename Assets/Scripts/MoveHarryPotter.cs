using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHarryPotter : MonoBehaviour {

	private SpriteRenderer spr;
	private Sprite [] sprites;

	public Rigidbody2D rb;
	public float walkingSpeed;
	public float jumpSpeed;

	private float nextUpdate=1;
	private int whichAnimation=3;
	private bool attackingMotion = false;

	private Animator animator;


	// Use this for initialization
	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		// TODO can specify folder name in Resources.LoadALL()
		sprites = Resources.LoadAll<Sprite> ("");
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKey ("up") || Input.GetKey (KeyCode.Space)) {
//			spr.sprite = sprites [0];
//		}
//
//		if (Input.GetKey ("left")) {
//			spr.sprite = sprites [2];
//		}
//
//		if (Input.GetKey ("right")) {
//			if(Time.time>=nextUpdate){
//				nextUpdate=(Time.time)+0.20f;
//				if (whichAnimation >= 8) {
//					whichAnimation = 3;
//				}
//				spr.sprite = sprites [whichAnimation];
//				whichAnimation += 1;
//			}
//		}
//
//		// TODO ANIMATE THIS!
//		// TODO INCREMENTALLY GROW THE SMOKE
		if (Input.GetKeyDown (KeyCode.F)) 
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
}
