using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmo : MonoBehaviour
{
    [SerializeField]
    MeshRenderer rend;
    [SerializeField]
    BoxCollider collid;

    private void Awake()
    {
        rend = gameObject.GetComponent<MeshRenderer>();
        collid = gameObject.GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rend.enabled = false;
            collid.enabled = false;
        }
    }
}
