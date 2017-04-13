using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOff : MonoBehaviour {

    public GameObject button;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(button.GetComponent<buttonTouched>().isOn)
        {
            turnoff();
        }
        else
        {
            turnon();
        }
	}
    public void turnon()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void turnoff()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

    }
}
