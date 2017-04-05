using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Timers;

public class changeText : MonoBehaviour {

	Text justText;

	Text textPartOfImage;


	Image TextAndImage;
	Image JustTextPanel;

	Image ControllerLeftAnalogImage;
	Image ControllerJumpImage;

	//bool startOfGame;
	Canvas canv;
	//System.Timers.Timer shutOffPopUp;

	GameObject textPanel;
	GameObject textAndImagePanel;

	string[] messages;

	bool nowCountingTime;
//	bool waitingForInput;
	bool imageEnabled;
	float shutOffTime = 2.5f;


	// Use this for initialization
	void Start () {
		Text[] textArr = GetComponentsInChildren<Text> ();

		justText = textArr [0];
		textPartOfImage = textArr [1];



		canv = GetComponentInChildren<Canvas> ();

		Image[] arr = canv.GetComponentsInChildren<Image> ();
		JustTextPanel = arr [0];
		TextAndImage = arr [1];

		//load all images from the canvas
		Image [] arr2 = TextAndImage.GetComponentsInChildren<Image> ();
		ControllerLeftAnalogImage = arr2 [1];
		ControllerJumpImage = arr2 [2];


		//Image[] obj = ControllerLeftAnalogImage.GetComponentInParent<Image>()




		//startOfGame = true;
		canv.gameObject.SetActive(false);
		TextAndImage.gameObject.SetActive (false);
		JustTextPanel.gameObject.SetActive (false);


		nowCountingTime = false;
		//waitingForInput = false;
		messages = new string[10];
		//gameObject.SetActive (true);
		messages [0] = "Welcome to How To Play: The Game!";
		messages [1] = "Move the stick to move right or left!";
		messages [2] = "Move the stick to the left to move left!";

		//run the trigger method right at the beginning for the intro pop up
		isTriggered(0, false);

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
				canv.gameObject.SetActive (false);
				JustTextPanel.gameObject.SetActive (false);
				TextAndImage.gameObject.SetActive (false);

				shutOffTime = 2.5f;
				nowCountingTime = false;

			}
		}

	}


	//method should be ran when a trigger is encountered in the game world causing a text to pop up.
	public void isTriggered(int messageNumber, bool imageEnabled){
		canv.gameObject.SetActive (true);
		if (!imageEnabled) {
			JustTextPanel.gameObject.SetActive (true);
			//text.GetComponent<Renderer> ().enabled = true;
			//canv.gameObject.SetActive (true);
			//change/set message to be displayed on screen
			justText.text = messages [messageNumber];
		} else {
			TextAndImage.gameObject.SetActive (true);
			textPartOfImage.text = messages [messageNumber];
		}

		nowCountingTime = true;
	}
		
	public void enableTheCanvas(){
		canv.gameObject.SetActive (true);
	}




}
