using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicTrigger : MonoBehaviour {
	private MusicManager mandm;
	// Use this for initialization
	void Start () {
		mandm = FindObjectOfType<MusicManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			mandm.StopBGM ();
			
		}
	}
}
