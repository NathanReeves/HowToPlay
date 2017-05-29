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
    private float patrolZSize = 10f;
    [SerializeField]
    private float patrolXSize = 0f;

    private float patrolZBoundMin;
    private float patrolZBoundMax;
    private float patrolXBoundMin;
    private float patrolXBoundMax;

    void Awake()
    {
        // Setup references
        enemyHealth = GetComponent<SandEnemyHealth>();
        nav = GetComponent<NavMeshAgent>();

        // Setup patrol bounds based on x or z axis
        if (patrolXSize == 0)
        {
            patrolZBoundMin = transform.position.z - patrolZSize;
            patrolZBoundMax = transform.position.z + patrolZSize;

            // Start by moving left towards patrolBoundMin
            nav.SetDestination(new Vector3(transform.position.x, transform.position.y, patrolZBoundMin));
        }
        else if (patrolZSize == 0)
        {
            patrolXBoundMin = transform.position.x - patrolXSize;
            patrolXBoundMax = transform.position.x + patrolXSize;

            // Start by moving left towards patrolBoundMin
            nav.SetDestination(new Vector3(patrolXBoundMin, transform.position.y, transform.position.z));
        }
    }

    void Update()
    {
        // If enemy still has health...
        if (enemyHealth.currentHealth > 0)
        {
            // If moving only in Z axis...
            if (patrolXSize == 0)
            {
                // ... patrol back and forth between min and max position:
                if (transform.position.z == patrolZBoundMin)
                {
                    nav.SetDestination(new Vector3(transform.position.x, transform.position.y, patrolZBoundMax));
                }
                // If enemy has hit patrolBoundMax, move left again towards patrolBoundMin
                else if (transform.position.z == patrolZBoundMax)
                {
                    nav.SetDestination(new Vector3(transform.position.x, transform.position.y, patrolZBoundMin));
                }
            }
            // If moving only in X axis...
            else if (patrolZSize == 0)
            {
                // ... patrol back and forth between min and max position:
                if (transform.position.x == patrolXBoundMin)
                {
                    nav.SetDestination(new Vector3(patrolXBoundMax, transform.position.y, transform.position.z));
                }
                // If enemy has hit patrolBoundMax, move left again towards patrolBoundMin
                else if (transform.position.x == patrolXBoundMax)
                {
                    nav.SetDestination(new Vector3(patrolXBoundMin, transform.position.y, transform.position.z));
                }
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
