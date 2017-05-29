using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCharCheckpoint : MonoBehaviour
{
    public Transform spawnPoint;

    private GameObject player;
    private SandCharHealth charHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        charHealth = player.GetComponent<SandCharHealth>();
    }

    private void Update()
    {
        //Debug.Log("Current spawnPoint: " + spawnPoint.position);
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
        transform.position = spawnPoint.position;

        // Now that player has respawned, they're no longer dead
        charHealth.currentHealth = charHealth.startHealth;
        charHealth.isDead = false;
    }
}
