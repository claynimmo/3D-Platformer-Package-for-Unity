using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObjectManager : MonoBehaviour, IButton
{
    public Renderer[] invisObjects;
    private bool notInvis = false;
    public void PrimaryButtonPressed(){
        notInvis = !notInvis;
        foreach(Renderer rend in invisObjects){
            rend.enabled = notInvis;
        }
    }

    public void SecondaryButtonPressed(){
        return;
    }
}
