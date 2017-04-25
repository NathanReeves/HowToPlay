using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoxKey : MonoBehaviour
{
    [SerializeField]
    MeshRenderer rend;
    [SerializeField]
    BoxCollider collid;

    GameObject button1;
    GameObject button2;
    bool button1Status;
    bool button2Status;

	// Use this for initialization
	void Start ()
    {
        rend = gameObject.GetComponent<MeshRenderer>();
        collid = gameObject.GetComponent<BoxCollider>();
        rend.enabled = false;
        collid.enabled = false;
        button1 = GameObject.FindGameObjectWithTag("Button1");
        button2 = GameObject.FindGameObjectWithTag("Button2");
    }
	
	// Update is called once per frame
	void Update ()
    {
        button1Status = button1.GetComponent<buttonTouchedPlatformer>().isOn;
        button2Status = button2.GetComponent<buttonTouchedPlatformer>().isOn;

        Debug.Log("B1 Down: " + button1Status);
        Debug.Log("B2 Down: " + button2Status);

		if (button1Status && button2Status)
        {
            rend.enabled = true;
            collid.enabled = true;
            gameObject.GetComponent<FirstBoxKey>().enabled = false;
        }
	}
}
