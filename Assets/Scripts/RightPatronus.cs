using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPatronus : MonoBehaviour {

	private float originalInstantiatedPosition;

	// Use this for initialization
	void Start () {
		originalInstantiatedPosition = this.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (originalInstantiatedPosition + 5 >= this.transform.position.x) {
			this.transform.position += new Vector3 (1, 0, 0) * Time.deltaTime;
		} else {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "dementor") {
			print ("triggered");
			Destroy (this.gameObject);
		}
	}
}
