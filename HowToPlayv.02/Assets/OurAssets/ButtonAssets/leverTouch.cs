using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverTouch : MonoBehaviour {

	Animator anim;
	private bool toggle;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		toggle = true;
	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}

	void OnTriggerEnter(Collider other){

		anim.SetBool ("shouldTriggerLever", toggle);
		toggle = !toggle;
	}
}
