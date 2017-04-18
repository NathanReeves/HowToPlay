using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCharMelee : MonoBehaviour
{
    // Serialized fields
    [SerializeField]
    private int damageAmount = 100;
    [SerializeField]
    private float attackCooldown = 0.5f;
    [SerializeField]
    private float attackRange = 5f;

    // Private fields
    private float timer;
    private int hitableMask;
    private bool hitable;
    SandEnemyHealth enemyHealth;
    //ParticleSystem meleeParticles;
    //AudioSource meleeAudio;
    //float effectsDisplayTime = 0.2f;

    // Call when game first starts up
    void Awake()
    {
        // Create layer mask to determine what's hitable (using "Shootable" layer in this case)
        hitableMask = LayerMask.GetMask("Shootable");

        // Setup references
        //meleeParticles = GetComponent<ParticleSystem>();
        //meleeAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.Log(enemyHealth);

        // Increase timer since Update was called
        timer += Time.deltaTime;

        // If player is swinging melee weapon and attackCooldown isn't active...
        if (Input.GetButton("Fire1") && timer >= attackCooldown)
        {
            // ... attack!
            Attack();
        }

        /*
        // If timer has exceeded 
        if (timer >= shotCooldown * effectsDisplayTime)
        {
            // ... disable effects
            DisableEffects();
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        // If colliding with an enemy hitbox...
        if (other.CompareTag("Enemy"))
        {
            // ... set hitable to true
            hitable = true;

            // Also make note of enemy being hit (so we know who's health to decrease)
            enemyHealth = other.GetComponentInParent<SandEnemyHealth>();
        }

        // If player collides with head collider on enemy...
        if (other.GetType() == typeof(SphereCollider))
        {
            // ... destroy this enemy
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If leaving an enemy hitbox...
        if (other.CompareTag("Enemy"))
        {
            // ... set hitable to false
            hitable = false;
        }
    }

    void Attack()
    {
        // Reset timer
        timer = 0f;

        // Play melee audio
        //meleeAudio.Play();

        // Start/reset melee particle effects
        //gunParticles.Stop();
        //gunParticles.Play();

        // If enemy is hitable and has health...
        if (hitable && enemyHealth != null)
        {
            // ... attack it and subtract damageAmount from their health
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}