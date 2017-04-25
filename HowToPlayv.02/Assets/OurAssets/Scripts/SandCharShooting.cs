using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCharShooting : MonoBehaviour
{
    // Serialized fields
    [SerializeField]
    int damageAmount = 20;
    [SerializeField]
    float shotCooldown = 0.3f;
    [SerializeField]
    float shotRange = 100f;

    // Private fields
    float timer;
    Ray shotFired;
    RaycastHit shotHit;
    int shootableMask;
    //ParticleSystem gunParticles;
    LineRenderer shotLine;
    //AudioSource gunAudio;
    //Light gunLight;
    float effectsDisplayTime = 0.2f;

    // Call when game first starts up
	void Awake ()
    {
        // Create layer mask to determine what's "shootable"
        shootableMask = LayerMask.GetMask("Shootable");

        // Setup references
        //gunParticles = GetComponent<ParticleSystem>();
        shotLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
        //gunLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Increase timer since Update was called
        timer += Time.deltaTime;

        // If player is firing and shotCooldown isn't active...
        if (Input.GetButton("Fire1") && timer >= shotCooldown)
        {
            // ... shoot!
            Shoot();
        }
        if(Input.GetAxis("Fire3") != 0 && timer >= shotCooldown)
        {
            Shoot();
        }
        // If timer is exceeded...
        if (timer >= shotCooldown * effectsDisplayTime)
        {
            // ... disable effects
            DisableEffects();
        }
	}

    public void DisableEffects()
    {
        shotLine.enabled = false;
        //gunLight.enabled = false;
    }

    void Shoot()
    {
        // Reset timer
        timer = 0f;

        // Play gun audio
        //gunAudio.Play();

        // Flash gun's light
        //gunLight.enabled = true;

        // Start/reset gun particle effects
        //gunParticles.Stop();
        //gunParticles.Play();

        // Set first point of visible shot line at gun barrel
        shotLine.enabled = true;
        shotLine.SetPosition(0, transform.position);

        // Set first point & direction of shotFired ray 
        shotFired.origin = transform.position;
        shotFired.direction = transform.forward;

        // Raycast against shootable objects; if it hits anything...
        if (Physics.Raycast(shotFired, out shotHit, shotRange, shootableMask))
        {
            // See if hit object has EnemyHealth script
            SandEnemyHealth enemyHealth = shotHit.collider.GetComponent<SandEnemyHealth>();

            // If hit object does have EnemyHelth, it takes damage
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }

            // Set end of line renderer at hit object
            shotLine.SetPosition(1, shotHit.point);
        }
        // Otherwise, if didn't hit anything shootable
        else
        {
            // Extend gunshot line out to range limit
            shotLine.SetPosition(1, shotFired.origin + shotFired.direction * shotRange);
        }
    }
}
