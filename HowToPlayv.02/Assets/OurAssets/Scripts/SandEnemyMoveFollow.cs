using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SandEnemyMoveFollow : MonoBehaviour
{
    // References
    [SerializeField]
    private GameObject player;
    private Transform playerPos;
    [SerializeField]
    private SandCharHealth playerHealth;
    [SerializeField]
    private SandEnemyHealth enemyHealth;
    [SerializeField]
    private NavMeshAgent nav;
    private bool triggered;

	void Awake ()
    {
        // Setup references
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform;
        playerHealth = playerPos.GetComponent<SandCharHealth>();
        enemyHealth = GetComponent<SandEnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        triggered = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        // If player collides with enemy "aggro" trigger...
        if (other.GetType() == typeof(CapsuleCollider) && other.gameObject == player)
        {
            // ... set enemy to follow player
            triggered = true;
        }
    }

    void Update ()
    {
        //Debug.Log("Triggered: " + triggered);

        if (triggered)
        {
            // If player and enemy both have health...
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                // ... set enemy's destination as the player's position
                nav.SetDestination(playerPos.position);
            }
            // Otherwise...
            else
            {
                // ... disable nav mesh agent so enemy stop moving
                nav.enabled = false;
            }
        }

	}
}
