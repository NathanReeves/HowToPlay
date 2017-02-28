using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{
		//var playerRigidbody = other.attachedRigidbody
		var playerRigidbody = other.GetComponent<Rigidbody> ();
		playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 4, playerRigidbody.velocity.z);
		Destroy (gameObject);
	}
}
