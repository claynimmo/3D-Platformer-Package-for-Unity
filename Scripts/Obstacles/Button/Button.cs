using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
   
    public bool secondaryEffect;

    //the linked object must have a script that derives from the IButton interface. It uses gameobject since interfaces are not serializable in the editor
    public GameObject[] linkedObjects;

    public float offDuration = 1;

    protected bool turnedOff;

    public Material offMaterial;
    protected Material onMaterial;

    public Renderer buttonRenderer;
    public bool particleHit = false;

    protected AudioSource source;
    public AudioClip pressClip;
    public AudioClip activateClip;

    public float volume = 1f;
    
    void Awake(){
        onMaterial = buttonRenderer.material;
        source = GetComponent<AudioSource>();
    }

    protected void ButtonPressed(){
        if(turnedOff){return;}
        turnedOff = true;

        buttonRenderer.material = offMaterial;

        source.PlayOneShot(pressClip,(volume+0.2f) * SettingsData.Volume);

        foreach(GameObject link in linkedObjects){
            //get component with script using interface
            var button = link.GetComponent<IButton>();

            if(button!=null){
                if(secondaryEffect){
                    button.SecondaryButtonPressed();
                }
                else{
                    button.PrimaryButtonPressed();
                }
            }
        }
    }

    protected void ResetButton(){
        turnedOff = false;
        buttonRenderer.material = onMaterial;
        source.PlayOneShot(activateClip,volume * SettingsData.Volume);

    }
}
