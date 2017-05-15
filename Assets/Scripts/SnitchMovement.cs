using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnitchMovement : MonoBehaviour {

	private int snitchPlacementPosition = 0;
	private int maxSnitchPlacements = 12;
	public Transform snitchPlacementsGroup;
	public GameObject player;
	private int playerLastSeen;
	private Text playerScoreText;

	// Use this for initialization
	void Start () {
		updateNewSnitchPlacement ();
		playerScoreText = GameObject.Find ("Potter Points").GetComponent<Text>();
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
		int count = 0;
		do {
			count++;
			possibleNewPositionNumber = Random.Range (0, maxSnitchPlacements);
			possibleNewPosition = snitchPlacementsGroup.transform.GetChild (possibleNewPositionNumber).gameObject;
			if (possibleNewPosition.transform.position.x < player.transform.position.x 
				&& player.transform.position.x < this.transform.position.x) {
				flag = true;
			} else if (possibleNewPosition.transform.position.x > player.transform.position.x 
				&& player.transform.position.x > this.transform.position.x) {
				flag = true;
			} else if (possibleNewPosition.transform.position.y < player.transform.position.y 
				&& player.transform.position.y < this.transform.position.y) {
				flag = true;
			} else if (possibleNewPosition.transform.position.y > player.transform.position.y 
				&& player.transform.position.y > this.transform.position.y) {
				flag = true;
			} else {
				flag = false;
			}
		} while (flag && count <= maxSnitchPlacements);
//		while (possibleNewPositionNumber == playerLastSeen || )
		snitchPlacementPosition = possibleNewPositionNumber;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "HarryPotter") {
			playerScoreText.text = (int.Parse (playerScoreText.text) + 150).ToString();
		}
	}
}
