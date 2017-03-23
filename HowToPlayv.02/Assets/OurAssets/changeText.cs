using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Timers;

public class changeText : MonoBehaviour {

	Text text;
	//bool startOfGame;

	//System.Timers.Timer shutOffPopUp;

	string[] messages;

	bool nowCountingTime;
	float shutOffTime = 3;


	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text> ();
		//startOfGame = true;
		gameObject.SetActive(false);
		nowCountingTime = false;
		messages = new string[10];
		//gameObject.SetActive (true);
		messages [0] = "Welcome to How To Play: The Game!";
		messages [1] = "Move the stick to the right to move right!";
		messages [2] = "Move the stick to the left to move left!";

		//run the trigger method right at the beginning for the intro pop up
		isTriggered(0);

	}
	
	// Update is called once per frame
	void Update () {
		//if (Time.fixedTime > 1) {
		//	gameObject.SetActive (true);
		//}
			
		//if (Time.fixedTime > 3) {
		//	text.text = "HIHIHIH!IH!IH!IH!IH!I!";
		//

		//counting seconds before the text pop up shuts off
		if (nowCountingTime) {
			shutOffTime = shutOffTime - Time.deltaTime;
			if (shutOffTime <= 0) {
				//text.GetComponent<Renderer> ().enabled = false;
				gameObject.SetActive(false);
				shutOffTime = 3;
				nowCountingTime = false;

			}
		}
	}


	//method should be ran when a trigger is encountered in the game world causing a text to pop up.
	void isTriggered(int messageNumber){
		//text.GetComponent<Renderer> ().enabled = true;
		gameObject.SetActive(true);

		//change/set message to be displayed on screen
		text.text = messages[messageNumber];

		nowCountingTime = true;
	}


}
