using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainBoxPosZ : MonoBehaviour
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
        minBound = new Vector3(0, 0, 687);
        maxBound = new Vector3(0, 0, 740);
    }

    // Update is called once per frame
    void Update()
    {
        boxPos.z = transform.position.z;

        if (boxPos.z > maxBound.z)
        {
            boxRigid.MovePosition(maxBound);
            boxRigid.AddForce(Vector3.zero);
            boxRigid.velocity = Vector3.zero;
        }
        if (boxPos.z < minBound.z)
        {
            boxRigid.MovePosition(minBound);
            boxRigid.AddForce(Vector3.zero);
            boxRigid.velocity = Vector3.zero;
        }
    }
}
