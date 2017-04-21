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
	Image ControllerRightAnalogImage;



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
		ControllerRightAnalogImage = arr2[3];


		//Image[] obj = ControllerLeftAnalogImage.GetComponentInParent<Image>()

		ControllerLeftAnalogImage.gameObject.SetActive (false);
		ControllerJumpImage.gameObject.SetActive (false);
		ControllerRightAnalogImage.gameObject.SetActive (false);

		//startOfGame = true;
		canv.gameObject.SetActive(false);
		TextAndImage.gameObject.SetActive (false);
		JustTextPanel.gameObject.SetActive (false);


		nowCountingTime = false;
		//waitingForInput = false;
		messages = new string[10];
		//gameObject.SetActive (true);
		messages [0] = "Welcome to How To Play: The Game!";
		messages [1] = "Press the A button to jump!";
		messages [2] = "Move the stick to the left to move left!";
		messages [3] = "Now you can move the left stick to move in any direction!";
		messages [4] = "Move the right stick to look around without moving!";
		messages [5] = "Move the stick to the right to move right!";

		//run the trigger method right at the beginning for the intro pop up
		//isTriggered(0, -1, 2.5f);

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

				ControllerLeftAnalogImage.gameObject.SetActive(false);
				ControllerJumpImage.gameObject.SetActive(false);
				ControllerRightAnalogImage.gameObject.SetActive(false);

				shutOffTime = 2.5f;
				nowCountingTime = false;

			}
		}

	}


	//method should be ran when a trigger is encountered in the game world causing a text to pop up.
	public void isTriggered(int messageNumber, int numOfImage, float appearTime){
		canv.gameObject.SetActive (true);
		if (numOfImage == -1) {
			JustTextPanel.gameObject.SetActive (true);
			//text.GetComponent<Renderer> ().enabled = true;
			//canv.gameObject.SetActive (true);
			//change/set message to be displayed on screen
			justText.text = messages [messageNumber];
		} else {
			TextAndImage.gameObject.SetActive (true);
			if (numOfImage == 0){
				ControllerLeftAnalogImage.gameObject.SetActive(true);
			}
			else if (numOfImage == 1){
				ControllerJumpImage.gameObject.SetActive(true);
			}
			else if (numOfImage == 2){
				ControllerRightAnalogImage.gameObject.SetActive(true);
			}

			textPartOfImage.text = messages [messageNumber];
		}

		shutOffTime = appearTime;
		nowCountingTime = true;
	}
		
	public void enableTheCanvas(){
		canv.gameObject.SetActive (true);
	}




}
