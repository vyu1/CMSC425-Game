﻿using System.Collections;
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


	// Use this for initialization
	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		sprites = Resources.LoadAll<Sprite> ("");
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("up") || Input.GetKey (KeyCode.Space)) {
			spr.sprite = sprites [0];
		}

		if (Input.GetKey ("left")) {
			spr.sprite = sprites [2];
		}

		if (Input.GetKey ("right")) {
			if(Time.time>=nextUpdate){
				nextUpdate=(Time.time)+0.20f;
				if (whichAnimation >= 8) {
					whichAnimation = 3;
				}
				spr.sprite = sprites [whichAnimation];
				whichAnimation += 1;
			}
		}

		// TODO ANIMATE THIS!
		// TODO INCREMENTALLY GROW THE SMOKE
		if (Input.GetKeyDown (KeyCode.W)) 
		{
			spr.sprite = sprites [8];
			spr.sprite = sprites [11];
		}
	}

	void FixedUpdate () 
	{
		var horizontalMove = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		transform.position += horizontalMove * walkingSpeed * Time.deltaTime;

		var verticalMove = new Vector3(0, Input.GetAxis("Vertical"), 0);
		transform.position += verticalMove * jumpSpeed * Time.deltaTime;
	}
}
