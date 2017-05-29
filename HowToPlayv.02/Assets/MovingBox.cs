using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour {

    private Rigidbody boxRigidBody;
	// Use this for initialization
	void Start () {
        boxRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	//void Update () {

 //   }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingGround"))
        {
            boxRigidBody.transform.parent = collision.transform;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingGround"))
        {
            boxRigidBody.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingGround"))
        { 
            boxRigidBody.transform.parent = null;
        }
    }
}
