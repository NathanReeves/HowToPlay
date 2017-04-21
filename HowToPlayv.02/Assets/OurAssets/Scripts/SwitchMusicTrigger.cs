using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicTrigger : MonoBehaviour {
	public AudioClip newTrack;

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
			if (newTrack != null) {
				mandm.ChangeBGM (newTrack);
			}
		}
	}
}
