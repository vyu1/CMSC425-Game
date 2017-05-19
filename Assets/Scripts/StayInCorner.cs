using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInCorner : MonoBehaviour {

	private Vector3 offset;

	void Awake ()
	{
		// Setting up the reference.
	}

	// Use this for initialization
	void Start () {

	}

	void Update () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, 5));
	}
}
