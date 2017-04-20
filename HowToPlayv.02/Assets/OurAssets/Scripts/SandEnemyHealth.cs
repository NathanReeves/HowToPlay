using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandEnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 100;
    public int currentHealth;
    //[SerializeField]
    //private float sinkSpeed = 2.5f;
    //[SerializeField]
    //private int scoreValue = 10;
    //[SerializeField]
    //private AudioClip deathClip;

    //private Animator anim;
    //private AudioSource enemyAudio;
    //private ParticleSystem hitParticles;
    private BoxCollider boxCollider;
    private bool isDead;
    //private bool isSinking;

	void Awake ()
    {
        // Setup references
        //anim = GetComponent<Animator>();
        //enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider>();

        // Set current health as start health at beginning
        currentHealth = startingHealth;
	}
	
	void Update ()
    {
        //Debug.Log("Enemy health: " + currentHealth);

		// If the enemy should be sinking...
        //if (isSinking)
        //{
            // ... move the enemy down by sinkSpeed / second
            //transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        //}
	}

    public void TakeDamage(int amount)
    {
        // If the enemy is dead...
        if (isDead)
        {
            // ... no need to take damage, so return/exit
            return;
        }

        // Play hurt sound effect
        //enemyAudio.Play();

        // Reduce current health by damage amount
        currentHealth -= amount;

        // Set poistion of particle system to hitPoint
        //hitParticles.transform.position = hitPoint;

        // Play the particles
        //hitParticles.Play();

        // If current health is <= zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead
            Death();
        }
    }

    private void Death()
    {
        // Set enemy to dead
        isDead = true;

        // Turn collider into a trigger so shots can now pass through it
        boxCollider.isTrigger = true;

        // Destroy this enemy
        Destroy(this.gameObject);

        // Tell animator that enemy is dead
        //anim.SetTrigger("Dead");

        // Change audio to play enemy death, then play it
        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();
    }

    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent
        //GetComponent<NavMeshAgent>().enabled = false;
    }
}
