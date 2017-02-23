using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTouched : MonoBehaviour {

	GameObject cube;
	public float downwardAmount;
	private bool isSinking;
	private bool shouldStopSinking;
	private int count = 0;

	// Use this for initialization
	void Start () {
		cube = this.transform.GetChild (1).gameObject;
		isSinking = false;
		shouldStopSinking = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!shouldStopSinking) {
			sinking ();
		}
	}

    void OnTriggerEnter(Collider other)
    {
		//Debug.Log ("COLLISION");
		shouldStopSinking = false;
    }

	void sinking(){
		Vector3 vec = new Vector3 (0.0f, 0.0f);

			vec.y = -(downwardAmount * Time.deltaTime);
			cube.transform.Translate (vec);
			count++;
			if (count > 40) {
				shouldStopSinking = true;
			}
			
	}
}
