using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;

    public Text healthText;

    public AudioSource playSound;
    public AudioClip[] painSoundsArray;
    public AudioClip healthPickup;

    private void Start()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthText.text = "HEALTH: " + health + " / " + maxHealth;

        // change colour of text depending on health amount
        if (health >= 80)
            healthText.color = Color.green;
        else if (health >= 40)
            healthText.color = Color.yellow;
        else
            healthText.color = Color.red;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        // pain sounds
        if (!playSound.isPlaying)
        {
            playSound.clip = painSoundsArray[Random.Range(0, painSoundsArray.Length)]; // choose random clip
            playSound.Play(); // play clip
        }

        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
        UpdateHealthBar();
    }

    public void MissedShot(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
        UpdateHealthBar(); // update health text
    }

    public void RestoreHealth(float restore)
    {
        playSound.clip = healthPickup;
        playSound.Play();

        if (health < maxHealth)
        {
            health += restore;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealthBar(); // update health text
    }

    private void Die()
    {
        GameManager.Instance.GameOver();
    }
}
