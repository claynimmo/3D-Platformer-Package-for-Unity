using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private bool iframes;
    public float iframeDuration = 0.5f;

    public bool invincible;

    public float maxHealth = 5;
    private float currentHealth = 0;

    public float regeneration = 0.1f;
    public float resistance = 1; //value to multiply the damage by. values > 1 reduce incomming damage


    public AudioClip damageSound;
    public AudioSource damageSource;

    public ParticleSystem hitEffect;
    public float volume = 1f;

    public DeathState death;

    // Update is called once per frame
    void Update()
    {

        if(regeneration!= 0 && currentHealth != 0){
            HealDamage(regeneration * Time.deltaTime);
        }
    }

    private void UpdateUI(float previousHealth){
        //add code to show the health on the healthbar. Try including the UI package and using the health bar.
        return;
    }


    //damage function that ignores iframes for effects such as poison and fire damage
    public void TakeDamageAlways(float damage){
        float previousHealth = currentHealth;
        currentHealth += (damage * resistance);
        if(currentHealth <= 0){
            currentHealth = 0;
        }
        else if(currentHealth > maxHealth){
            currentHealth = maxHealth;
            KillPlayer();
        }

        UpdateUI(previousHealth);
    }

    //heal the player, ignoring iframes. The input should be positive
    public void HealDamage(float health){
        float previousHealth = currentHealth;
        currentHealth -= health;
        if(currentHealth <= 0){
            currentHealth = 0;
        }

        UpdateUI(previousHealth);
    }

    public void TakeDamage(float damage){
        if(!iframes&&!invincible){
            iframes = true;
            Invoke("ResetIFrames",iframeDuration);

            if(damageSource!=null){
                damageSource.PlayOneShot(damageSound,volume * SettingsData.Volume);
            }

            hitEffect.Play();

            float previousHealth = currentHealth;

            currentHealth += (damage * resistance);

            if(currentHealth <= 0 ){
                currentHealth = 0;
            }
            else if(currentHealth >= maxHealth){
                currentHealth = maxHealth;
                KillPlayer();
            }
        
            UpdateUI(previousHealth);
        }
        
    }

    //update this function to include more indepth damage calculation, like for adding health debuffs
    private float DamageCalculation(float damage){
        return damage * resistance;
    }

    void ResetIFrames(){
        iframes = false;
    }

    public void PlayerRespawned(){
        invincible = false;
    }

    public void KillPlayer(){
        invincible = true;
        death.KillPlayer();
        TakeDamageAlways(-100);
    }

    public void SetRegeneration(float value){
        regeneration = value;
    }

    public void ResetRegeneration(){
        regeneration = 0;
    }

    public void SetResistance(float value){
        resistance = value;
    }
    public void ResetResistance(){
        resistance = 1;
    }

}
