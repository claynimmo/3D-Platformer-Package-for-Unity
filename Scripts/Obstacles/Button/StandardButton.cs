using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardButton : Button
{

    void OnTriggerEnter(Collider other){
        if(particleHit){return;}
        if(other.CompareTag("Player")){
            ButtonPressed();
            Invoke("ResetButton",offDuration);
        }
    }

    void OnParticleCollision(GameObject other){
        if(particleHit){
            ButtonPressed();
            Invoke("ResetButton",offDuration);
        }
    }
}
