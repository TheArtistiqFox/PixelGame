using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource deathSound;
    public AudioClip deathClip;
    public GameObject death_Anim;

    public int maxHealth = 100;
    public int currentHealth;


    public HealthBar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            deathSound.PlayOneShot(deathClip);
            GameObject death_Animation = Instantiate(death_Anim);
            death_Animation.transform.position = transform.position;

            if (gameObject.tag == "Player")
            {
                SceneManager.LoadScene("DeathScreen");
            }

            Destroy(gameObject);//destroy the player
            Destroy(death_Animation, 0.4f);
        }
    }
}
