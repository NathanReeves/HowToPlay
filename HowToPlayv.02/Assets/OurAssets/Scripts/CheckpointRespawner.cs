using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointRespawner : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If player collides with a checkpoint, the new spawnPoint is set to it
            other.GetComponent<SandCharCheckpoint>().spawnPoint = transform;

            // Also make checkpoint "disappear" and disable collider, so player can't reach this point more than once
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
