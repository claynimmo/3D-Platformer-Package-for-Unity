using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVisuals : MonoBehaviour
{

    
    public Animator anim;

    public Movement movement;

    public bool playing;

    public AudioSource walkingSource;


    //footstep sounds
    public float stepInverval = 0.5f;
    private float currentStepInverval;
    private float currentStepInvervalCounter;

    public float movementDeadZone = 0.02f;

    void Awake(){
        currentStepInverval = stepInverval;
    }

    //animation states relating to the movement. For more animation states, put it in a different function. Extend this function for sprinting
    private void SetMovementAnimationStates(){
        bool verticalMovement   = movement.vertical > movementDeadZone || movement.vertical < -movementDeadZone;
        bool horizontalMovement = movement.horizontal > movementDeadZone || movement.horizontal < -movementDeadZone;
        //check if there is movement, while not airborn and not in another state
        if((verticalMovement||horizontalMovement)&&movement.grounded){

            anim.SetBool("Run",true);
        }
        else{
            anim.SetBool("Run",false);
        }
    }

    void Update(){

        if(!playing){return;}

        SetMovementAnimationStates();

        anim.SetBool("Jump",!movement.grounded);

        if(!movement.moveAble || !movement.grounded){
            StopMovementSounds();
            return;
        }

        if(!anim.GetBool("Run")){
            StopMovementSounds();
        }

        currentStepInvervalCounter += Time.deltaTime;

        if(currentStepInvervalCounter >= currentStepInverval){
            //play walking sound
            Debug.Log("footstep");
            currentStepInvervalCounter = 0;
        }
        
    }
    
    void StopMovementSounds(){
        walkingSource.Stop();
        currentStepInvervalCounter = 0;
    }
}
