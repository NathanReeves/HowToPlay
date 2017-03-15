using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCharMelee : MonoBehaviour
{
    // Serialized fields
    [SerializeField]
    int dps = 20;
    [SerializeField]
    float attackCooldown = 0.5f;
    [SerializeField]
    float attackRange = 3f;

    // Private fields
    float timer;
    Ray attack;
    RaycastHit attackHit;
    int hitableMask;
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
        // Increase timer since Update was called
        timer += Time.deltaTime;

        // If player is firing and shotCooldown isn't active...
        if (Input.GetButton("Fire1") && timer >= attackCooldown)
        {
            // ... shoot!
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

    void Attack()
    {
        // Reset timer
        timer = 0f;

        // Play melee audio
        //meleeAudio.Play();

        // Start/reset melee particle effects
        //gunParticles.Stop();
        //gunParticles.Play();

        // Set first point & direction of attack ray 
        attack.origin = transform.position;
        attack.direction = transform.forward;

        // Raycast against hitable objects; if it hits anything...
        if (Physics.Raycast(attack, out attackHit, attackRange, hitableMask))
        {
            // See if hit object has EnemyHealth script
            EnemyHealth enemyHealth = attackHit.collider.GetComponent<EnemyHealth>();

            // If hit object does have EnemyHelth, it takes damage
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(dps, attackHit.point);
            }
        }
    }
}