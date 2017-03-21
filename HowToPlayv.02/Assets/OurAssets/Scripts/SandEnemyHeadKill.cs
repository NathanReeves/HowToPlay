using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandEnemyHeadKill : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        // If player collides with head collider on enemy...
        if (other.GetType() == typeof(SphereCollider))
        {
            // ... destroy this enemy
            Destroy(other.gameObject);
        }
    }
}
