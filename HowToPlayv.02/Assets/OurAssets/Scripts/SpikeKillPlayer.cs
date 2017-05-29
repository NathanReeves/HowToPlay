using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeKillPlayer : MonoBehaviour
{
    private GameObject player;
    private SandCharHealth charHealth;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        charHealth = player.GetComponent<SandCharHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player enters spikes, take all health away to kill player (should trigger respawn)
        if (other.gameObject == player)
        {
            charHealth.TakeDamage(charHealth.startHealth);
        }
    }
}
