using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoxKey : MonoBehaviour
{
    [SerializeField]
    GameObject button1;
    [SerializeField]
    GameObject button2;

    bool button1Down;
    bool button2Down;
    MeshRenderer renderer;
    BoxCollider collider;

	// Use this for initialization
	void Awake ()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
        collider = gameObject.GetComponent<BoxCollider>();
        renderer.enabled = false;
        collider.enabled = false;
        button1Down = button1.GetComponent<buttonTouchedPlatformer>().isOn;
        button2Down = button1.GetComponent<buttonTouchedPlatformer>().isOn;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("B1 Down: " + button1Down);
        Debug.Log("B2 Down: " + button2Down);

		if (button1Down && button1Down)
        {
            renderer.enabled = false;
            collider.enabled = false;
        }
	}
}
