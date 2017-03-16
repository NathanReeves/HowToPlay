using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTouched : MonoBehaviour {

	GameObject cube;
	public float downwardAmount;
	private bool isSinking;
	private bool shouldStopSinking;
	private bool defaultState;
	//private int count = 30;
	private float endYCoordinate;
	private float startYCoordinate;
	private float currentYCoordinate;

	// Use this for initialization
	void Start () {
		cube = this.transform.GetChild (1).gameObject;
		isSinking = false;
		//sinking = true;
		defaultState = true;
		startYCoordinate = cube.transform.position.y;
		currentYCoordinate = startYCoordinate;
		endYCoordinate = startYCoordinate - 0.2f;

	}
	// Update is called once per frame
	void Update () {
		if (isSinking) {
			sinking();
		} else if (!defaultState) {
			rising();
		}
		// if (!defaultState) {
		// rising ();
		// }
		//
	}

	//    void OnTriggerEnter(Collider other)
	//    {
	// //Debug.Log ("COLLISION");
	// isSinking = true;
	//    }

	void OnTriggerStay(Collider other){
		isSinking = true;
	}

	void OnTriggerExit(Collider other) {
		defaultState = false;
		isSinking = false;
	}

	void sinking(){
		if (currentYCoordinate > endYCoordinate) {
			Vector3 vec = new Vector3 (0.0f, 0.0f);
			vec.y = -(downwardAmount * Time.deltaTime);
			currentYCoordinate = cube.transform.position.y;
			cube.transform.Translate (vec);
			//count--;
		}
	}

	void rising(){

		if (currentYCoordinate < startYCoordinate) {
			Vector3 vec = new Vector3 (0.0f, 0.0f);
			vec.y = (downwardAmount * Time.deltaTime);
			cube.transform.Translate (vec);
			//if (cube.transform.position.y > startYCoordinate) {
				//vec.y = cube.transform.position.y - startYCoordinate;
				//cube.transform.Translate (vec);
			//}
			currentYCoordinate = cube.transform.position.y;
			//count++;
		} else {
			Vector3 vec = new Vector3 (0.0f, 0.0f);
			vec.y = (cube.transform.position.y - startYCoordinate) * -1.0f;
			cube.transform.Translate (vec);
			defaultState = true;
		}


	}

}




