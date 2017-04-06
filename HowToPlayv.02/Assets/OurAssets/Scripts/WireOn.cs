using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireOn : MonoBehaviour {

    public GameObject coloredCube;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<MeshRenderer>().material = coloredCube.GetComponent<MeshRenderer>().material;
        enabled = false;
    }
}
