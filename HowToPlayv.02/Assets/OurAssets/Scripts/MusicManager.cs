using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
	public AudioSource bgm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StopBGM()
	{
		
		bgm.Stop ();
	}
	public void ChangeBGM(AudioClip music)
	{
		if (bgm.clip.name == music.name)
			return;
		bgm.volume = 1;
		bgm.clip = music;
		bgm.Play ();
	}
}
