using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireOnCombination : MonoBehaviour {

    public GameObject[] buttons;
    public GameObject[] wrongbuttons;
    public Material red;
    //bool canunlock;
    Material dfault;
    List<GameObject> code;
    public bool unlocked;
    // Use this for initialization
    void Start () {
        unlocked = false;
        //code = new List<GameObject>(){button1};
        dfault = gameObject.GetComponent<MeshRenderer>().material;

    }
	
	// Update is called once per frame
	void Update () {
        //hard code button sequence
        unlocked = true;
        foreach(GameObject button in buttons)
         {
            if (!button.GetComponent<buttonTouched>().isOn)
            {
                 unlocked = false;
             
            }
            else
            {

            }

            
        }
        foreach (GameObject button in wrongbuttons)
        {
            if (button.GetComponent<buttonTouched>().isOn)
            {
                unlocked = false;

            }
            else
            {

               // unlocked = true;
            }


        }
        if (unlocked)
        {
            gameObject.GetComponent<MeshRenderer>().material = red;
            //this.enabled = false;
        }
        else
            gameObject.GetComponent<MeshRenderer>().material = dfault;
    }
}
