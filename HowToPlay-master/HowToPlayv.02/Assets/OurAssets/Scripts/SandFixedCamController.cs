using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandFixedCamController : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 5.0f;
    [SerializeField]
    private float smoothing = 2.0f;

    private Vector2 mouseLook;
    private Vector2 smoothV;
	public GameObject character;
    
	// Use this for initialization
	void Start ()
    {
        //character = this.transform.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
		/*
        // Look/aim with mouse
        var mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseChange = Vector2.Scale(mouseChange, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mouseChange.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseChange.y, 1f / smoothing);
        mouseLook += smoothV;*/
		//transform.localRotation = Quaternion.AngleAxis(-mouseLook.x, Vector3.up);

		//3rd person
		var rot = new Vector2(0f, 0f);
		/*// rotates Camera Left
		if (Input.GetAxis("Mouse X") < 0)
		{
			rot.x -= 1;
		}
		// rotates Camera Left
		if (Input.GetAxis("Mouse X") > 0)
		{
			rot.x += 1;
		}
        */
		// rotates Camera Up
		if (Input.GetAxis("Mouse Y") < 0)
		{
			rot.y -= 1;
		}
		// rotates Camera Down
		if (Input.GetAxis("Mouse Y") > 0)
		{
			rot.y += 1;
		}
        //rot.y += Input.GetAxis("Fire1");
        //rot.x += (.5f)*Input.GetAxis("Fire2");
        
        if (Math.Abs(Input.GetAxis("Fire2")) > .9)
        {
            rot.x -= .25f*Input.GetAxis("Fire2");
            
        }
        if (Math.Abs(Input.GetAxis("Fire1")) > .9)
        {
            rot.y += .25f * Input.GetAxis("Fire1");

        }
        transform.Translate(rot);
        rot = new Vector2();
		transform.LookAt(character.transform);

		///first person

		//transform.Rotate(rot);
		//transform.localRotation = Quaternion.Euler (rot);
        //transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        //character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
