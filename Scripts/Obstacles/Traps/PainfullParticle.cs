using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainfullParticle : MonoBehaviour
{
    public float damage = 1;

    void OnParticleCollision(GameObject other){
        IHealth health = other.GetComponent<IHealth>();
        if(health == null){return;}

        health.TakeDamage(damage);
    }
}
