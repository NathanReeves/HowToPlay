using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SandEnemyMovePatrol : MonoBehaviour
{
    // References
    [SerializeField]
    private SandEnemyHealth enemyHealth;
    [SerializeField]
    private NavMeshAgent nav;
    [SerializeField]
    private float patrolSize = 10f;

    private float patrolBoundMin;
    private float patrolBoundMax;

    void Awake()
    {
        // Setup references
        enemyHealth = GetComponent<SandEnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        patrolBoundMin = transform.position.z - patrolSize;
        patrolBoundMax = transform.position.z + patrolSize;

        // Start by moving left towards patrolBoundMin
        nav.SetDestination(new Vector3(transform.position.x, transform.position.y, patrolBoundMin));
    }

    void Update()
    {
        Debug.Log(transform.position.z);
        Debug.Log(patrolBoundMin);

        // If enemy still has health...
        if (enemyHealth.currentHealth > 0)
        {
            // ... patrol back and forth between min and max position:
            if (transform.position.z == patrolBoundMin)
            {
                nav.SetDestination(new Vector3(transform.position.x, transform.position.y, patrolBoundMax));
            }
            // If enemy has hit patrolBoundMax, move left again towards patrolBoundMin
            else if (transform.position.z == patrolBoundMax)
            {
                nav.SetDestination(new Vector3(transform.position.x, transform.position.y, patrolBoundMin));
            }
        }
        // Otherwise...
        else
        {
            // ... disable nav mesh agent so enemy stop moving
            nav.enabled = false;
        }
    }
}
