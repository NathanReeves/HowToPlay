﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPopup : MonoBehaviour {


	private changeText changeTextScriptReference;
	public int triggerNumber;

	// Use this for initialization
	void Start () {
		changeTextScriptReference = GetComponentInParent<changeText> ();
	}
	void OnTriggerEnter(Collider other){

		changeTextScriptReference.isTriggered (2, true);

	}


}