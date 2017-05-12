using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	private Transform player;
	private Vector3 offset;

	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("HarryPotter").transform;
	}

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		if (!player.GetComponent<MoveHarryPotter> ().playerIsBobbing ()) { 
			// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, 0) + offset;
		}
	}
}
