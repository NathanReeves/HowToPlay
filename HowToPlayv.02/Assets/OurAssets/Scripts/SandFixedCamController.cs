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
    private GameObject character;
    
	// Use this for initialization
	void Start ()
    {
        character = this.transform.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Look/aim with mouse
        var mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseChange = Vector2.Scale(mouseChange, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mouseChange.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseChange.y, 1f / smoothing);
        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
