using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTouchedPlatformer : MonoBehaviour {

	GameObject cube;
	public Material red;
	Material dfault;
	public float downwardAmount;
	public bool isSinking;
	public bool isOn;
	private bool shouldStopSinking;
	private bool defaultState;
	//private int count = 30;
	private float endYCoordinate;
	private float startYCoordinate;
	private float currentYCoordinate;

	private bool cubeIsIn = false;
	private bool playerIsIn = false;

	// Use this for initialization
	void Start()
	{

		cube = this.transform.GetChild(1).gameObject;
		isSinking = false;
		//sinking = true;
		defaultState = true;
		startYCoordinate = cube.transform.position.y;
		currentYCoordinate = startYCoordinate;
		endYCoordinate = startYCoordinate - 0.2f;
		dfault = cube.GetComponent<MeshRenderer>().material;
	}
	// Update is called once per frame
	void Update()
	{
		if (isSinking)
		{
			sinking();
		}
		else if (!defaultState)
		{
			rising();
		}
		// if (!defaultState) {
		// rising ();
		// }
		//

		//if (playerIsIn || cubeIsIn) {
		//	cube.GetComponent<MeshRenderer> ().material = red;
		//}
			//playerIsIn = false;
			//cubeIsIn = false;


	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("COLLISION");
		//isSinking = true;
		cube.GetComponent<MeshRenderer> ().material = red;
		if (other.gameObject.tag == "Ground") {
			cubeIsIn = true;
		}

		if (other.gameObject.tag == "Player") {
			playerIsIn = true;
		}

	}

	void OnTriggerStay(Collider other)
	{
		isSinking = true;
		isOn = true;
		//if (cube.GetComponent<MeshRenderer>().material.Equals(dfault))
		//{
		//	cube.GetComponent<MeshRenderer>().material = red;
		//}



	}
	//void OnTriggerEnter(Collider other)
	//{
	//	if (cube.GetComponent<MeshRenderer>().material.Equals(dfault))
	//	{
	//		isOn = true;
	//		cube.GetComponent<MeshRenderer>().material = red;
	//	}
	//
	//	else
	//	{
	//		isOn = false;
	//		cube.GetComponent<MeshRenderer>().material = dfault;
	//	}
	//}
	public void turnoff()
	{
		isOn = false;
		cube.GetComponent<MeshRenderer>().material = dfault;

	}
	void OnTriggerExit(Collider other)
	{
		defaultState = false;
		isSinking = false;
		isOn = false;

		if (other.gameObject.tag == "Player") {
			playerIsIn = false;
		}
		if (other.gameObject.tag == "Ground") {
			cubeIsIn = false;
		}

		if (!cubeIsIn || !playerIsIn) {
			cube.GetComponent<MeshRenderer> ().material = dfault;
		}

	}

	void sinking()
	{
		if (currentYCoordinate > endYCoordinate)
		{
			Vector3 vec = new Vector3(0.0f, 0.0f);
			vec.y = -(downwardAmount * Time.deltaTime);
			currentYCoordinate = cube.transform.position.y;
			cube.transform.Translate(vec);
			//count--;
		}
	}

	void rising()
	{

		if (currentYCoordinate < startYCoordinate)
		{
			Vector3 vec = new Vector3(0.0f, 0.0f);
			vec.y = (downwardAmount * Time.deltaTime);
			cube.transform.Translate(vec);
			//if (cube.transform.position.y > startYCoordinate) {
			//vec.y = cube.transform.position.y - startYCoordinate;
			//cube.transform.Translate (vec);
			//}
			currentYCoordinate = cube.transform.position.y;
			//count++;
		}
		else
		{
			Vector3 vec = new Vector3(0.0f, 0.0f);
			vec.y = (cube.transform.position.y - startYCoordinate) * -1.0f;
			cube.transform.Translate(vec);
			defaultState = true;
		}


	}
}
