using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCharCheckpoint : MonoBehaviour
{
    public int reachedCheckpoint;
    [SerializeField]
    private GameObject startPoint;
    [SerializeField]
    private GameObject checkpoint1;
    [SerializeField]
    private SandCharHealth charHealth;

    void Awake()
    {
        startPoint = GameObject.FindGameObjectWithTag("Start");
        checkpoint1 = GameObject.FindGameObjectWithTag("Checkpoint");

        // Set inital respawn point at start of platformer
        reachedCheckpoint = 0;
    }

    private void Update()
    {
        Debug.Log("Checkpoint: " + reachedCheckpoint);
        Debug.Log("Start pos: " + startPoint.transform.position);
        Debug.Log("Checkpoint pos: " + checkpoint1.transform.position);
    }

    public void RespawnToCheckpoint()
    {
        // Set death flag so this function is only called once
        charHealth.isDead = true;

        // Turn off any player shooting effects
        //charShooting.DisableEffects();

        // Tell animator that player is dead
        //anim.SetTrigger("Die");

        // Play death audio
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        // Turn off movement and shooting scripts if last checkpoint was before twinstick
        //charShooting.enabled = false;
        //charMovement.enabled = false;

        // Set player at last-reached checkpoint
        if (reachedCheckpoint == 0)
        {
            this.transform.position = startPoint.transform.position;
        }
        else if (reachedCheckpoint == 1)
        {
            this.transform.position = checkpoint1.transform.position;
        }

        // Now that player has respawned, they're no longer dead
        charHealth.currentHealth = charHealth.startHealth;
        charHealth.isDead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player collides with a checkpoint...
        if (other.CompareTag("Checkpoint"))
        {
            // Disable this checkpoint's trigger so it doesn't increment more than once
            other.GetComponent<MeshRenderer>().enabled = false;
            other.GetComponent<CapsuleCollider>().enabled = false;

            // Increment checkpoint counter
            reachedCheckpoint++;
        }
    }
}
