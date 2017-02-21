using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	Rigidbody playerRigidbody;
	Vector3 movement;
	bool forward;
	bool turn;
	bool jumped;
	// Use this for initialization
	void Start () {
		forward = true;
		turn = false;
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerRigidbody.velocity.y == 0) {
			jumped = false;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			if (!forward)
				turn = true;
			forward = true;
			movement = new Vector3 (.22f, 0, 0);
			playerRigidbody.MovePosition (transform.position + movement);
			GetComponent<Animator> ().SetBool ("IsWalking", true);

		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			
			if (forward)
				turn = true;
			forward = false;
			movement = new Vector3 (-.22f, 0, 0);
			playerRigidbody.MovePosition (transform.position + movement);
			GetComponent<Animator> ().SetBool ("IsWalking", true);

		} 
		else
			GetComponent<Animator>().SetBool ("IsWalking", false);
		if (Input.GetKey (KeyCode.UpArrow)) {
			if (!jumped) {
				playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x,playerRigidbody.velocity.y + 9, playerRigidbody.velocity.z);
				jumped = true;
			}

		}
		if (turn) {
			Vector3 rot = transform.rotation.eulerAngles;
			rot = new Vector3(rot.x,rot.y+180,rot.z);
			transform.rotation = Quaternion.Euler(rot);
			//playerRigidbody.MoveRotation (transform.rota);
			turn = false;
		}
		
	}
}
