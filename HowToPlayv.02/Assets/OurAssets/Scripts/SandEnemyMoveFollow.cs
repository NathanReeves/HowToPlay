using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SandEnemyMoveFollow : MonoBehaviour
{
    // References
    [SerializeField]
    private Transform player;
    [SerializeField]
    private SandCharHealth playerHealth;
    [SerializeField]
    private SandEnemyHealth enemyHealth;
    [SerializeField]
    private NavMeshAgent nav;

	void Awake ()
    {
        // Setup references
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<SandCharHealth>();
        enemyHealth = GetComponent<SandEnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
	}
	
	void Update ()
    {
		// If player and enemy both have health...
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // ... set enemy's destination as the player's position
            nav.SetDestination(player.position);
        }
        // Otherwise...
        else
        {
            // ... disable nav mesh agent so enemy stop moving
            nav.enabled = false;
        }
	}
}
