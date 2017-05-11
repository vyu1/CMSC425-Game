using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchMovement : MonoBehaviour {

	private int snitchPlacementPosition = 0;
	private int maxSnitchPlacements = 12;
	public Transform snitchPlacementsGroup;
	public GameObject player;
	private int playerLastSeen;

	// Use this for initialization
	void Start () {
		updateNewSnitchPlacement ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerLastSeen != player.GetComponent<MoveHarryPotter> ().lastSeen ()) {
			updateNewSnitchPlacement ();
		}
		GameObject target = snitchPlacementsGroup.transform.GetChild (snitchPlacementPosition).gameObject;
		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), 
				new Vector2(target.transform.position.x, target.transform.position.y), 15 * Time.deltaTime);
		if (transform.position == target.transform.position) {
			updateNewSnitchPlacement ();
		}
	}

	public void updateNewSnitchPlacement() {
		playerLastSeen = player.GetComponent<MoveHarryPotter> ().lastSeen ();
		int possibleNewPositionNumber;
		GameObject possibleNewPosition;
		bool flag = false;
		do {
			possibleNewPositionNumber = Random.Range (0, maxSnitchPlacements);
			possibleNewPosition = snitchPlacementsGroup.transform.GetChild (possibleNewPositionNumber).gameObject;
//			print(possibleNewPosition.transform.position.x);
			if (possibleNewPosition.transform.position.x < player.transform.position.x 
				&& player.transform.position.x < this.transform.position.x) {
				flag = true;
			} else if (possibleNewPosition.transform.position.x > player.transform.position.x 
				&& player.transform.position.x > this.transform.position.x) {
				flag = true;
			} else {
//				print ("my snitch old position is " + this.transform.position.x);
//				print ("new snitch position is this " + possibleNewPosition.transform.position.x);
//				print ("but player is here " + player.transform.position.x);
				flag = false;
			}
		} while (possibleNewPositionNumber == playerLastSeen || flag);
		snitchPlacementPosition = possibleNewPositionNumber;
//		print (snitchPlacementPosition);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "HarryPotter") {
			print ("COLLIDING WITH SNITCH1COLLIDING WITH SNITCH1COLLIDING WITH SNITCH1COLLIDING WITH SNITCH1COLLIDING WITH SNITCH1");
		}
	}
}
