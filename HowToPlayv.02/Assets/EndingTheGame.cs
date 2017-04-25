using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EndingTheGame : MonoBehaviour {
	private UnityEngine.Video.VideoPlayer vPlayer;
	private Camera cam;

	// Use this for initialization
		void Start () {
			cam = gameObject.GetComponentInChildren<Camera>();
			cam.depth = -5;
			vPlayer = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();

		}

		void OnTriggerEnter(Collider other){
			cam.depth = 5;
		vPlayer.Play ();
		}


	
	// Update is called once per frame
	void Update () {
		
	}
}
