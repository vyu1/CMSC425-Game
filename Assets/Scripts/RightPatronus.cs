using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightPatronus : MonoBehaviour {

	private float originalInstantiatedPosition;
	private Text playerPointsText;

	// Use this for initialization
	void Start () {
		originalInstantiatedPosition = this.transform.position.x;
		playerPointsText = GameObject.Find ("Potter Points").GetComponent<Text>();
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
			playerPointsText.text = (int.Parse (playerPointsText.text) + 10).ToString();
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
	}
}
