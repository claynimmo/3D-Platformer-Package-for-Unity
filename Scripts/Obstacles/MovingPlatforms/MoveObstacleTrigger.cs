using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacleTrigger : MonoBehaviour
{

    public MovingPlatform[] movingPlatforms;
    public RotatingPlatform[] rotatingPlatforms;
    
    void ActivateTrigger(bool on){

        for(int i = 0; i < movingPlatforms.Length; i++){
            movingPlatforms[i].ToggleActive(on);
        }
        for(int i = 0; i < rotatingPlatforms.Length; i++){
            rotatingPlatforms[i].ToggleActive(on);
        }
    }


    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            ActivateTrigger(true);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            ActivateTrigger(false);
        }
    }
}
