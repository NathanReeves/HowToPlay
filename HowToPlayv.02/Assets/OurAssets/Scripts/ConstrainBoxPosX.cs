using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainBoxPosX : MonoBehaviour
{
    private Rigidbody boxRigid;
    private Vector3 boxPos;
    [SerializeField]
    private Vector3 maxBound;
    [SerializeField]
    private Vector3 minBound;

    private void Awake()
    {
        boxRigid = GetComponent<Rigidbody>();
        boxPos = boxRigid.position;
    }

    // Update is called once per frame
    void Update ()
    {
        boxPos.x = transform.position.x;

        Debug.Log("Box pos z: " + boxPos);
        Debug.Log("Max pos z: " + maxBound);
        Debug.Log("Min pos z: " + minBound);

        if (boxPos.x > maxBound.x)
        {
            boxRigid.MovePosition(maxBound);
            boxRigid.AddForce(Vector3.zero);
            boxRigid.velocity = Vector3.zero;
        }
        if (boxPos.x < minBound.x)
        {
            boxRigid.MovePosition(minBound);
            boxRigid.AddForce(Vector3.zero);
            boxRigid.velocity = Vector3.zero;
        }
	}
}
