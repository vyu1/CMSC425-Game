using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimation : MonoBehaviour {

	private float originalInstantiatedPosition;

	// Use this for initialization
	void Start () {
		originalInstantiatedPosition = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (originalInstantiatedPosition + 4 >= this.transform.position.y) {
			this.transform.position += new Vector3 (0, 2, 0) * Time.deltaTime;
		} else {
			Destroy (this.gameObject);
		}
	}
}
