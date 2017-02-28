using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTouched : MonoBehaviour {

	GameObject cube;
	public float downwardAmount;
	private bool isSinking;
	private bool shouldStopSinking;
	private bool defaultState;
	private int count = 30;

	// Use this for initialization
	void Start () {
		cube = this.transform.GetChild (1).gameObject;
		isSinking = false;
		//sinking = true;
		defaultState = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSinking) {
			sinking();
		} else if (!defaultState) {
			rising();
		}
//		if (!defaultState) {
//			rising ();
//		}
//
	}

//    void OnTriggerEnter(Collider other)
//    {
//		//Debug.Log ("COLLISION");
//		isSinking = true;
//    }

	void OnTriggerStay(Collider other){
		isSinking = true;
	}

	void OnTriggerExit(Collider other) {
		defaultState = false;
		isSinking = false;
	}

	void sinking(){
		
		if (count > 0) {
			Vector3 vec = new Vector3 (0.0f, 0.0f);
			vec.y = -(downwardAmount * Time.deltaTime);
			cube.transform.Translate (vec);
			count--;
		}
			
	}

	void rising(){

		if (count < 30) {
			Vector3 vec = new Vector3 (0.0f, 0.0f);
			vec.y = (downwardAmount * Time.deltaTime);
			cube.transform.Translate (vec);
			count++;
		} else {
			defaultState = true;
		}


	}

}
