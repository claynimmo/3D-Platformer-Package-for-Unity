using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour, IButton
{
    //this script will be enable and disabled through enabling the component itself, so that the fixed update is not always called, improving performance
    public Vector3 rotationDirection; //unit vector for rotation, values are 0 to 1

    public float rotationSpeed = 5; //float that multiplies the rotationDirection
    private Vector3 currentDirection;

    public bool disabled = false;

    void Start()
    {
        currentDirection = rotationDirection;
    }


    void FixedUpdate(){
        if(disabled){return;}
        transform.Rotate(currentDirection * rotationSpeed * Time.fixedDeltaTime);
    }

    //function broadcasted by a button
    public void PrimaryButtonPressed(){
        ChangeDirection();
    }
    public void SecondaryButtonPressed(){
        ToggleDisabled();
    }

    void ToggleDisabled(){
        disabled = !disabled;
        this.enabled = !disabled;
    }

    public void ToggleActive(bool active){
        this.enabled = active;
        disabled = !active;
    }

    void ChangeDirection(){
        currentDirection = currentDirection * -1;
    }


}
