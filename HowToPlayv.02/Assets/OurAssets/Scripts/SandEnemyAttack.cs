using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandEnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float attackCooldown = 0.5f;
    [SerializeField]
    private int attackDamage = 25;

    //private Animator anim;
    private GameObject player;
    private SandCharHealth charHealth;
    //private SandEnemyHealth enemyHealth;
    private bool playerInAttackRange;
    private float attackTimer;

	void Awake ()
    {
        // Setup references
        player = GameObject.FindGameObjectWithTag("Player");
        charHealth = player.GetComponent<SandCharHealth>();
        //enemyHealth = GetComponent<SandEnemyHealth>();
        //anim = GetComponent<Animator>();
	}

    /*
    private void OnTriggerEnter(Collider other)
    {
        // If enemy is colliding with player...
        if (other.gameObject == player)
        {
            // ... player is in attack range
            playerInAttackRange = true;
        }
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        // If enemy is colliding with player...
        if (collision.gameObject == player)
        {
            // ... player is in attack range
            playerInAttackRange = true;
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        // If enemy is moving away from player...
        if (other.gameObject == player)
        {
            // ... player is no longer in attack range
            playerInAttackRange = false;
        }
    }
    */

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            // ... player is no longer in attack range
            playerInAttackRange = false;
        }
    }

    void Update ()
    {
        // Increment time since Update was last called
        attackTimer += Time.deltaTime;

        // If the timer exceeds attackCooldown, player is in range, and enemy is alive...
        if (attackTimer >= attackCooldown && playerInAttackRange /*&& EnemyHealth.currentHealth > 0*/)
        {
            // ... attack the player
            Attack();
        }
	}

    void Attack()
    {
        // Reset the timer
        attackTimer = 0f;

        // If the player has enough health to lose/be attacked...
        if (charHealth.currentHealth > 0)
        {
            // ... damage the player
            charHealth.TakeDamage(attackDamage);
        }
    }
}
