using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public ScreenManager scrnMngr;

    public float health = 100f;
    public float maxHealth = 100f;
    public float healthRegen = 0.5f;
    public float healthRegenDelay = 3f;
    public float healthRegenDelayTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthRegenDelayTimer = healthRegenDelay;
    }

    // Update is called once per frame
    void Update()
    {
        SelfRegen();
        if(health <= 0)
        {
            scrnMngr.OnPlayerDeath();
        }
    }

    public bool TakeDamage(float damage)
    {
        if (damage < 0 && health == maxHealth) return false;

        health -= damage;
        healthRegenDelayTimer = healthRegenDelay;

        if (health < 0) health = 0;
        if (health > maxHealth) health = maxHealth;

        return true;
    }

    public void SelfRegen()
    {
        if(healthRegenDelayTimer <= 0)
        {
            if(health < maxHealth)
            {
                health += healthRegen;
            }
        }
        else
        {
            healthRegenDelayTimer -= Time.deltaTime;
        }
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        health += amount;
    }



}
