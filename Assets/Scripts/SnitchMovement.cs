using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchMovement : MonoBehaviour {

	private int snitchPlacementPosition = 0;
	public Transform snitchPlacementsGroup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject target = snitchPlacementsGroup.transform.GetChild (snitchPlacementPosition).gameObject;
		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), 
				new Vector2(target.transform.position.x, target.transform.position.y), 5 * Time.deltaTime);
	}

	public void updateNewSnitchPlacement() {
		snitchPlacementPosition += 1;
		if (snitchPlacementPosition >= 9) {
			snitchPlacementPosition = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
//		print ("COLLIDING WITH SNITCH1COLLIDING WITH SNITCH1COLLIDING WITH SNITCH1COLLIDING WITH SNITCH1COLLIDING WITH SNITCH1");
		updateNewSnitchPlacement ();
	}
}
