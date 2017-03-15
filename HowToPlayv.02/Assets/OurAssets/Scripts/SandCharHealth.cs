using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SandCharHealth : MonoBehaviour
{
    [SerializeField]
    private int startHealth = 100;
    public int currentHealth;
    //[SerializeField]
    //private Slider healthBar;
    //[SerializeField]
    //private Image damageImage;
    //[SerializeField]
    //private AudioClip deathSound;
    //[SerializeField]
    //private float deathFlashSpeed = 5f;
    //[SerializeField]
    //public Color hurtFlashColor = new Color(1f, 0f, 0f, 0.3f);

    //private Animator anim;
    //private AudioSource playerAudio;
    private SandCharController charMovement;
    private SandCharShooting charShooting;
    private bool isDead;
    private bool damaged;

    void Awake()
    {
        // Get references
        //anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        charMovement = GetComponent<SandCharController>();
        charShooting = GetComponent<SandCharShooting>();

        // Initial health is current health at start of game
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update ()
    {
        Debug.Log("Player health: " + currentHealth);

        // If the player has just been damaged...
        if (damaged)
        {
            // ... damage feedback
        }
        // Otherwise...
        else
        {
            // ... no damage feedback (clear)
        }

        // Reset damaged back to false
        damaged = false;
	}

    public void TakeDamage(int amount)
    {
        // Set damaged flag to show damage feedback
        damaged = true;

        // Reduce health
        currentHealth -= amount;

        // Decrease healthbar to match reduced currentHealth
        //healthBar.value = currentHealth;

        // Play hurt sound effect
        //playerAudio.Play();

        // If player's health is 0, and not already dead...
        if (currentHealth <= 0 && !isDead)
        {
            // ... player is killed
            Dead();
        }
    }

    void Dead()
    {
        // Set death flag so this function is only called once
        isDead = true;

        // Turn off any player shooting effects
        //charShooting.DisableEffects();

        // Tell animator that player is dead
        //anim.SetTrigger("Die");

        // Play death audio
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        // Turn off movement and shooting scripts
        charShooting.enabled = false;
        charMovement.enabled = false;
    }
}
