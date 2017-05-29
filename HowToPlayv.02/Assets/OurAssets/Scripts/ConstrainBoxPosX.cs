using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainBoxPosX : MonoBehaviour
{
    private Rigidbody boxRigid;
    private Vector3 boxPos;
    [SerializeField]
    private Vector3 minBound;
    [SerializeField]
    private Vector3 maxBound;

    private void Awake()
    {
        boxRigid = GetComponent<Rigidbody>();
        boxPos = boxRigid.position;
        minBound = new Vector3(330, 0, 0);
        maxBound = new Vector3(365, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        boxPos.x = transform.position.x;

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
