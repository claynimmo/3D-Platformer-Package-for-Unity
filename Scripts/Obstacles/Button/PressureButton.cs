using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : Button
{

    private bool used;
    private bool secondaryUsed;

    void OnCollisionEnter(Collision other){
        if(!used&&!secondaryUsed){
            used = true;
            secondaryUsed = true;
            if(other.gameObject.CompareTag("Player")){
                ButtonPressed();
            }
        }
    }

    void OnCollisionExit(Collision other){
        if(used){
            used = false;
            if(other.gameObject.CompareTag("Player")){
                ButtonPressed();
                Invoke("ResetUsed",offDuration);
            }
        }
    }

    void ResetUsed(){
        secondaryUsed = false;
        ResetButton();
    }
}
